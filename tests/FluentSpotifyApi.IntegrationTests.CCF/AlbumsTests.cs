using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.IntegrationTests.CCF
{
    [TestClass]    
    public class AlbumsTests : TestBase
    {
        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetAlbumByIdAsync()
        {
            // Arrange
            const string id = "6akEvsycLGftJxYudPjmqK";

            // Act
            var result = await this.Client.Album(id).GetAsync();

            // Assert
            result.Id.Should().Be(id);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetAlbumsByIdsAsync()
        {
            // Arrange
            var ids = new[] { "41MnTivkwTO3UUJ8DrqEJJ", "6JWc4iAiJ9FjyK0B59ABb4", "6UXCm6bOO4gFlDQZV5yL37" };

            // Act
            var result = await this.Client.Albums(ids).GetAsync();

            // Assert
            result.Items.Select(item => item.Id).Should().Equal(ids);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetAlbumTracksByIdAsync()
        {
            // Arrange
            const string id = "6akEvsycLGftJxYudPjmqK";
            const int limit = 2;

            // Act
            var result = await this.Client.Album(id).Tracks.GetAsync(limit: limit);

            // Assert
            result.Items.Should().HaveCount(limit);
        }
    }
}