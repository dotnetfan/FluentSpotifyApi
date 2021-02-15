using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Builder.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.IntegrationTests.CCF
{
    [TestClass]
    public class SearchTests : TestsBase
    {
        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldSearchAlbums()
        {
            // Arrange + Act
            var result = await this.Client.Search.Albums.Matching(f => f.Any.Contains("Metallica")).GetAsync();

            // Assert
            result.Page.Items.Should().NotBeEmpty();
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldSearchArtists()
        {
            // Arrange + Act
            var result = await this.Client.Search.Artists.Matching(f => f.Any.Contains("Metallica")).GetAsync();

            // Assert
            result.Page.Items.Should().NotBeEmpty();
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldSearchPlaylists()
        {
            // Arrange + Act
            var result = await this.Client.Search.Playlists.Matching(f => f.Any.Contains("Metallica")).GetAsync();

            // Assert
            result.Page.Items.Should().NotBeEmpty();
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldSearchTracks()
        {
            // Arrange + Act
            var result = await this.Client.Search.Tracks.Matching(f => f.Any.Contains("Metallica")).GetAsync();

            // Assert
            result.Page.Items.Should().NotBeEmpty();
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldSearchShows()
        {
            // Arrange + Act
            var result = await this.Client.Search.Shows.Matching(f => f.Any.Contains("Metallica")).GetAsync();

            // Assert
            result.Page.Items.Should().NotBeEmpty();
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldSearchEpisodes()
        {
            // Arrange + Act
            var result = await this.Client.Search.Episodes.Matching(f => f.Any.Contains("Metallica")).GetAsync();

            // Assert
            result.Page.Items.Should().NotBeEmpty();
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldSearchAny()
        {
            // Arrange + Act
            var result = await this.Client.Search.Entities().Matching(f => f.Any.Contains("Metallica")).GetAsync();

            // Assert
            result.Albums.Items.Should().NotBeEmpty();
            result.Artists.Items.Should().NotBeEmpty();
            result.Playlists.Items.Should().NotBeEmpty();
            result.Tracks.Items.Should().NotBeEmpty();
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldSearchAlbumsAndArtists()
        {
            // Arrange + Act
            var result = await this.Client.Search.Entities(Entity.Artist, Entity.Album).Matching(f => f.Any.Contains("Metallica")).GetAsync();

            // Assert
            result.Albums.Items.Should().NotBeEmpty();
            result.Artists.Items.Should().NotBeEmpty();
        }
    }
}
