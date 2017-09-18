using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.IntegrationTests.CCF
{
    [TestClass]
    public class BrowseTests : TestBase
    {
        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetFeaturedPlaylistsAsync()
        {
            // Arrange
            const int limit = 2;

            // Act
            var result = await this.Client.Browse.FeaturedPlaylists.GetAsync(limit: limit);

            // Assert
            result.Playlists.Items.Should().HaveCount(limit);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetNewReleasesAsync()
        {
            // Arrange
            const int limit = 2;

            // Act
            var result = await this.Client.Browse.NewReleases.GetAsync(limit: limit);

            // Assert
            result.Albums.Items.Should().HaveCount(limit);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetCategoriesAsync()
        {
            // Arrange
            const int limit = 2;

            // Act
            var result = await this.Client.Browse.Categories.GetAsync(limit: limit);

            // Assert
            result.Page.Items.Should().HaveCount(limit);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetCategoryAsync()
        {
            // Arrange
            const string categoryId = "party";

            // Act
            var result = await this.Client.Browse.Category(categoryId).GetAsync();

            // Assert
            result.Id.Should().Be(categoryId);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetCategoryPlaylistsAsync()
        {
            // Arrange
            const int limit = 2;

            // Act
            var result = await this.Client.Browse.Category("party").Playlists.GetAsync(country: "BR", limit: limit);

            // Assert
            result.Page.Items.Should().HaveCount(limit);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetRecommedationsAsync()
        {
            // Arrnage + Act
            var result = await this.Client.Browse.Recommendations.GetAsync(
                market: "US", 
                seedArtists: new[] { "4NHQUGzhtTLFvgF5SZesLK" }, 
                seedTracks: new[] { "0c6xIDDpzE81m2q797ordA" }, 
                buildTunableTrackAttributes: a => a.Energy(v => v.Min(0.5f)).Popularity(v => v.Min(50)));

            // Assert
            result.Should().NotBeNull();
        }
    }
}
