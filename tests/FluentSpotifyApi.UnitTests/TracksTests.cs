using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Audio;
using FluentSpotifyApi.Model.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.UnitTests
{
    [TestClass]
    [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1000:KeywordsMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
    public class TracksTests : TestBase
    {
        [TestMethod]
        public async Task ShouldGetTrackByIdAsync()
        {
            // Arrange
            const string id = "3n3Ppam7vgaVa1iaRUc9Lp";
            const string market = "AU";

            var mockResults = this.MockGet<FullTrack>();

            // Act
            var result = await this.Client.Track(id).GetAsync(market: market);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("market", market) });
            mockResults.First().RouteValues.Should().Equal(new[] { "tracks", id });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetTracksByIdsAsync()
        {
            // Arrange
            const int resultSize = 115;
            const string market = "AU";
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();
            var expectedResult = Enumerable.Range(0, resultSize).Select(item => new FullTrack()).ToList();

            var mockResults = this.MockGet<FullTracksMessage>(i => new FullTracksMessage { Items = expectedResult.ToArray() });

            // Act
            var result = await this.Client.Tracks(ids).GetAsync(market: market);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("ids", string.Join(",", ids)), ("market", market) });
            mockResults.First().RouteValues.Should().Equal(new[] { "tracks" });
            result.Items.Should().Equal(expectedResult);
        }

        [TestMethod]
        public async Task ShouldGetTrackAudioAnalysisByIdAsync()
        {
            // Arrange
            const string id = "3JIxjvbbDrA9ztYlNcp3yL";

            var mockResults = this.MockGet<AudioAnalysis>();

            // Act
            var result = await this.Client.Track(id).AudioAnalysis.GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "audio-analysis", id });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetTrackAudioFeaturesByIdAsync()
        {
            // Arrange
            const string id = "06AKEBrKUckW0KREUWRnvT";

            var mockResults = this.MockGet<AudioFeatures>();

            // Act
            var result = await this.Client.Track(id).AudioFeatures.GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "audio-features", id });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetTracksAudioFeaturesByIdsAsync()
        {
            // Arrange
            const int resultSize = 335;
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();
            var expectedResult = Enumerable.Range(0, resultSize).Select(item => new AudioFeatures()).ToList();

            var mockResults = this.MockGet<AudioFeaturesListMessage>(i => new AudioFeaturesListMessage { Items = expectedResult.ToArray() });

            // Act
            var result = await this.Client.Tracks(ids).AudioFeatures.GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("ids", string.Join(",", ids)) });
            mockResults.First().RouteValues.Should().Equal(new[] { "audio-features" });
            result.Items.Should().Equal(expectedResult);
        }
    }
}
