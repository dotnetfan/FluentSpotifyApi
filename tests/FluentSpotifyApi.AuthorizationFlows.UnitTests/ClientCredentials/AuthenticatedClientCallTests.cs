using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.Core.Client;
using FluentSpotifyApi.Core.Exceptions;
using FluentSpotifyApi.Core.Internal;
using FluentSpotifyApi.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluentSpotifyApi.AuthorizationFlows.UnitTests.ClientCredentials
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
                .Returns(new DateTimeOffset(new DateTime(2005, 1, 2)))
                .Returns(new DateTimeOffset(new DateTime(2005, 1, 2)))
                .Returns(new DateTimeOffset(new DateTime(2005, 1, 2)))
                .Returns(new DateTimeOffset(new DateTime(2005, 1, 2)))
                .Returns(new DateTimeOffset(new DateTime(2005, 1, 2)));

            const string accessToken1 = "test access token 1";
            const string accessToken2 = "test access token 2";
            const string accessToken3 = "test access token 3";
            var accessTokenDto1 = new AccessTokenDto { ExpiresIn = 3600, Token = accessToken1 };
            var accessTokenDto2 = new AccessTokenDto { ExpiresIn = 3600, Token = accessToken2 };
            var accessTokenDto3 = new AccessTokenDto { ExpiresIn = 3600, Token = accessToken3 };

            const string userId = "johnsmith";
            var expectedUser1 = new PublicUser { Id = userId, DisplayName = "John Smith 1" };
            var expectedUser2 = new PublicUser { Id = userId, DisplayName = "John Smith 2" };
            var expectedUser3 = new PublicUser { Id = userId, DisplayName = "John Smith 3" };

            this.AuthorizationFlowsHttpClientMock
                .SetupSequence(x => x.SendAsync<AccessTokenDto>(
                    this.Options.TokenEndpoint,
                    HttpMethod.Post,                    
                    null,
                    It.Is<object>(item => SpotifyObjectHelpers.GetPropertyBag(item).Single().Equals(new KeyValuePair<string, object>("grant_type", "client_credentials"))),
                    It.Is<IEnumerable<KeyValuePair<string, string>>>(item => item.Single().Equals(new KeyValuePair<string, string>("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{this.Options.ClientId}:{this.Options.ClientSecret}"))}"))),
                    It.IsAny<CancellationToken>(),
                    It.Is<object[]>(item => item.Length == 0)))
                .Returns(Task.FromResult(accessTokenDto1))
                .Returns(Task.FromResult(accessTokenDto2))
                .Returns(Task.FromResult(accessTokenDto3));

            this.HttpClientWrapperMock
                .SetupSequence(x => x.SendAsync(
                    It.Is<HttpRequest<PublicUser>>(httpRequest => httpRequest.RequestHeaders.Contains(new KeyValuePair<string, string>("Authorization", $"Bearer {accessToken1}"))),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expectedUser1));

            this.HttpClientWrapperMock
                .SetupSequence(x => x.SendAsync(
                    It.Is<HttpRequest<PublicUser>>(httpRequest => httpRequest.RequestHeaders.Contains(new KeyValuePair<string, string>("Authorization", $"Bearer {accessToken2}"))),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expectedUser2))
                .Throws(new SpotifyHttpResponseWithErrorCodeException(System.Net.HttpStatusCode.Unauthorized, null, string.Empty));

            this.HttpClientWrapperMock
                .SetupSequence(x => x.SendAsync(
                    It.Is<HttpRequest<PublicUser>>(httpRequest => httpRequest.RequestHeaders.Contains(new KeyValuePair<string, string>("Authorization", $"Bearer {accessToken3}"))),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expectedUser3));

            // Act + Assert
            var user1 = await this.Client.User(userId).GetAsync();
            user1.ShouldBeEquivalentTo(expectedUser1);

            var user2 = await this.Client.User(userId).GetAsync();
            user2.ShouldBeEquivalentTo(expectedUser2);

            var user3 = await this.Client.User(userId).GetAsync();
            user3.ShouldBeEquivalentTo(expectedUser3);
        }

        [TestMethod]
        public async Task ShouldNotMakeRepeatedCallsWithInvalidAccessTokenAsync()
        {
            // Arrange
            this.DateTimeOffsetProviderMock.Setup(x => x.GetUtcNow()).Returns(new DateTimeOffset(new DateTime(2005, 1, 1)));

            const string accessToken1 = "test access token 1";
            const string accessToken2 = "test access token 2";
            var accessTokenDto1 = new AccessTokenDto { ExpiresIn = 3600, Token = accessToken1 };
            var accessTokenDto2 = new AccessTokenDto { ExpiresIn = 3600, Token = accessToken2 };

            const string userId = "johnsmith";
            var expectedUser1 = new PublicUser { Id = userId, DisplayName = "John Smith" };

            this.AuthorizationFlowsHttpClientMock
                .SetupSequence(x => x.SendAsync<AccessTokenDto>(
                    this.Options.TokenEndpoint,
                    HttpMethod.Post,
                    null,
                    It.Is<object>(item => SpotifyObjectHelpers.GetPropertyBag(item).Single().Equals(new KeyValuePair<string, object>("grant_type", "client_credentials"))),
                    It.Is<IEnumerable<KeyValuePair<string, string>>>(item => item.Single().Equals(new KeyValuePair<string, string>("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{this.Options.ClientId}:{this.Options.ClientSecret}"))}"))),
                    It.IsAny<CancellationToken>(),
                    It.Is<object[]>(item => item.Length == 0)))
                .Returns(Task.FromResult(accessTokenDto1))
                .Throws(new SpotifyHttpResponseWithErrorCodeException(System.Net.HttpStatusCode.ServiceUnavailable, null, string.Empty))
                .Returns(Task.FromResult(accessTokenDto2));

            this.HttpClientWrapperMock
                .SetupSequence(x => x.SendAsync(
                    It.Is<HttpRequest<PublicUser>>(httpRequest => httpRequest.RequestHeaders.Contains(new KeyValuePair<string, string>("Authorization", $"Bearer {accessToken1}"))),
                    It.IsAny<CancellationToken>()))
                .Throws(new SpotifyHttpResponseWithErrorCodeException(System.Net.HttpStatusCode.Unauthorized, null, string.Empty));

            this.HttpClientWrapperMock
                .SetupSequence(x => x.SendAsync(
                    It.Is<HttpRequest<PublicUser>>(httpRequest => httpRequest.RequestHeaders.Contains(new KeyValuePair<string, string>("Authorization", $"Bearer {accessToken2}"))),
                    It.IsAny<CancellationToken>()))                         
                .Returns(Task.FromResult(expectedUser1));

            // Act + Assert
            ((Func<Task>)(() => this.Client.User(userId).GetAsync()))
                .ShouldThrow<SpotifyHttpResponseWithErrorCodeException>()
                .Which.ErrorCode.Should().Be(System.Net.HttpStatusCode.ServiceUnavailable);

            var user = await this.Client.User(userId).GetAsync();
            user.ShouldBeEquivalentTo(expectedUser1);
        }
    }
}
