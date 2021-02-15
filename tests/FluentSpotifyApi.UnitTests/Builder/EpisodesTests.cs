using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.UnitTests.Builder
{
    [TestClass]
    public class EpisodesTests : TestsBase
    {
        [TestMethod]
        public async Task ShouldGetEpisode()
        {
            // Arrange
            const string id = "512ojhOuo1ktJprKbVcKyQ";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"episodes/{id}")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Episodes(id).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetEpisodeWithAllParams()
        {
            // Arrange
            const string id = "512ojhOuo1ktJprKbVcKyQ";
            const string market = "BS";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"episodes/{id}")
                .WithExactQueryString(new Dictionary<string, string> { ["market"] = market })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Episodes(id).GetAsync(market: market);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetEpisodes()
        {
            // Arrange
            var ids = new[] { "512ojhOuo1ktJprKbVcKyQ", "0Q86acNRm6V9GYx55SXKwf" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, "episodes")
                .WithExactQueryString(new Dictionary<string, string> { ["ids"] = string.Join(",", ids) })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Episodes(ids).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetEpisodesWithAllParams()
        {
            // Arrange
            const string market = "BS";
            var ids = new[] { "512ojhOuo1ktJprKbVcKyQ", "0Q86acNRm6V9GYx55SXKwf" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, "episodes")
                .WithExactQueryString(new Dictionary<string, string> { ["market"] = market, ["ids"] = string.Join(",", ids) })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Episodes(ids).GetAsync(market: market);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }
    }
}
