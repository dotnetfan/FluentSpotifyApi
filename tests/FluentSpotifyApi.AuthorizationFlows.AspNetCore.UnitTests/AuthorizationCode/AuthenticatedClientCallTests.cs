using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Exceptions;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.Core.Exceptions;
using FluentSpotifyApi.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.UnitTests.AuthorizationCode
{
    [TestClass]
    public class AuthenticatedClientCallTests : TestBase
    {
        [TestMethod]
        public async Task ShouldMakeAuthenticatedCallAndRenewAccessTokenWhenItIsNotValidOrUnauthorizedExceptionIsThrownAsync()
        {
            // Arrange
            this.SystemClockMock.SetupSequence(x => x.UtcNow)
                .Returns(new DateTimeOffset(new DateTime(2005, 1, 1)))
                .Returns(new DateTimeOffset(new DateTime(2005, 1, 2)))
                .Returns(new DateTimeOffset(new DateTime(2005, 1, 2)))
                .Returns(new DateTimeOffset(new DateTime(2005, 1, 2)))
                .Returns(new DateTimeOffset(new DateTime(2005, 1, 2)));

            const string refreshToken = "test refresh token";
            const string accessToken1 = "test access token 1";
            const string accessToken2 = "test access token 2";
            const string accessToken3 = "test access token 3";

            var accessTokenDto2 = new AccessTokenDto { ExpiresIn = 3600, Token = accessToken2 };
            var accessTokenDto3 = new AccessTokenDto { ExpiresIn = 3600, Token = accessToken3 };

            const string playlistId = "test playlist";
            var expectedPlaylist1 = new FullPlaylist { Id = playlistId, Description = "playlist 1" };
            var expectedPlaylist2 = new FullPlaylist { Id = playlistId, Description = "playlist 2" };
            var expectedPlaylist3 = new FullPlaylist { Id = playlistId, Description = "playlist 3" };

            const string userId = "test user";
            this.AuthenticationManager.SetAuthenticateResult((userId, refreshToken, accessToken1, new DateTimeOffset(new DateTime(2005, 1, 1, 3, 0, 0))));

            this.MockGetAccessToken(refreshToken)
                .Returns(Task.FromResult(accessTokenDto2))
                .Returns(Task.FromResult(accessTokenDto3));

            this.MockGetPlaylistHttpClientWrapper(userId, playlistId, accessToken1)
                .Returns(Task.FromResult(expectedPlaylist1));

            this.MockGetPlaylistHttpClientWrapper(userId, playlistId, accessToken2)
                .Returns(Task.FromResult(expectedPlaylist2))
                .Throws(new SpotifyHttpResponseWithErrorCodeException(System.Net.HttpStatusCode.Unauthorized, null, string.Empty));

            this.MockGetPlaylistHttpClientWrapper(userId, playlistId, accessToken3)
                .Returns(Task.FromResult(expectedPlaylist3));

            // Act + Assert
            var playlist1 = await this.Client.Me.Playlist(playlistId).GetAsync();
            playlist1.ShouldBeEquivalentTo(expectedPlaylist1);

            var playlist2 = await this.Client.Me.Playlist(playlistId).GetAsync();
            playlist2.ShouldBeEquivalentTo(expectedPlaylist2);

            var playlist3 = await this.Client.Me.Playlist(playlistId).GetAsync();
            playlist3.ShouldBeEquivalentTo(expectedPlaylist3);
        }

        [TestMethod]
        public void ShouldThrowInvalidRefreshTokenExceptionWhenTheRefreshTokenIsNotValid()
        {
            // Arrange
            this.SystemClockMock.Setup(x => x.UtcNow).Returns(new DateTimeOffset(new DateTime(2005, 1, 1)));

            const string refreshToken = "test refresh token";
            const string accessToken1 = "test access token 1";

            const string playlistId = "test playlist";
            var expectedPlaylist1 = new FullPlaylist { Id = playlistId, Description = "playlist 1" };

            const string userId = "test user";
            this.AuthenticationManager.SetAuthenticateResult((userId, refreshToken, accessToken1, new DateTimeOffset(new DateTime(2004, 1, 1, 3, 0, 0))));

            this.MockGetAccessToken(refreshToken)
                .Throws(new SpotifyHttpResponseWithAuthenticationErrorException(System.Net.HttpStatusCode.BadRequest, null, string.Empty, new FluentSpotifyApi.Core.Model.AuthenticationError { Error = "invalid_grant" }));

            // Act + Assert
            ((Func<Task>)(() => this.Client.Me.Playlist(playlistId).GetAsync())).ShouldThrow<SpotifyInvalidRefreshTokenException>();               
        }
    }
}
