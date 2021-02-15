using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.AuthorizationFlows.UnitTests.ClientCredentials
{
    [TestClass]
    public class ClientCredentialsFlowTests : TestsBase
    {
        [TestMethod]
        public async Task ShouldPerformClientCredentialsFlow()
        {
            // Arrange
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}"));

            this.MockHttp
                .Expect(HttpMethod.Post, SpotifyTokenClientDefaults.TokenEndpoint.AbsolutePath)
                .WithHeaders(new Dictionary<string, string> { ["Authorization"] = $"Basic {credentials}" })
                .WithExactQueryString(string.Empty)
                .WithExactFormData(new Dictionary<string, string> { ["grant_type"] = "client_credentials" })
                .Respond(HttpStatusCode.OK, "application/json", "{ \"access_token\": \"testToken1\", \"token_type\": \"bearer\", \"expires_in\": 3600}");

            this.MockHttp
                .Expect(HttpMethod.Get, "http://localhost/test1")
                .WithHeaders(new Dictionary<string, string> { ["Authorization"] = $"Bearer testToken1" })
                .Respond(HttpStatusCode.OK);

            this.MockHttp
                .Expect(HttpMethod.Get, "http://localhost/test2")
                .WithHeaders(new Dictionary<string, string> { ["Authorization"] = $"Bearer testToken1" })
                .Respond(HttpStatusCode.OK);

            this.MockHttp
                .Expect(HttpMethod.Post, SpotifyTokenClientDefaults.TokenEndpoint.AbsolutePath)
                .WithHeaders(new Dictionary<string, string> { ["Authorization"] = $"Basic {credentials}" })
                .WithExactQueryString(string.Empty)
                .WithExactFormData(new Dictionary<string, string> { ["grant_type"] = "client_credentials" })
                .Respond(HttpStatusCode.OK, "application/json", "{ \"access_token\": \"testToken2\", \"token_type\": \"bearer\", \"expires_in\": 3600}");

            this.MockHttp
                .Expect(HttpMethod.Get, "http://localhost/test3")
                .WithHeaders(new Dictionary<string, string> { ["Authorization"] = $"Bearer testToken2" })
                .Respond(HttpStatusCode.OK);

            // Act + Assert
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

            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldRetryWhenExpiredAccessTokenErrorIsReturned()
        {
            // Arrange
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}"));

            this.MockHttp
                .Expect(HttpMethod.Post, SpotifyTokenClientDefaults.TokenEndpoint.AbsolutePath)
                .WithHeaders(new Dictionary<string, string> { ["Authorization"] = $"Basic {credentials}" })
                .WithExactQueryString(string.Empty)
                .WithExactFormData(new Dictionary<string, string> { ["grant_type"] = "client_credentials" })
                .Respond(HttpStatusCode.OK, "application/json", "{ \"access_token\": \"testToken1\", \"token_type\": \"bearer\", \"expires_in\": 3600}");

            this.MockHttp
                .Expect(HttpMethod.Get, "http://localhost/test")
                .WithHeaders(new Dictionary<string, string> { ["Authorization"] = $"Bearer testToken1" })
                .Respond(HttpStatusCode.Unauthorized, "application/json", "{ \"error\": { \"message\": \"The access token expired\" }}");

            this.MockHttp
                .Expect(HttpMethod.Post, SpotifyTokenClientDefaults.TokenEndpoint.AbsolutePath)
                .WithHeaders(new Dictionary<string, string> { ["Authorization"] = $"Basic {credentials}" })
                .WithExactQueryString(string.Empty)
                .WithExactFormData(new Dictionary<string, string> { ["grant_type"] = "client_credentials" })
                .Respond(HttpStatusCode.OK, "application/json", "{ \"access_token\": \"testToken2\", \"token_type\": \"bearer\", \"expires_in\": 3600}");

            this.MockHttp
                .Expect(HttpMethod.Get, "http://localhost/test")
                .WithHeaders(new Dictionary<string, string> { ["Authorization"] = $"Bearer testToken2" })
                .Respond(HttpStatusCode.OK);

            // Act + Assert
            var result = await this.TestClient.GetAsync("http://localhost/test");
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            this.MockHttp.VerifyNoOutstandingExpectation();
        }
    }
}
