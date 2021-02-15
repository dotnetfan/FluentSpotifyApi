using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.UnitTests.Builder
{
    [TestClass]
    public class TracksTests : TestsBase
    {
        [TestMethod]
        public async Task ShouldGetTrack()
        {
            // Arrange
            const string id = "3n3Ppam7vgaVa1iaRUc9Lp";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"tracks/{id}")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Tracks(id).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetTrackWithAllParams()
        {
            // Arrange
            const string id = "3n3Ppam7vgaVa1iaRUc9Lp";
            const string market = "AU";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"tracks/{id}")
                .WithExactQueryString(new Dictionary<string, string> { ["market"] = market })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Tracks(id).GetAsync(market: market);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetTracks()
        {
            // Arrange
            var ids = new[] { "3n3Ppam7vgaVa1iaRUc9Lp", "3twNvmDtFQtAd5gMKedhLD" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, "tracks")
                .WithExactQueryString(new Dictionary<string, string> { ["ids"] = string.Join(",", ids) })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Tracks(ids).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetTracksWithAllParams()
        {
            // Arrange
            var ids = new[] { "3n3Ppam7vgaVa1iaRUc9Lp", "3twNvmDtFQtAd5gMKedhLD" };
            const string market = "AU";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, "tracks")
                .WithExactQueryString(new Dictionary<string, string> { ["ids"] = string.Join(",", ids), ["market"] = market })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Tracks(ids).GetAsync(market: market);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetTrackAudioAnalysis()
        {
            // Arrange
            const string id = "3n3Ppam7vgaVa1iaRUc9Lp";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"audio-analysis/{id}")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Tracks(id).AudioAnalysis.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetTrackAudioFeaturesAsync()
        {
            // Arrange
            const string id = "3n3Ppam7vgaVa1iaRUc9Lp";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"audio-features/{id}")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Tracks(id).AudioFeatures.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetTracksAudioFeaturesAsync()
        {
            // Arrange
            var ids = new[] { "3n3Ppam7vgaVa1iaRUc9Lp", "3twNvmDtFQtAd5gMKedhLD" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"audio-features")
                .WithExactQueryString(new Dictionary<string, string> { ["ids"] = string.Join(",", ids) })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Tracks(ids).AudioFeatures.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }
    }
}
