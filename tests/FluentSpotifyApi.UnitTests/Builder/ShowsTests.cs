using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.UnitTests.Builder
{
    [TestClass]
    public class ShowsTests : TestsBase
    {
        [TestMethod]
        public async Task ShouldGetShow()
        {
            // Arrange
            const string id = "38bS44xjbVVZ3No3ByF1dJ";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"shows/{id}")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Shows(id).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetShowWithAllParams()
        {
            // Arrange
            const string id = "38bS44xjbVVZ3No3ByF1dJ";
            const string market = "BS";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"shows/{id}")
                .WithExactQueryString(new Dictionary<string, string> { ["market"] = market })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Shows(id).GetAsync(market: market);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetShows()
        {
            // Arrange
            var ids = new[] { "38bS44xjbVVZ3No3ByF1dJ", "5CfCWKI5pZ28U0uOzXkDHe" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, "shows")
                .WithExactQueryString(new Dictionary<string, string> { ["ids"] = string.Join(",", ids) })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Shows(ids).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetShowsWithAllParams()
        {
            // Arrange
            const string market = "BS";
            var ids = new[] { "38bS44xjbVVZ3No3ByF1dJ", "5CfCWKI5pZ28U0uOzXkDHe" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, "shows")
                .WithExactQueryString(new Dictionary<string, string> { ["market"] = market, ["ids"] = string.Join(",", ids) })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Shows(ids).GetAsync(market: market);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetShowEpisodes()
        {
            // Arrange
            const string id = "38bS44xjbVVZ3No3ByF1dJ";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"shows/{id}/episodes")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Shows(id).Episodes.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetShowEpisodesWithAllParams()
        {
            // Arrange
            const string market = "BS";
            const int limit = 30;
            const int offset = 10;
            const string id = "38bS44xjbVVZ3No3ByF1dJ";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"shows/{id}/episodes")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["market"] = market,
                    ["limit"] = limit.ToString(CultureInfo.InvariantCulture),
                    ["offset"] = offset.ToString(CultureInfo.InvariantCulture),
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Shows(id).Episodes.GetAsync(limit: limit, offset: offset, market: market);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }
    }
}
