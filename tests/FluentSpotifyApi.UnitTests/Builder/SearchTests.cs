using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Builder.Search;
using FluentSpotifyApi.Expressions.Query;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.UnitTests.Builder
{
    [TestClass]
    public class SearchTests : TestsBase
    {
        [TestMethod]
        public async Task ShouldSearchAlbums()
        {
            // Arrange
            const string query = "artist:\"Metallica\" NOT album:ride";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"search")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["q"] = query,
                    ["type"] = "album"
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", @"{ ""albums"": {} }");

            // Act
            var result = await this.Client.Search.Albums.Matching(f => f.Artist == "Metallica" && !f.Album.Contains("ride")).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
            result.Page.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldSearchAlbumsWithAllParams()
        {
            // Arrange
            const string query = "TestAlbum";
            const string market = "BS";
            const int limit = 30;
            const int offset = 10;
            var externalContent = new[] { ExternalContent.Audio };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"search")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["q"] = query,
                    ["type"] = "album",
                    ["market"] = market,
                    ["limit"] = limit.ToString(CultureInfo.InvariantCulture),
                    ["offset"] = offset.ToString(CultureInfo.InvariantCulture),
                    ["include_external"] = "audio",
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", @"{ ""albums"": {} }");

            // Act
            var result = await this.Client.Search.Albums.Matching(query).GetAsync(market: market, limit: limit, offset: offset, includeExternal: externalContent);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
            result.Page.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldSearchArtists()
        {
            // Arrange
            const string query = "artist:bur* artist:the NOT artist:after";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"search")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["q"] = query,
                    ["type"] = "artist"
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", @"{ ""artists"": {} }");

            // Act
            var result = await this.Client.Search
                .Artists
                .Matching(f => f.Artist.Contains("bur* the") && !f.Artist.Contains("after"), new QueryOptions { NormalizePartialMatch = true })
                .GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
            result.Page.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldSearchPlaylists()
        {
            // Arrange
            const string query = "TestPlaylists";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"search")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["q"] = query,
                    ["type"] = "playlist"
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", @"{ ""playlists"": {} }");

            // Act
            var result = await this.Client.Search.Playlists.Matching(query).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
            result.Page.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldSearchShows()
        {
            // Arrange
            const string query = "TestShows";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"search")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["q"] = query,
                    ["type"] = "show"
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", @"{ ""shows"": {} }");

            // Act
            var result = await this.Client.Search.Shows.Matching(query).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
            result.Page.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldSearchEpisodes()
        {
            // Arrange
            const string query = "TestEpisodes";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"search")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["q"] = query,
                    ["type"] = "episode"
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", @"{ ""episodes"": {} }");

            // Act
            var result = await this.Client.Search.Episodes.Matching(query).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
            result.Page.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldSearchTracks()
        {
            // Arrange
            const string query = "TestTracks";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"search")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["q"] = query,
                    ["type"] = "track"
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", @"{ ""tracks"": {} }");

            // Act
            var result = await this.Client.Search.Tracks.Matching(query).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
            result.Page.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldSearchAny()
        {
            // Arrange
            const string query = "Test";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"search")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["q"] = query,
                    ["type"] = "album,artist,playlist,track,show,episode"
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Search.Entities().Matching(query).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldSearchAlbumsAndArtists()
        {
            // Arrange
            const string query = "Test";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"search")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["q"] = query,
                    ["type"] = "album,artist"
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Search.Entities(Entity.Album, Entity.Artist).Matching(query).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }
    }
}
