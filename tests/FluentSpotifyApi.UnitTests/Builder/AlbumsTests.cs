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
    public class AlbumsTests : TestsBase
    {
        [TestMethod]
        public async Task ShouldGetAlbum()
        {
            // Arrange
            const string id = "6akEvsycLGftJxYudPjmqK";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"albums/{id}")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Albums(id).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetAlbumWithAllParams()
        {
            // Arrange
            const string id = "6akEvsycLGftJxYudPjmqK";
            const string market = "BS";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"albums/{id}")
                .WithExactQueryString(new Dictionary<string, string> { ["market"] = market })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Albums(id).GetAsync(market: market);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetAlbums()
        {
            // Arrange
            var ids = new[] { "6akEvsycLGftJxYudPjmqK", "41MnTivkwTO3UUJ8DrqEJJ" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, "albums")
                .WithExactQueryString(new Dictionary<string, string> { ["ids"] = string.Join(",", ids) })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Albums(ids).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetAlbumsWithAllParams()
        {
            // Arrange
            const string market = "BS";
            var ids = new[] { "6akEvsycLGftJxYudPjmqK", "41MnTivkwTO3UUJ8DrqEJJ" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, "albums")
                .WithExactQueryString(new Dictionary<string, string> { ["market"] = market, ["ids"] = string.Join(",", ids) })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Albums(ids).GetAsync(market: market);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetAlbumTracks()
        {
            // Arrange
            const string id = "6akEvsycLGftJxYudPjmqK";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"albums/{id}/tracks")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Albums(id).Tracks.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetAlbumTracksWithAllParams()
        {
            // Arrange
            const string market = "BS";
            const int limit = 30;
            const int offset = 10;
            const string id = "6akEvsycLGftJxYudPjmqK";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"albums/{id}/tracks")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["limit"] = limit.ToString(CultureInfo.InvariantCulture),
                    ["offset"] = offset.ToString(CultureInfo.InvariantCulture),
                    ["market"] = market
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Albums(id).Tracks.GetAsync(market: market, limit: limit, offset: offset);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }
    }
}
