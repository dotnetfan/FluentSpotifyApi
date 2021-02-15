using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.User;
using FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.AuthorizationFlows.Native.UnitTests.AuthorizationCode
{
    [TestClass]
    public class AuthorizationCodeFlowTests : TestsBase
    {
        [TestMethod]
        public async Task ShouldPerformAuthorizationCodeFlow()
        {
            // Arrange
            this.CodeVerifierProvider.CodeVerifier = "testCodeVerifier";
            this.AuthorizationRedirectUriProvider.Uri = new Uri("http://localhost/callback");
            this.AuthorizationInteractionClient.AuthorizationCode = "testCode";

            this.MockHttp
                .Expect(HttpMethod.Post, SpotifyTokenClientDefaults.TokenEndpoint.AbsolutePath)
                .WithHeaders(string.Empty)
                .WithExactQueryString(string.Empty)
                .WithExactFormData(new Dictionary<string, string>
                {
                    ["client_id"] = ClientId,
                    ["grant_type"] = "authorization_code",
                    ["code"] = "testCode",
                    ["redirect_uri"] = "http://localhost/callback",
                    ["code_verifier"] = "testCodeVerifier"
                })
                .Respond(HttpStatusCode.OK, "application/json", "{ \"refresh_token\": \"testRefreshToken1\", \"access_token\": \"testAccessToken1\", \"token_type\": \"bearer\", \"expires_in\": 3600}");

            this.MockHttp
                .Expect(HttpMethod.Get, SpotifyUserClientDefaults.UserInformationEndpoint.AbsolutePath)
                .Respond(HttpStatusCode.OK, "application/json", "{ \"id\": \"testUser\"}");

            this.MockHttp
                .Expect(HttpMethod.Get, "http://localhost/test1")
                .WithHeaders(new Dictionary<string, string> { ["Authorization"] = $"Bearer testAccessToken1" })
                .Respond(HttpStatusCode.OK);

            this.MockHttp
                .Expect(HttpMethod.Get, "http://localhost/test2")
                .WithHeaders(new Dictionary<string, string> { ["Authorization"] = $"Bearer testAccessToken1" })
                .Respond(HttpStatusCode.OK);

            this.MockHttp
                .Expect(HttpMethod.Post, SpotifyTokenClientDefaults.TokenEndpoint.AbsolutePath)
                .WithHeaders(string.Empty)
                .WithExactQueryString(string.Empty)
                .WithExactFormData(new Dictionary<string, string>
                {
                    ["grant_type"] = "refresh_token",
                    ["refresh_token"] = "testRefreshToken1",
                    ["client_id"] = ClientId,
                })
                .Respond(HttpStatusCode.OK, "application/json", "{ \"refresh_token\": \"testRefreshToken2\", \"access_token\": \"testAccessToken2\", \"token_type\": \"bearer\", \"expires_in\": 3600}");

            this.MockHttp
                .Expect(HttpMethod.Get, "http://localhost/test3")
                .WithHeaders(new Dictionary<string, string> { ["Authorization"] = $"Bearer testAccessToken2" })
                .Respond(HttpStatusCode.OK);

            this.MockHttp
                .Expect(HttpMethod.Post, SpotifyTokenClientDefaults.TokenEndpoint.AbsolutePath)
                .WithHeaders(string.Empty)
                .WithExactQueryString(string.Empty)
                .WithExactFormData(new Dictionary<string, string>
                {
                    ["grant_type"] = "refresh_token",
                    ["refresh_token"] = "testRefreshToken2",
                    ["client_id"] = ClientId,
                })
                .Respond(HttpStatusCode.OK, "application/json", "{ \"refresh_token\": \"testRefreshToken3\", \"access_token\": \"testAccessToken3\", \"token_type\": \"bearer\", \"expires_in\": 3600}");

            this.MockHttp
                .Expect(HttpMethod.Get, "http://localhost/test4")
                .WithHeaders(new Dictionary<string, string> { ["Authorization"] = $"Bearer testAccessToken3" })
                .Respond(HttpStatusCode.OK);

            // Act + Assert
            // Authorize user
            var authResult = await this.AuthenticationManager.RestoreSessionOrAuthorizeUserAsync();
            authResult.Should().Be(RestoreSessionOrAuthorizeUserResult.PerfomedUserAuthorization);

            // perform http calls
            var result = await this.TestClient.GetAsync("http://localhost/test1");
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            // token is still valid
            this.Clock.Add(TimeSpan.FromMinutes(30));

            result = await this.TestClient.GetAsync("http://localhost/test2");
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            // token is not valid anymore
            this.Clock.Add(TimeSpan.FromMinutes(31));

            result = await this.TestClient.GetAsync("http://localhost/test3");
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            // token is not valid anymore
            this.Clock.Add(TimeSpan.FromHours(2));

            result = await this.TestClient.GetAsync("http://localhost/test4");
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            // verify stored ticket
            var savedTicket = await this.AuhenticationTicketStorage.TryGetAsync();
            savedTicket.IsSuccess.Should().BeTrue();
            savedTicket.Value.Should().NotBeNull();

            var deserializedTicket = JsonSerializer.Deserialize<AuthenticationTicketRepositoryItem>(savedTicket.Value);
            deserializedTicket.Should().BeEquivalentTo(new AuthenticationTicketRepositoryItem
            {
                Version = 2,
                RefreshToken = "testRefreshToken3",
                AccessToken = "testAccessToken3",
                ExpiresAt = this.Clock.Time.AddHours(1),
                UserClaims = new Dictionary<string, string>
                {
                    ["Id"] = "testUser",
                    ["DisplayName"] = "testUser"
                }
            });

            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldRetryWhenExpiredAccessTokenErrorIsReturned()
        {
            // Arrange
            var ticket = JsonSerializer.Serialize(new AuthenticationTicketRepositoryItem
            {
                Version = 2,
                RefreshToken = "testRefreshToken1",
                AccessToken = "testAccessToken1",
                ExpiresAt = this.Clock.Time.AddHours(1),
                UserClaims = new Dictionary<string, string>
                {
                    ["Id"] = "testUser",
                    ["DisplayName"] = "testUser"
                }
            });

            await this.AuhenticationTicketStorage.SaveAsync(ticket);

            this.MockHttp
                .Expect(HttpMethod.Get, "http://localhost/test")
                .WithHeaders(new Dictionary<string, string> { ["Authorization"] = $"Bearer testAccessToken1" })
                .Respond(HttpStatusCode.Unauthorized, "application/json", "{ \"error\": { \"message\": \"The access token expired\" }}");

            this.MockHttp
                .Expect(HttpMethod.Post, SpotifyTokenClientDefaults.TokenEndpoint.AbsolutePath)
                .WithHeaders(string.Empty)
                .WithExactQueryString(string.Empty)
                .WithExactFormData(new Dictionary<string, string>
                {
                    ["grant_type"] = "refresh_token",
                    ["refresh_token"] = "testRefreshToken1",
                    ["client_id"] = ClientId,
                })
                .Respond(HttpStatusCode.OK, "application/json", "{ \"refresh_token\": \"testRefreshToken2\", \"access_token\": \"testAccessToken2\", \"token_type\": \"bearer\", \"expires_in\": 3600}");

            this.MockHttp
                .Expect(HttpMethod.Get, "http://localhost/test")
                .WithHeaders(new Dictionary<string, string> { ["Authorization"] = $"Bearer testAccessToken2" })
                .Respond(HttpStatusCode.OK);

            // Act + Assert
            // Authorize user
            var authResult = await this.AuthenticationManager.RestoreSessionOrAuthorizeUserAsync();
            authResult.Should().Be(RestoreSessionOrAuthorizeUserResult.RestoredSessionFromLocalStorage);

            this.Clock.Time.AddMinutes(30);

            var result = await this.TestClient.GetAsync("http://localhost/test");
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            // verify stored ticket
            var savedTicket = await this.AuhenticationTicketStorage.TryGetAsync();
            savedTicket.IsSuccess.Should().BeTrue();
            savedTicket.Value.Should().NotBeNull();

            var deserializedTicket = JsonSerializer.Deserialize<AuthenticationTicketRepositoryItem>(savedTicket.Value);
            deserializedTicket.Should().BeEquivalentTo(new AuthenticationTicketRepositoryItem
            {
                Version = 2,
                RefreshToken = "testRefreshToken2",
                AccessToken = "testAccessToken2",
                ExpiresAt = this.Clock.Time.AddHours(1),
                UserClaims = new Dictionary<string, string>
                {
                    ["Id"] = "testUser",
                    ["DisplayName"] = "testUser"
                }
            });

            this.MockHttp.VerifyNoOutstandingExpectation();
        }
    }
}