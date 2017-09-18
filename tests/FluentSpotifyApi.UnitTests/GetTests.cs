using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.UnitTests
{
    [TestClass]
    public class GetTests : TestBase
    {
        [TestMethod]
        public async Task ShouldGetAsync()
        {
            // Arrange
            var uri = new Uri("http://localhost");

            var mockResults = this.MockGet<object>();

            // Act
            var result = await this.Client.GetAsync<object>(uri);

            // Assert
            mockResults.Should().HaveCount(1);
            result.Should().BeSameAs(mockResults.First().Result);
        }
    }
}
