using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Builder.Artists;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.UnitTests
{
    [TestClass]
    [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1000:KeywordsMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
    public class ArtistsTests : TestBase
    {
        [TestMethod]
        public async Task ShouldGetArtistByIdAsync()
        {
            // Arrange
            const string id = "0OdUWJ0sBjDrqHygGUXeCF";

            var mockResults = this.MockGet<FullArtist>();

            // Act
            var result = await this.Client.Artist(id).GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().RouteValues.Should().Equal(new[] { "artists", id });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetArtistsByIdsAsync()
        {
            // Arrange
            const int resultSize = 115;
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();
            var expectedResult = Enumerable.Range(0, resultSize).Select(item => new FullArtist()).ToList();

            var mockResults = this.MockGet<FullArtistsMessage>(i => new FullArtistsMessage { Items = expectedResult.ToArray() });

            // Act
            var result = await this.Client.Artists(ids).GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("ids", string.Join(",", ids)) });
            mockResults.First().RouteValues.Should().Equal(new[] { "artists" });
            result.Items.Should().Equal(expectedResult);
        }

        [TestMethod]
        public async Task ShouldGetArtistAlbumsByIdAsync()
        {
            // Arrange
            const string id = "1vCWHaC5f2uS3yhpwWbIA6";
            const int limit = 20;
            const int offset = 10;
            const string market = "ES";

            var albumTypes = new[] { AlbumType.Album, AlbumType.Single, AlbumType.Compilation };
            var dynamicAlbumTypes = new[] { "appears_no" };

            var mockResults = this.MockGet<Page<SimpleAlbum>>();

            // Act
            var result = await this.Client.Artist(id).Albums.GetAsync(albumTypes: albumTypes, dynamicAlbumTypes: dynamicAlbumTypes, market: market, limit: limit, offset: offset);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("market", market), ("limit", limit), ("offset", offset), ("album_type", "album,single,compilation,appears_no") });
            mockResults.First().RouteValues.Should().Equal(new[] { "artists", id, "albums" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetArtistAlbumsByIdWithDefaultsAsync()
        {
            // Arrange
            const string id = "1vCWHaC5f2uS3yhpwWbIA6";

            var albumTypes = new[] { AlbumType.Album, AlbumType.Single, AlbumType.Compilation };
            var dynamicAlbumTypes = new[] { "appears_no" };

            var mockResults = this.MockGet<Page<SimpleAlbum>>();

            // Act
            var result = await this.Client.Artist(id).Albums.GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", 20), ("offset", 0) });
            mockResults.First().RouteValues.Should().Equal(new[] { "artists", id, "albums" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetArtistTopTracksByIdAsync()
        {
            // Arrange
            const string id = "43ZHCT0cAZBISjO8DG9PnE";
            const string country = "SE";

            var mockResults = this.MockGet<FullTracksMessage>();

            // Act
            var result = await this.Client.Artist(id).TopTracks.GetAsync(country: country);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("country", country) });
            mockResults.First().RouteValues.Should().Equal(new[] { "artists", id, "top-tracks" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetArtistRelatedArtistsByIdAsync()
        {
            // Arrange
            const string id = "43ZHCT0cAZBISjO8DG9PnE";

            var mockResults = this.MockGet<FullArtistsMessage>();

            // Act
            var result = await this.Client.Artist(id).RelatedArtists.GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "artists", id, "related-artists" });
            result.Should().BeSameAs(mockResults.First().Result);
        }
    }
}
