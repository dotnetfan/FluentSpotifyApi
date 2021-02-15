using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.UnitTests.Builder.Users
{
    [TestClass]
    public class UserTests : TestsBase
    {
        [TestMethod]
        public async Task ShouldGetUser()
        {
            // Arrange
            const string id = "wizzler";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"users/{id}")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Users(id).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }
    }
}
