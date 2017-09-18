using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.UnitTests
{
    [TestClass]
    [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1000:KeywordsMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
    public class AlbumsTests : TestBase
    {
        [TestMethod]
        public async Task ShouldGetAlbumByIdAsync()
        {
            // Arrange
            const string id = "6akEvsycLGftJxYudPjmqK";
            const string market = "BS";

            var mockResults = this.MockGet<FullAlbum>();

            // Act
            var result = await this.Client.Album(id).GetAsync(market: market);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("market", market) });
            mockResults.First().RouteValues.Should().Equal(new[] { "albums", id });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetAlbumByIdWithDefaultsAsync()
        {
            // Arrange
            const string id = "6akEvsycLGftJxYudPjmqK";

            var mockResults = this.MockGet<FullAlbum>();

            // Act
            var result = await this.Client.Album(id).GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "albums", id });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetAlbumsByIdsAsync()
        {
            // Arrange
            const int resultSize = 46;
            const string market = "BS";
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();
            var expectedResult = Enumerable.Range(0, resultSize).Select(item => new FullAlbum()).ToList();

            var mockResults = this.MockGet<FullAlbumsMessage>(i => new FullAlbumsMessage { Items = expectedResult.ToArray() });

            // Act
            var result = await this.Client.Albums(ids).GetAsync(market: market);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("market", market), ("ids", string.Join(",", ids)) });
            mockResults.First().RouteValues.Should().Equal(new[] { "albums" });
            result.Items.Should().Equal(expectedResult);
        }

        [TestMethod]
        public async Task ShouldGetAlbumsByIdsWithDefaultsAsync()
        {
            // Arrange
            const int resultSize = 46;
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();
            var expectedResult = Enumerable.Range(0, resultSize).Select(item => new FullAlbum()).ToList();

            var mockResults = this.MockGet<FullAlbumsMessage>(i => new FullAlbumsMessage { Items = expectedResult.ToArray() });

            // Act
            var result = await this.Client.Albums(ids).GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("ids", string.Join(",", ids)) });
            mockResults.First().RouteValues.Should().Equal(new[] { "albums" });
            result.Items.Should().Equal(expectedResult);
        }

        [TestMethod]
        public async Task ShouldGetAlbumTracksByIdAsync()
        {
            // Arrange
            const int limit = 20;
            const int offset = 10;
            const string market = "BS";
            const string id = "6akEvsycLGftJxYudPjmqK";

            var mockResults = this.MockGet<Page<SimpleTrack>>();

            // Act
            var result = await this.Client.Album(id).Tracks.GetAsync(limit: limit, offset: offset, market: market);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", limit), ("offset", offset), ("market", market) });
            mockResults.First().RouteValues.Should().Equal(new[] { "albums", id, "tracks" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetAlbumTracksByIdWithDefaultsAsync()
        {
            // Arrange
            const string id = "6akEvsycLGftJxYudPjmqK";

            var mockResults = this.MockGet<Page<SimpleTrack>>();

            // Act
            var result = await this.Client.Album(id).Tracks.GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", 20), ("offset", 0) });
            mockResults.First().RouteValues.Should().Equal(new[] { "albums", id, "tracks" });
            result.Should().BeSameAs(mockResults.First().Result);
        }
    }
}
