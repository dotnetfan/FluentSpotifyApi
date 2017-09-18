using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.IntegrationTests.CCF
{
    [TestClass]
    public class TracksTests : TestBase
    {
        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetTrackByIdAsync()
        {
            // Arrange
            const string id = "3n3Ppam7vgaVa1iaRUc9Lp";

            // Act
            var result = await this.Client.Track(id).GetAsync();

            // Assert
            result.Id.Should().Be(id);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetTracksByIdsAsync()
        {
            // Arrange
            var ids = new[] { "3n3Ppam7vgaVa1iaRUc9Lp", "3twNvmDtFQtAd5gMKedhLD" };

            // Act
            var result = await this.Client.Tracks(ids).GetAsync();

            // Assert
            result.Items.Select(item => item.Id).Should().Equal(ids);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetTrackAudioAnalysisByIdAsync()
        {
            // Arrange
            const string id = "3JIxjvbbDrA9ztYlNcp3yL";

            // Act
            var result = await this.Client.Track(id).AudioAnalysis.GetAsync();

            // Assert
            result.Track.Should().NotBeNull();
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetTrackAudioFeaturesByIdAsync()
        {
            // Arrange
            const string id = "06AKEBrKUckW0KREUWRnvT";

            // Act
            var result = await this.Client.Track(id).AudioFeatures.GetAsync();

            // Assert
            result.Id.Should().Be(id);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetTracksAudioFeaturesByIdsAsync()
        {
            // Arrange
            var ids = new[] { "4JpKVNYnVcJ8tuMKjAj50A", "2NRANZE9UCmPAS5XVbXL40", "24JygzOLM0EmRQeGtFcIcG" };

            // Act
            var result = await this.Client.Tracks(ids).AudioFeatures.GetAsync();

            // Assert
            result.Items.Select(item => item.Id).Should().Equal(ids);
        }
    }
}
