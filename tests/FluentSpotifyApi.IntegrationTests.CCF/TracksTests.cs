using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.IntegrationTests.CCF
{
    [TestClass]
    public class TracksTests : TestsBase
    {
        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetTrack()
        {
            // Arrange
            const string id = "3n3Ppam7vgaVa1iaRUc9Lp";

            // Act
            var result = await this.Client.Tracks(id).GetAsync();

            // Assert
            result.Id.Should().Be(id);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetTracks()
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
        public async Task ShouldGetTrackAudioAnalysis()
        {
            // Arrange
            const string id = "3JIxjvbbDrA9ztYlNcp3yL";

            // Act
            var result = await this.Client.Tracks(id).AudioAnalysis.GetAsync();

            // Assert
            result.Track.Should().NotBeNull();
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetTrackAudioFeatures()
        {
            // Arrange
            const string id = "06AKEBrKUckW0KREUWRnvT";

            // Act
            var result = await this.Client.Tracks(id).AudioFeatures.GetAsync();

            // Assert
            result.Id.Should().Be(id);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetTracksAudioFeatures()
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
