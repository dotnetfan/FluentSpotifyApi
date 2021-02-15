using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.IntegrationTests.CCF
{
    [TestClass]
    public class EpisodesTests : TestsBase
    {
        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetEpisode()
        {
            // Arrange
            const string id = "512ojhOuo1ktJprKbVcKyQ";

            // Act
            var result = await this.Client.Episodes(id).GetAsync(market: "ES");

            // Assert
            result.Id.Should().Be(id);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetEpisodes()
        {
            // Arrange
            var ids = new[] { "77o6BIVlYM3msb4MMIL1jH", "0Q86acNRm6V9GYx55SXKwf" };

            // Act
            var result = await this.Client.Episodes(ids).GetAsync(market: "ES");

            // Assert
            result.Items.Select(item => item.Id).Should().Equal(ids);
        }
    }
}