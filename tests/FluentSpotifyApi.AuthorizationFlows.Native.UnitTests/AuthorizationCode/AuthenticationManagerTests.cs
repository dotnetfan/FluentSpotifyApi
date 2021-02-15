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
using FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.AuthorizationFlows.Native.UnitTests.AuthorizationCode
{
    [TestClass]
    public class AuthenticationManagerTests : TestsBase
    {
        [TestMethod]
        public async Task ShouldPerformAuthorizationWhenRestoreSessionOrAuthorizeUserIsCalledWithoutSession()
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
                .Respond(HttpStatusCode.OK, "application/json", "{ \"refresh_token\": \"testRefreshToken\", \"access_token\": \"testAccessToken\", \"token_type\": \"bearer\", \"expires_in\": 3600}");

            this.MockHttp
                .Expect(HttpMethod.Get, SpotifyUserClientDefaults.UserInformationEndpoint.AbsolutePath)
                .Respond(HttpStatusCode.OK, "application/json", "{ \"id\": \"testUser\" }");

            // Act
            var authResult = await this.AuthenticationManager.RestoreSessionOrAuthorizeUserAsync();

            // Assert
            authResult.Should().Be(RestoreSessionOrAuthorizeUserResult.PerfomedUserAuthorization);
            this.AuthenticationManager.GetUserClaims().TryGetClaim(UserClaimTypes.Id, out var claim).Should().BeTrue();
            claim.Value.Should().Be("testUser");

            var savedTicket = await this.AuhenticationTicketStorage.TryGetAsync();
            savedTicket.IsSuccess.Should().BeTrue();
            savedTicket.Value.Should().NotBeNull();

            var deserializedTicket = JsonSerializer.Deserialize<AuthenticationTicketRepositoryItem>(savedTicket.Value);
            deserializedTicket.Should().BeEquivalentTo(new AuthenticationTicketRepositoryItem
            {
                Version = 2,
                RefreshToken = "testRefreshToken",
                AccessToken = "testAccessToken",
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
        public async Task ShouldRestoreSessionWhenRestoreSessionOrAuthorizeUserIsCalledWithSession()
        {
            // Arrange
            await this.InitializeStorageAsync();

            // Act
            var result = await this.AuthenticationManager.RestoreSessionOrAuthorizeUserAsync();

            // Assert
            result.Should().Be(RestoreSessionOrAuthorizeUserResult.RestoredSessionFromLocalStorage);
            this.AuthenticationManager.GetUserClaims().TryGetClaim(UserClaimTypes.Id, out var claim).Should().BeTrue();
            claim.Value.Should().Be("testUser");
        }

        [TestMethod]
        public async Task ShouldDoNothingWhenRestoreSessionOrAuthorizeUserIsCalledSecondTime()
        {
            // Arrange
            await this.InitializeStorageAsync();

            // Act
            await this.AuthenticationManager.RestoreSessionOrAuthorizeUserAsync();
            var result = await this.AuthenticationManager.RestoreSessionOrAuthorizeUserAsync();

            // Assert
            result.Should().Be(RestoreSessionOrAuthorizeUserResult.NoAction);
        }

        [TestMethod]
        public async Task ShuldRestoreSessionWhenRestoreSessionIsCalledWithSession()
        {
            // Arrange
            await this.InitializeStorageAsync();

            // Act
            var result = await this.AuthenticationManager.RestoreSessionAsync();

            // Assert
            result.Should().Be(true);
            this.AuthenticationManager.GetUserClaims().TryGetClaim(UserClaimTypes.Id, out var claim).Should().BeTrue();
            claim.Value.Should().Be("testUser");
        }

        [TestMethod]
        public async Task ShouldThrowSessionNotFoundExceptionWhenRestoreSessionIsCalledWithoutSession()
        {
            // Arrange + Act + Assert
            await ((Func<Task>)(() => this.AuthenticationManager.RestoreSessionAsync())).Should().ThrowAsync<SpotifySessionNotFoundException>();
        }

        [TestMethod]
        public async Task ShouldDoNothingWhenRestoreSessionIsCalledSecondTime()
        {
            // Arrange
            await this.InitializeStorageAsync();

            // Act
            await this.AuthenticationManager.RestoreSessionAsync();
            var result = await this.AuthenticationManager.RestoreSessionAsync();

            // Assert
            result.Should().Be(false);
        }

        [TestMethod]
        public async Task ShouldRemoveSession()
        {
            // Arrange
            await this.InitializeStorageAsync();

            // Act
            var result = await this.AuthenticationManager.RemoveSessionAsync();

            // Assert
            result.Should().Be(true);

            var savedTicket = await this.AuhenticationTicketStorage.TryGetAsync();
            savedTicket.IsSuccess.Should().BeFalse();
        }

        [TestMethod]
        public async Task ShouldGetSessionState()
        {
            // Arrange + Act + Assert
            var state = await this.AuthenticationManager.GetSessionStateAsync();
            state.Should().Be(SessionState.NotFound);

            await this.InitializeStorageAsync();
            state = await this.AuthenticationManager.GetSessionStateAsync();
            state.Should().Be(SessionState.StoredInLocalStorage);

            await this.AuthenticationManager.RestoreSessionAsync();
            state = await this.AuthenticationManager.GetSessionStateAsync();
            state.Should().Be(SessionState.CachedInMemory);
        }

        private async Task InitializeStorageAsync()
        {
            var ticket = JsonSerializer.Serialize(new AuthenticationTicketRepositoryItem
            {
                Version = 2,
                RefreshToken = "testRefreshToken",
                AccessToken = "testAccessToken",
                ExpiresAt = this.Clock.Time.AddHours(1),
                UserClaims = new Dictionary<string, string>
                {
                    ["Id"] = "testUser",
                    ["DisplayName"] = "testUser"
                }
            });

            await this.AuhenticationTicketStorage.SaveAsync(ticket);
        }
    }
}
