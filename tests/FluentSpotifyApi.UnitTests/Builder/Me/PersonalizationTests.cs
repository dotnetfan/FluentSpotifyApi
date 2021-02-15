using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Builder.Me.Personalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.UnitTests.Builder.Me
{
    [TestClass]
    public class PersonalizationTests : TestsBase
    {
        [TestMethod]
        public async Task ShouldGetTopArtists()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/top/artists")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Me.Personalization.TopArtists.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetTopArtistsWithAllParams()
        {
            // Arrange
            const TimeRange timeRange = TimeRange.ShortTerm;
            const int limit = 30;
            const int offset = 40;

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/top/artists")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["time_range"] = "short_term",
                    ["limit"] = limit.ToString(CultureInfo.InvariantCulture),
                    ["offset"] = offset.ToString(CultureInfo.InvariantCulture),
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Me.Personalization.TopArtists.GetAsync(timeRange: timeRange, limit: limit, offset: offset);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetTopTracks()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/top/tracks")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Me.Personalization.TopTracks.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetTopTracksWithAllParams()
        {
            // Arrange
            const TimeRange timeRange = TimeRange.ShortTerm;
            const int limit = 30;
            const int offset = 40;

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/top/tracks")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["time_range"] = "short_term",
                    ["limit"] = limit.ToString(CultureInfo.InvariantCulture),
                    ["offset"] = offset.ToString(CultureInfo.InvariantCulture)
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Me.Personalization.TopTracks.GetAsync(timeRange: timeRange, limit: limit, offset: offset);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }
    }
}
