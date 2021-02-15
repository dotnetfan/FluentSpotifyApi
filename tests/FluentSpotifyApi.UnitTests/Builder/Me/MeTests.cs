using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.UnitTests.Builder.Me
{
    [TestClass]
    public class MeTests : TestsBase
    {
        [TestMethod]
        public async Task ShouldGetCurrentUser()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Me.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }
    }
}
