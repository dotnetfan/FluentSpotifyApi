using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Builder.Browse;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.UnitTests.Builder.Browse
{
    [TestClass]
    public class BrowseTests : TestsBase
    {
        [TestMethod]
        public async Task ShouldGetFeaturedPlaylists()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"browse/featured-playlists")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Browse.FeaturedPlaylists.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetFeaturedPlaylistsWithAllParams()
        {
            // Arrange
            const string country = "MX";
            const string locale = "es_MX";
            var timestamp = new DateTime(2014, 10, 23, 7, 0, 0);
            const int limit = 30;
            const int offset = 10;

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"browse/featured-playlists")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["country"] = country,
                    ["locale"] = locale,
                    ["timestamp"] = "2014-10-23T07:00:00",
                    ["limit"] = limit.ToString(CultureInfo.InvariantCulture),
                    ["offset"] = offset.ToString(CultureInfo.InvariantCulture),
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Browse.FeaturedPlaylists.GetAsync(country: country, locale: locale, timestamp: timestamp, limit: limit, offset: offset);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetNewReleases()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"browse/new-releases")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Browse.NewReleases.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetNewReleasesWithAllParams()
        {
            // Arrange
            const string country = "MX";
            const int limit = 30;
            const int offset = 10;

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"browse/new-releases")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["country"] = country,
                    ["limit"] = limit.ToString(CultureInfo.InvariantCulture),
                    ["offset"] = offset.ToString(CultureInfo.InvariantCulture),
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Browse.NewReleases.GetAsync(country: country, limit: limit, offset: offset);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetRecommedations()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"recommendations")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Browse.Recommendations.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetRecommedationsWithAllParams()
        {
            // Arrange
            const int limit = 50;
            const string market = "US";
            var seedArtists = new[] { "4NHQUGzhtTLFvgF5SZesLK", "JIOJOIJS8DHSH" };
            var seedTracks = new[] { "0oSGxfWSnnOXhD2fKuz2Gy", "3dBVyJ7JuOMt4GE9607Qin" };
            var seedGenres = new[] { "genre1", "genre2" };
            Action<ITuneableTrackAttributesBuilder> builderAction = a => a
                .Acousticness(v => v.Min(0.33f).Max(3.33f).Target(1.33f))
                .Danceability(v => v.Min(3.66f).Max(5.77f).Target(8.33f))
                .Duration(v => v.Min(TimeSpan.FromMilliseconds(23)).Max(TimeSpan.FromMilliseconds(67)).Target(TimeSpan.FromMilliseconds(88)))
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
                .Valence(v => v.Min(11f).Max(33f).Target(88f));

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"recommendations")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["limit"] = limit.ToString(CultureInfo.InvariantCulture),
                    ["market"] = market,
                    ["seed_artists"] = string.Join(",", seedArtists),
                    ["seed_tracks"] = string.Join(",", seedTracks),
                    ["seed_genres"] = string.Join(",", seedGenres),
                    ["min_acousticness"] = "0.33",
                    ["max_acousticness"] = "3.33",
                    ["target_acousticness"] = "1.33",
                    ["min_danceability"] = "3.66",
                    ["max_danceability"] = "5.77",
                    ["target_danceability"] = "8.33",
                    ["min_duration_ms"] = "23",
                    ["max_duration_ms"] = "67",
                    ["target_duration_ms"] = "88",
                    ["min_energy"] = "9.22",
                    ["max_energy"] = "10.44",
                    ["target_energy"] = "6.66",
                    ["min_instrumentalness"] = "4.55",
                    ["max_instrumentalness"] = "95.22",
                    ["target_instrumentalness"] = "1.11",
                    ["min_key"] = "2",
                    ["max_key"] = "9",
                    ["target_key"] = "5",
                    ["min_liveness"] = "7.77",
                    ["max_liveness"] = "9.99",
                    ["target_liveness"] = "10.11",
                    ["min_loudness"] = "55.33",
                    ["max_loudness"] = "8.77",
                    ["target_loudness"] = "44.44",
                    ["min_mode"] = "11",
                    ["max_mode"] = "22",
                    ["target_mode"] = "66",
                    ["min_popularity"] = "10",
                    ["max_popularity"] = "20",
                    ["target_popularity"] = "30",
                    ["min_speechiness"] = "3.01",
                    ["max_speechiness"] = "11.08",
                    ["target_speechiness"] = "22.4",
                    ["min_tempo"] = "5.66",
                    ["max_tempo"] = "8.55",
                    ["target_tempo"] = "11.11",
                    ["min_time_signature"] = "55",
                    ["max_time_signature"] = "33",
                    ["target_time_signature"] = "66",
                    ["min_valence"] = "11",
                    ["max_valence"] = "33",
                    ["target_valence"] = "88"
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Browse.Recommendations.GetAsync(
                limit: limit,
                market: market,
                seedArtists: seedArtists,
                seedTracks: seedTracks,
                seedGenres: seedGenres,
                buildTunableTrackAttributes: builderAction);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }
    }
}
