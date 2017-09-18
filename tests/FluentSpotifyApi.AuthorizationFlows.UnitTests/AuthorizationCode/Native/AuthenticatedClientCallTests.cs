using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native;
using FluentSpotifyApi.Core.Exceptions;
using FluentSpotifyApi.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluentSpotifyApi.AuthorizationFlows.UnitTests.AuthorizationCode.Native
{
    [TestClass]
    public class AuthenticatedClientCallTests : TestBase
    {
        [TestMethod]
        public async Task ShouldMakeAuthenticatedCallAndRenewAccessTokenWhenItIsNotValidOrUnauthorizedExceptionIsThrownAsync()
        {
            // Arrange
            this.DateTimeOffsetProviderMock.SetupSequence(x => x.GetUtcNow())
                .Returns(new DateTimeOffset(new DateTime(2005, 1, 1)))
                .Returns(new DateTimeOffset(new DateTime(2005, 1, 1)))
                .Returns(new DateTimeOffset(new DateTime(2005, 1, 2)))
                .Returns(new DateTimeOffset(new DateTime(2005, 1, 2)))
                .Returns(new DateTimeOffset(new DateTime(2005, 1, 2)))
                .Returns(new DateTimeOffset(new DateTime(2005, 1, 2)))
                .Returns(new DateTimeOffset(new DateTime(2005, 1, 2)));

            const string authorizationKey = "test authorization ket";
            const string accessToken1 = "test access token 1";
            const string accessToken2 = "test access token 2";
            const string accessToken3 = "test access token 3";
            
            var proxyAccessToken2 = new ProxyAccessToken { ExpiresIn = 3600, Token = accessToken2 };
            var proxyAccessToken3 = new ProxyAccessToken { ExpiresIn = 3600, Token = accessToken3 };

            const string playlistId = "test playlist";
            var expectedPlaylist1 = new FullPlaylist { Id = playlistId, Description = "playlist 1" };
            var expectedPlaylist2 = new FullPlaylist { Id = playlistId, Description = "playlist 2" };
            var expectedPlaylist3 = new FullPlaylist { Id = playlistId, Description = "playlist 3" };

            var user = this.SetupAuthorizationFlow(authorizationKey: authorizationKey, accessToken: accessToken1);

            this.MockGetAccessToken(authorizationKey)
                .Returns(Task.FromResult(proxyAccessToken2))
                .Returns(Task.FromResult(proxyAccessToken3));

            this.MockGetPlaylistHttpClientWrapper(user.Id, playlistId, accessToken1)
                .Returns(Task.FromResult(expectedPlaylist1));

            this.MockGetPlaylistHttpClientWrapper(user.Id, playlistId, accessToken2)
                .Returns(Task.FromResult(expectedPlaylist2))
                .Throws(new SpotifyHttpResponseWithErrorCodeException(System.Net.HttpStatusCode.Unauthorized, null, string.Empty));

            this.MockGetPlaylistHttpClientWrapper(user.Id, playlistId, accessToken3)
                .Returns(Task.FromResult(expectedPlaylist3));

            // Act + Assert
            await this.AuthenticationManager.RestoreSessionOrAuthorizeUserAsync();

            var playlist1 = await this.Client.Me.Playlist(playlistId).GetAsync();
            playlist1.ShouldBeEquivalentTo(expectedPlaylist1);

            var playlist2 = await this.Client.Me.Playlist(playlistId).GetAsync();
            playlist2.ShouldBeEquivalentTo(expectedPlaylist2);

            var playlist3 = await this.Client.Me.Playlist(playlistId).GetAsync();
            playlist3.ShouldBeEquivalentTo(expectedPlaylist3);
        }

        [TestMethod]
        public async Task ShouldNotMakeRepeatedCallsWithInvalidAccessTokenAsync()
        {
            // Arrange
            this.DateTimeOffsetProviderMock.Setup(x => x.GetUtcNow()).Returns(new DateTimeOffset(new DateTime(2005, 1, 1)));

            const string authorizationKey = "test authorization key";
            const string accessToken1 = "test access token 1";
            const string accessToken2 = "test access token 2";

            var proxyAccessToken2 = new ProxyAccessToken { ExpiresIn = 3600, Token = accessToken2 };

            const string playlistId = "test playlist";
            var expectedPlaylist1 = new FullPlaylist { Id = playlistId, Description = "playlist 1" };

            var user = this.SetupAuthorizationFlow(authorizationKey: authorizationKey, accessToken: accessToken1);

            this.MockGetAccessToken(authorizationKey)
                .Throws(new SpotifyHttpResponseWithErrorCodeException(System.Net.HttpStatusCode.ServiceUnavailable, null, string.Empty))
                .Returns(Task.FromResult(proxyAccessToken2));

            this.MockGetPlaylistHttpClientWrapper(user.Id, playlistId, accessToken1)
                .Throws(new SpotifyHttpResponseWithErrorCodeException(System.Net.HttpStatusCode.Unauthorized, null, string.Empty));

            this.MockGetPlaylistHttpClientWrapper(user.Id, playlistId, accessToken2)
                .Returns(Task.FromResult(expectedPlaylist1));

            // Act + Assert
            await this.AuthenticationManager.RestoreSessionOrAuthorizeUserAsync();

            ((Func<Task>)(() => this.Client.Me.Playlist(playlistId).GetAsync()))
                .ShouldThrow<SpotifyHttpResponseWithErrorCodeException>()
                .Which.ErrorCode.Should().Be(System.Net.HttpStatusCode.ServiceUnavailable);

            var playlist1 = await this.Client.Me.Playlist(playlistId).GetAsync();
            playlist1.ShouldBeEquivalentTo(expectedPlaylist1);
        }

        [TestMethod]
        public void ShouldThrowUnauthorizedAccessExceptionWhenACallIsMadeWithoutUserBeingAuthorized()
        {
            // Arrange + Act + Assert
            ((Func<Task<FullPlaylist>>)(() => this.Client.Me.Playlist("testplaylist").GetAsync())).ShouldThrow<UnauthorizedAccessException>();           
        }
    }
}
