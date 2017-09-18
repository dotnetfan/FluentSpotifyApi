using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Builder.Browse;
using FluentSpotifyApi.Model.Browse;
using FluentSpotifyApi.Model.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.UnitTests
{
    [TestClass]
    [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1000:KeywordsMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
    public class BrowseTests : TestBase
    {
        [TestMethod]
        public async Task ShouldGetFeaturedPlaylistsAsync()
        {
            // Arrange
            const int limit = 20;
            const int offset = 10;
            const string country = "MX";
            const string locale = "es_MX";
            var timestamp = new DateTime(2014, 10, 23, 7, 0, 0);

            var mockResults = this.MockGet<FeaturedPlaylists>();

            // Act
            var result = await this.Client.Browse.FeaturedPlaylists.GetAsync(limit: limit, offset: offset, country: country, locale: locale, timestamp: timestamp);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", limit), ("offset", offset), ("country", country), ("locale", locale), ("timestamp", "2014-10-23T07:00:00") });
            mockResults.First().RouteValues.Should().Equal(new[] { "browse", "featured-playlists" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetFeaturedPlaylistsWithDefaultsAsync()
        {
            // Arrange
            var mockResults = this.MockGet<FeaturedPlaylists>();

            // Act
            var result = await this.Client.Browse.FeaturedPlaylists.GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", 20), ("offset", 0) });
            mockResults.First().RouteValues.Should().Equal(new[] { "browse", "featured-playlists" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetNewReleasesAsync()
        {
            // Arrange
            const int limit = 20;
            const int offset = 10;
            const string country = "MX";

            var mockResults = this.MockGet<NewReleases>();

            // Act
            var result = await this.Client.Browse.NewReleases.GetAsync(limit: limit, offset: offset, country: country);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", limit), ("offset", offset), ("country", country) });
            mockResults.First().RouteValues.Should().Equal(new[] { "browse", "new-releases" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetNewReleasesWithDefaultsAsync()
        {
            // Arrange
            var mockResults = this.MockGet<NewReleases>();

            // Act
            var result = await this.Client.Browse.NewReleases.GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", 20), ("offset", 0) });
            mockResults.First().RouteValues.Should().Equal(new[] { "browse", "new-releases" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetCategoriesAsync()
        {
            // Arrange
            const int limit = 20;
            const int offset = 10;
            const string country = "MX";
            const string locale = "es_MX";

            var mockResults = this.MockGet<CategoriesPageMessage>();

            // Act
            var result = await this.Client.Browse.Categories.GetAsync(limit: limit, offset: offset, country: country, locale: locale);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", limit), ("offset", offset), ("country", country), ("locale", locale) });
            mockResults.First().RouteValues.Should().Equal(new[] { "browse", "categories" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetCategoriesWithDefaultsAsync()
        {
            // Arrange
            var mockResults = this.MockGet<CategoriesPageMessage>();

            // Act
            var result = await this.Client.Browse.Categories.GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", 20), ("offset", 0) });
            mockResults.First().RouteValues.Should().Equal(new[] { "browse", "categories" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetCategoryAsync()
        {
            // Arrange
            const string categoryId = "party";
            const string country = "MX";
            const string locale = "es_MX";

            var mockResults = this.MockGet<Category>();

            // Act
            var result = await this.Client.Browse.Category(categoryId).GetAsync(country: country, locale: locale);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("country", country), ("locale", locale) });
            mockResults.First().RouteValues.Should().Equal(new[] { "browse", "categories", categoryId });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetCategoryPlaylistsAsync()
        {
            // Arrange
            const string categoryId = "party";
            const int limit = 20;
            const int offset = 10;
            const string country = "MX";

            var mockResults = this.MockGet<SimplePlaylistsPageMessage>();

            // Act
            var result = await this.Client.Browse.Category(categoryId).Playlists.GetAsync(country: country, limit: limit, offset: offset);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("country", country), ("offset", offset), ("limit", limit) });
            mockResults.First().RouteValues.Should().Equal(new[] { "browse", "categories", categoryId, "playlists" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetCategoryPlaylistsWithDefaultsAsync()
        {
            // Arrange
            const string categoryId = "party";

            var mockResults = this.MockGet<SimplePlaylistsPageMessage>();

            // Act
            var result = await this.Client.Browse.Category(categoryId).Playlists.GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("offset", 0), ("limit", 20) });
            mockResults.First().RouteValues.Should().Equal(new[] { "browse", "categories", categoryId, "playlists" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetRecommedationsAsync()
        {
            // Arrange
            const string market = "US";
            const int limit = 50;
            var seedArtists = new[] { "4NHQUGzhtTLFvgF5SZesLK", "JIOJOIJS8DHSH" };
            var seedTracks = new[] { "0c6xIDDpzE81m2q797ordA", "JJIIJOJHUI766" };
            var seedGenres = new[] { "HJKIODJSUHUIJUHUIHI", "JHHSUIDHSIU0BBYU" };
            var builderAction = (Action<ITuneableTrackAttributesBuilder>)(a => a
                                .Acousticness(v => v.Min(0.33f).Max(3.33f).Target(1.33f))
                                .Danceability(v => v.Min(3.66f).Max(5.77f).Target(8.33f))
                                .DurationMs(v => v.Min(23).Max(67).Target(88))
                                .Energy(v => v.Min(9.22f).Max(10.44f).Target(6.66f))
                                .Instrumentalness(v => v.Min(4.55f).Max(95.22f).Target(1.11f))
                                .Key(v => v.Min(2).Max(9).Target(5))
                                .Liveness(v => v.Min(7.77f).Max(9.99f).Target(10.11f))
                                .Loudness(v => v.Min(55.33f).Max(8.77f).Target(44.44f))
                                .Mode(v => v.Min(11).Max(22).Target(66))
                                .Popularity(v => v.Min(10).Max(20).Target(30))
                                .Speechiness(v => v.Min(3.01f).Max(11.08f).Target(22.4f))
                                .Tempo(v => v.Min(5.66f).Max(8.55f).Target(11.11f))
                                .TimeSignature(v => v.Min(55).Max(33).Target(66))
                                .DynamicAttribute<float>("valence", v => v.Min(11f).Max(33f).Target(88f)));

            var mockResults = this.MockGet<Recommendations>();

            // Act
            var result = await this.Client.Browse.Recommendations.GetAsync(
                market: market,
                limit: limit,
                seedArtists: seedArtists,
                seedTracks: seedTracks,
                seedGenres: seedGenres,
                buildTunableTrackAttributes: builderAction);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] 
            {
                ("market", market),
                ("limit", limit),
                ("seed_artists", string.Join(",", seedArtists)),
                ("seed_tracks", string.Join(",", seedTracks)),
                ("seed_genres", string.Join(",", seedGenres)),
                ("min_acousticness", 0.33f),
                ("max_acousticness", 3.33f),
                ("target_acousticness", 1.33f),
                ("min_danceability", 3.66f),
                ("max_danceability", 5.77f),
                ("target_danceability", 8.33f),
                ("min_duration_ms", 23),
                ("max_duration_ms", 67),
                ("target_duration_ms", 88),
                ("min_energy", 9.22f),
                ("max_energy", 10.44f),
                ("target_energy", 6.66f),
                ("min_instrumentalness", 4.55f),
                ("max_instrumentalness", 95.22f),
                ("target_instrumentalness", 1.11f),
                ("min_key", 2),
                ("max_key", 9),
                ("target_key", 5),
                ("min_liveness", 7.77f),
                ("max_liveness", 9.99f),
                ("target_liveness", 10.11f),
                ("min_loudness", 55.33f),
                ("max_loudness", 8.77f),
                ("target_loudness", 44.44f),
                ("min_mode", 11),
                ("max_mode", 22),
                ("target_mode", 66),
                ("min_popularity", 10),
                ("max_popularity", 20),
                ("target_popularity", 30),
                ("min_speechiness", 3.01f),
                ("max_speechiness", 11.08f),
                ("target_speechiness", 22.4f),
                ("min_tempo", 5.66f),
                ("max_tempo", 8.55f),
                ("target_tempo", 11.11f),
                ("min_time_signature", 55),
                ("max_time_signature", 33),
                ("target_time_signature", 66),
                ("min_valence", 11f),
                ("max_valence", 33f),
                ("target_valence", 88f)
            });

            mockResults.First().RouteValues.Should().Equal(new[] { "recommendations" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetRecommedationsWithDefaultsAsync()
        {
            // Arrange
            var mockResults = this.MockGet<Recommendations>();

            // Act
            var result = await this.Client.Browse.Recommendations.GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[]
            {
                ("limit", 20),
            });

            mockResults.First().RouteValues.Should().Equal(new[] { "recommendations" });
            result.Should().BeSameAs(mockResults.First().Result);
        }
    }
}
