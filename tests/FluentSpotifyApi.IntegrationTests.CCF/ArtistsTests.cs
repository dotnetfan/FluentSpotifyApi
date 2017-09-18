using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Builder.Artists;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.IntegrationTests.CCF
{
    [TestClass]
    public class ArtistsTests : TestBase
    {
        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetArtistByIdAsync()
        {
            // Arrange
            const string id = "0OdUWJ0sBjDrqHygGUXeCF";

            // Act
            var result = await this.Client.Artist(id).GetAsync();

            // Assert
            result.Id.Should().Be(id);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetArtistsByIdsAsync()
        {
            // Arrange
            var ids = new[] { "0oSGxfWSnnOXhD2fKuz2Gy", "3dBVyJ7JuOMt4GE9607Qin" };

            // Act
            var result = await this.Client.Artists(ids).GetAsync();

            // Assert
            result.Items.Select(item => item.Id).Should().Equal(ids);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetArtistAlbumsByIdAsync()
        {
            // Arrange
            const string id = "1vCWHaC5f2uS3yhpwWbIA6";
            const int limit = 2;

            // Act
            var result = await this.Client.Artist(id).Albums.GetAsync(albumTypes: new[] { AlbumType.Single }, limit: limit, market: "ES");

            // Assert
            result.Items.Should().HaveCount(limit);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetArtistTopTracksByIdAsync()
        {
            // Arrange
            const string id = "43ZHCT0cAZBISjO8DG9PnE";

            // Act
            var result = await this.Client.Artist(id).TopTracks.GetAsync(country: "SE");

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetArtistRelatedArtistsByIdAsync()
        {
            // Arrange
            const string id = "43ZHCT0cAZBISjO8DG9PnE";

            // Act
            var result = await this.Client.Artist(id).RelatedArtists.GetAsync();

            // Assert
            result.Should().NotBeNull();
        }
    }
}
