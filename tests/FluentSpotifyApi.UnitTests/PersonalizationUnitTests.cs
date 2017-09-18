using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Builder.Me.Personalization.Top;
using FluentSpotifyApi.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.UnitTests
{
    [TestClass]
    [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1000:KeywordsMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
    public class PersonalizationUnitTests : TestBase
    {
        [TestMethod]
        public async Task ShouldGetTopArtistsAsync()
        {
            // Arrange
            const int limit = 20;
            const int offset = 10;
            const TimeRange timeRange = TimeRange.MediumTerm;

            var mockResults = this.MockGet<Page<FullArtist>>();

            // Act
            var result = await this.Client.Me.Personalization.TopArtists.GetAsync(limit: limit, offset: offset, timeRange: timeRange);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", limit), ("offset", offset), ("time_range", "medium_term") });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "top", "artists" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetTopArtistsWithDefaultsAsync()
        {
            // Arrange
            var mockResults = this.MockGet<Page<FullArtist>>();

            // Act
            var result = await this.Client.Me.Personalization.TopArtists.GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", 20), ("offset", 0), ("time_range", "medium_term") });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "top", "artists" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetTopTracksAsync()
        {
            // Arrange
            const int limit = 20;
            const int offset = 10;
            const TimeRange timeRange = TimeRange.ShortTerm;

            var mockResults = this.MockGet<Page<FullTrack>>();

            // Act
            var result = await this.Client.Me.Personalization.TopTracks.GetAsync(limit: limit, offset: offset, timeRange: timeRange);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", limit), ("offset", offset), ("time_range", "short_term") });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "top", "tracks" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetTopTracksWithDefaultsAsync()
        {
            // Arrange
            var mockResults = this.MockGet<Page<FullTrack>>();

            // Act
            var result = await this.Client.Me.Personalization.TopTracks.GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", 20), ("offset", 0), ("time_range", "medium_term") });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "top", "tracks" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetRecentlyPlayedTracksAsync()
        {
            // Arrange
            const int limit = 20;
            const long after = 300;
            const long before = 100;

            var mockResults = this.MockGet<CursorBasedPage<PlayHistory>>();

            // Act
            var result = await this.Client.Me.Personalization.RecentlyPlayedTracks.GetAsync(limit: limit, after: after, before: before);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", limit), ("after", after), ("before", before) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "recently-played" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetRecentlyPlayedTracksWithDefaultsAsync()
        {
            // Arrange
            var mockResults = this.MockGet<CursorBasedPage<PlayHistory>>();

            // Act
            var result = await this.Client.Me.Personalization.RecentlyPlayedTracks.GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", 20) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "recently-played" });
            result.Should().BeSameAs(mockResults.First().Result);
        }
    }
}
