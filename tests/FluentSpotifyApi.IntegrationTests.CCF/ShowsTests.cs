using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.IntegrationTests.CCF
{
    [TestClass]
    public class ShowsTests : TestsBase
    {
        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetShow()
        {
            // Arrange
            const string id = "38bS44xjbVVZ3No3ByF1dJ";

            // Act
            var result = await this.Client.Shows(id).GetAsync(market: "ES");

            // Assert
            result.Id.Should().Be(id);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetShows()
        {
            // Arrange
            var ids = new[] { "5CfCWKI5pZ28U0uOzXkDHe", "5as3aKmN2k11yfDDDSrvaZ" };

            // Act
            var result = await this.Client.Shows(ids).GetAsync(market: "ES");

            // Assert
            result.Items.Select(item => item.Id).Should().Equal(ids);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetShowEpisodes()
        {
            // Arrange
            const string id = "38bS44xjbVVZ3No3ByF1dJ";
            const int limit = 2;

            // Act
            var result = await this.Client.Shows(id).Episodes.GetAsync(market: "ES", limit: limit);

            // Assert
            result.Items.Should().HaveCount(limit);
        }
    }
}