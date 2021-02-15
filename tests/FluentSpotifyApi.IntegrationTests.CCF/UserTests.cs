using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.IntegrationTests.CCF
{
    [TestClass]
    public class UserTests : TestsBase
    {
        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetUser()
        {
            // Arrange
            const string id = "wizzler";

            // Act
            var result = await this.Client.Users(id).GetAsync();

            // Assert
            result.Id.Should().Be(id);
        }
    }
}
