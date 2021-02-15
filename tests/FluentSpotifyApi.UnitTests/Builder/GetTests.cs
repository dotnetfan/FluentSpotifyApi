using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.UnitTests.Builder
{
    [TestClass]
    public class GetTests : TestsBase
    {
        [TestMethod]
        public async Task ShouldGet()
        {
            // Arrange
            var uri = new Uri("http://localhost/test/123?a=b");

            this.MockHttp
                .Expect(HttpMethod.Get, uri.AbsoluteUri)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.GetAsync<object>(uri);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();

            result.Should().NotBeNull();
        }
    }
}
