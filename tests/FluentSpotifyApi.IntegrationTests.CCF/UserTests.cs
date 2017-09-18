using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.IntegrationTests.CCF
{
    [TestClass]
    public class UserTests : TestBase
    {
        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetUserAsync()
        {
            // Arrange
            const string id = "wizzler";

            // Act
            var result = await this.Client.User(id).GetAsync();

            // Assert
            result.Id.Should().Be(id);
        }
    }
}
