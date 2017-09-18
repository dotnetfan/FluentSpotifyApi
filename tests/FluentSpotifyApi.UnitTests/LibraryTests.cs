using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace FluentSpotifyApi.UnitTests
{
    [TestClass]
    [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1000:KeywordsMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
    public class LibraryTests : TestBase
    {
        [TestMethod]
        public async Task ShouldSaveTracksAsync()
        {
            // Arrange
            const int resultSize = 135;
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();

            var mockResults = this.MockPut();

            // Act
            await this.Client.Me.Library.Tracks(ids).SaveAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().RequestPayload.Value<JArray>("ids").Cast<JValue>().Select(item => item.Value).ToArray().Should().Equal(ids.ToArray());
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "tracks" });
        }

        [TestMethod]
        public async Task ShouldGetSavedTracksAsync()
        {
            // Arrange
            const int limit = 20;
            const int offset = 10;
            const string market = "BS";

            var mockResults = this.MockGet<Page<SavedTrack>>();

            // Act
            var result = await this.Client.Me.Library.Tracks().GetAsync(limit: limit, offset: offset, market: market);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", limit), ("offset", offset), ("market", market) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "tracks" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetSavedTracksWithDefaultsAsync()
        {
            // Arrange
            var mockResults = this.MockGet<Page<SavedTrack>>();

            // Act
            var result = await this.Client.Me.Library.Tracks().GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", 20), ("offset", 0) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "tracks" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldRemoveTracksAsync()
        {
            // Arrange
            const int resultSize = 135;
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();

            var mockResults = this.MockDelete();

            // Act
            await this.Client.Me.Library.Tracks(ids).RemoveAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().RequestPayload.Value<JArray>("ids").Cast<JValue>().Select(item => item.Value).ToArray().Should().Equal(ids.ToArray());
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "tracks" });
        }

        [TestMethod]
        public async Task ShouldCheckSavedTracksAsync()
        {
            // Arrange
            const int resultSize = 135;
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();
            var expectedResult = Enumerable.Range(0, resultSize).Select(item => item % 2 == 0 ? true : false).ToList();

            var mockResults = this.MockGet<bool[]>(i => new List<bool>(expectedResult).ToArray());

            // Act
            var result = await this.Client.Me.Library.Tracks(ids).CheckAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("ids", string.Join(",", ids)) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "tracks", "contains" });
            result.Should().Equal(expectedResult);
        }

        [TestMethod]
        public async Task ShouldSaveAlbumsAsync()
        {
            // Arrange
            const int resultSize = 135;
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();

            var mockResults = this.MockPut();

            // Act
            await this.Client.Me.Library.Albums(ids).SaveAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().RequestPayload.Value<JArray>("ids").Cast<JValue>().Select(item => item.Value).ToArray().Should().Equal(ids.ToArray());
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "albums" });
        }

        [TestMethod]
        public async Task ShouldGetSavedAlbumsAsync()
        {
            // Arrange
            const int limit = 20;
            const int offset = 10;
            const string market = "BS";

            var mockResults = this.MockGet<Page<SavedAlbum>>();

            // Act
            var result = await this.Client.Me.Library.Albums().GetAsync(limit: limit, offset: offset, market: market);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", limit), ("offset", offset), ("market", market) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "albums" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetSavedAlbumsWithDefaulesAsync()
        {
            // Arrange
            var mockResults = this.MockGet<Page<SavedAlbum>>();

            // Act
            var result = await this.Client.Me.Library.Albums().GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", 20), ("offset", 0) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "albums" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldRemoveAlbumsAsync()
        {
            // Arrange
            const int resultSize = 135;
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();

            var mockResults = this.MockDelete();

            // Act
            await this.Client.Me.Library.Albums(ids).RemoveAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().RequestPayload.Value<JArray>("ids").Cast<JValue>().Select(item => item.Value).ToArray().Should().Equal(ids.ToArray());
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "albums" });
        }

        [TestMethod]
        public async Task ShouldCheckSavedAlbumsAsync()
        {
            // Arrange
            const int resultSize = 135;
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();
            var expectedResult = Enumerable.Range(0, resultSize).Select(item => item % 2 == 0 ? true : false).ToList();

            var mockResults = this.MockGet<bool[]>(i => new List<bool>(expectedResult).ToArray());

            // Act
            var result = await this.Client.Me.Library.Albums(ids).CheckAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("ids", string.Join(",", ids)) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "albums", "contains" });
            result.Should().Equal(expectedResult);
        }
    }
}