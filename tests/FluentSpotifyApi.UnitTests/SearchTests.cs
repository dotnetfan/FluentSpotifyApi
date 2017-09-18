using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Builder.Search;
using FluentSpotifyApi.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.UnitTests
{
    [TestClass]
    [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1000:KeywordsMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
    public class SearchTests : TestBase
    {
        [TestMethod]
        public async Task ShouldSearchAlbumsAsync()
        {
            // Arrange
            const int limit = 20;
            const int offset = 10;
            const string market = "BS";
            const string query = "TestAlbum";

            var mockResults = this.MockGet<SearchResult>(i => new SearchResult { Albums = new Page<SimpleAlbum>() });

            // Act
            var result = await this.Client.Search.Albums.Matching(query).GetAsync(market: market, limit: limit, offset: offset);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("type", "album"), ("q", query), ("limit", limit), ("offset", offset), ("market", market) });
            mockResults.First().RouteValues.Should().Equal(new[] { "search" });
            result.Page.Should().BeSameAs(mockResults.First().Result.Albums);
        }

        [TestMethod]
        public async Task ShouldSearchAlbumsWithDefaultsAsync()
        {
            // Arrange
            const string query = "TestAlbum";

            var mockResults = this.MockGet<SearchResult>(i => new SearchResult { Albums = new Page<SimpleAlbum>() });

            // Act
            var result = await this.Client.Search.Albums.Matching(query).GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("type", "album"), ("q", query), ("limit", 20), ("offset", 0) });
            mockResults.First().RouteValues.Should().Equal(new[] { "search" });
            result.Page.Should().BeSameAs(mockResults.First().Result.Albums);
        }

        [TestMethod]
        public async Task ShouldSearchArtistsAsync()
        {
            // Arrange
            const int limit = 20;
            const int offset = 10;
            const string market = "BS";
            const string query = "TestArtists";

            var mockResults = this.MockGet<SearchResult>(i => new SearchResult { Artists = new Page<FullArtist>() });

            // Act
            var result = await this.Client.Search.Artists.Matching(query).GetAsync(market: market, limit: limit, offset: offset);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("type", "artist"), ("q", query), ("limit", limit), ("offset", offset), ("market", market) });
            mockResults.First().RouteValues.Should().Equal(new[] { "search" });
            result.Page.Should().BeSameAs(mockResults.First().Result.Artists);
        }

        [TestMethod]
        public async Task ShouldSearchArtistsWithDefaultsAsync()
        {
            // Arrange
            const string query = "TestArtists";

            var mockResults = this.MockGet<SearchResult>(i => new SearchResult { Artists = new Page<FullArtist>() });

            // Act
            var result = await this.Client.Search.Artists.Matching(query).GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("type", "artist"), ("q", query), ("limit", 20), ("offset", 0) });
            mockResults.First().RouteValues.Should().Equal(new[] { "search" });
            result.Page.Should().BeSameAs(mockResults.First().Result.Artists);
        }

        [TestMethod]
        public async Task ShouldSearchPlaylistsAsync()
        {
            // Arrange
            const int limit = 20;
            const int offset = 10;
            const string market = "BS";
            const string query = "TestPlaylists";

            var mockResults = this.MockGet<SearchResult>(i => new SearchResult { Playlists = new Page<SimplePlaylist>() });

            // Act
            var result = await this.Client.Search.Playlists.Matching(query).GetAsync(market: market, limit: limit, offset: offset);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("type", "playlist"), ("q", query), ("limit", limit), ("offset", offset), ("market", market) });
            mockResults.First().RouteValues.Should().Equal(new[] { "search" });
            result.Page.Should().BeSameAs(mockResults.First().Result.Playlists);
        }

        [TestMethod]
        public async Task ShouldSearchPlaylistsWithDefaultsAsync()
        {
            // Arrange
            const string query = "TestPlaylists";

            var mockResults = this.MockGet<SearchResult>(i => new SearchResult { Playlists = new Page<SimplePlaylist>() });

            // Act
            var result = await this.Client.Search.Playlists.Matching(query).GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("type", "playlist"), ("q", query), ("limit", 20), ("offset", 0) });
            mockResults.First().RouteValues.Should().Equal(new[] { "search" });
            result.Page.Should().BeSameAs(mockResults.First().Result.Playlists);
        }

        [TestMethod]
        public async Task ShouldSearchTracksAsync()
        {
            // Arrange
            const int limit = 20;
            const int offset = 10;
            const string market = "BS";
            const string query = "TestTracks";

            var mockResults = this.MockGet<SearchResult>(i => new SearchResult { Tracks = new Page<FullTrack>() });

            // Act
            var result = await this.Client.Search.Tracks.Matching(query).GetAsync(market: market, limit: limit, offset: offset);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("type", "track"), ("q", query), ("limit", limit), ("offset", offset), ("market", market) });
            mockResults.First().RouteValues.Should().Equal(new[] { "search" });
            result.Page.Should().BeSameAs(mockResults.First().Result.Tracks);
        }

        [TestMethod]
        public async Task ShouldSearchTracksWithDefaultsAsync()
        {
            // Arrange
            const string query = "TestTracks";

            var mockResults = this.MockGet<SearchResult>(i => new SearchResult { Tracks = new Page<FullTrack>() });

            // Act
            var result = await this.Client.Search.Tracks.Matching(query).GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("type", "track"), ("q", query), ("limit", 20), ("offset", 0) });
            mockResults.First().RouteValues.Should().Equal(new[] { "search" });
            result.Page.Should().BeSameAs(mockResults.First().Result.Tracks);
        }

        [TestMethod]
        public async Task ShouldSearchAnyAsync()
        {
            // Arrange
            const int limit = 20;
            const int offset = 10;
            const string market = "BS";
            const string query = "Test";

            var mockResults = this.MockGet<SearchResult>(i => new SearchResult { Albums = new Page<SimpleAlbum>(), Artists = new Page<FullArtist>(), Playlists = new Page<SimplePlaylist>(), Tracks = new Page<FullTrack>() });

            // Act
            var result = await this.Client.Search.Entities().Matching(query).GetAsync(market: market, limit: limit, offset: offset);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("type", "album,artist,playlist,track"), ("q", query), ("limit", limit), ("offset", offset), ("market", market) });
            mockResults.First().RouteValues.Should().Equal(new[] { "search" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldSearchAnyWithDefaultsAsync()
        {
            // Arrange
            const string query = "Test";

            var mockResults = this.MockGet<SearchResult>(i => new SearchResult { Albums = new Page<SimpleAlbum>(), Artists = new Page<FullArtist>(), Playlists = new Page<SimplePlaylist>(), Tracks = new Page<FullTrack>() });

            // Act
            var result = await this.Client.Search.Entities().Matching(query).GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("type", "album,artist,playlist,track"), ("q", query), ("limit", 20), ("offset", 0) });
            mockResults.First().RouteValues.Should().Equal(new[] { "search" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldSearchAlbumsAndArtistsAsync()
        {
            // Arrange
            const int limit = 20;
            const int offset = 10;
            const string market = "BS";
            const string query = "Test";

            var mockResults = this.MockGet<SearchResult>(i => new SearchResult { Albums = new Page<SimpleAlbum>(), Artists = new Page<FullArtist>() });

            // Act
            var result = await this.Client.Search.Entities(Entity.Album, Entity.Artist).Matching(query).GetAsync(market: market, limit: limit, offset: offset);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("type", "album,artist"), ("q", query), ("limit", limit), ("offset", offset), ("market", market) });
            mockResults.First().RouteValues.Should().Equal(new[] { "search" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldSearchAlbumsAndArtistsWithDefaultsAsync()
        {
            // Arrange
            const string query = "Test";

            var mockResults = this.MockGet<SearchResult>(i => new SearchResult { Albums = new Page<SimpleAlbum>(), Artists = new Page<FullArtist>() });

            // Act
            var result = await this.Client.Search.Entities(Entity.Album, Entity.Artist).Matching(query).GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("type", "album,artist"), ("q", query), ("limit", 20), ("offset", 0) });
            mockResults.First().RouteValues.Should().Equal(new[] { "search" });
            result.Should().BeSameAs(mockResults.First().Result);
        }
    }
}
