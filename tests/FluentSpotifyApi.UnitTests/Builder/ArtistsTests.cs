using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Builder.Artists;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.UnitTests.Builder
{
    [TestClass]
    public class ArtistsTests : TestsBase
    {
        [TestMethod]
        public async Task ShouldGetArtist()
        {
            // Arrange
            const string id = "0OdUWJ0sBjDrqHygGUXeCF";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"artists/{id}")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Artists(id).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetArtists()
        {
            // Arrange
            var ids = new[] { "0OdUWJ0sBjDrqHygGUXeCF", "0oSGxfWSnnOXhD2fKuz2Gy" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, "artists")
                .WithExactQueryString(new Dictionary<string, string> { ["ids"] = string.Join(",", ids) })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Artists(ids).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetArtistAlbums()
        {
            // Arrange
            const string id = "0OdUWJ0sBjDrqHygGUXeCF";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"artists/{id}/albums")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Artists(id).Albums.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetArtistAlbumsWithAllParams()
        {
            // Arrange
            const string id = "0OdUWJ0sBjDrqHygGUXeCF";
            var includeGroups = new[] { AlbumType.Album, AlbumType.Single, AlbumType.AppearsOn, AlbumType.Compilation };
            const string market = "ES";
            const int limit = 30;
            const int offset = 10;

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"artists/{id}/albums")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["include_groups"] = "album,single,appears_on,compilation",
                    ["market"] = market,
                    ["limit"] = limit.ToString(CultureInfo.InvariantCulture),
                    ["offset"] = offset.ToString(CultureInfo.InvariantCulture)
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Artists(id).Albums.GetAsync(includeGroups: includeGroups, market: market, limit: limit, offset: offset);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetArtistTopTracks()
        {
            // Arrange
            const string id = "0OdUWJ0sBjDrqHygGUXeCF";
            const string country = "SE";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"artists/{id}/top-tracks")
                .WithExactQueryString(new Dictionary<string, string> { ["country"] = country })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Artists(id).TopTracks.GetAsync(country);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetArtistRelatedArtists()
        {
            // Arrange
            const string id = "0OdUWJ0sBjDrqHygGUXeCF";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"artists/{id}/related-artists")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Artists(id).RelatedArtists.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }
    }
}
