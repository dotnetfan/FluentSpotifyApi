using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.UnitTests.Builder.Me
{
    [TestClass]
    public class LibraryTests : TestsBase
    {
        [TestMethod]
        public async Task ShouldGetSavedAlbums()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/albums")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Me.Library.Albums.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetSavedAlbumsWithAllParams()
        {
            // Arrange
            const int limit = 20;
            const int offset = 10;
            const string market = "BS";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/albums")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["limit"] = limit.ToString(CultureInfo.InvariantCulture),
                    ["offset"] = offset.ToString(CultureInfo.InvariantCulture),
                    ["market"] = market
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Me.Library.Albums.GetAsync(limit, offset, market);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldSaveAlbums()
        {
            // Arrange
            var ids = new[] { "41MnTivkwTO3UUJ8DrqEJJ", "6JWc4iAiJ9FjyK0B59ABb4" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/albums")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 1 &&
                    j.TryGetProperty("ids", out var array) && array.EnumerateArray().ToArray().Select(x => x.GetString()).SequenceEqual(ids))
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Library.Albums.SaveAsync(ids);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldRemoveAlbums()
        {
            // Arrange
            var ids = new[] { "41MnTivkwTO3UUJ8DrqEJJ", "6JWc4iAiJ9FjyK0B59ABb4" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Delete, $"me/albums")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 1 &&
                    j.TryGetProperty("ids", out var array) && array.EnumerateArray().ToArray().Select(x => x.GetString()).SequenceEqual(ids))
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Library.Albums.RemoveAsync(ids);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldCheckSavedAlbums()
        {
            // Arrange
            var ids = new[] { "41MnTivkwTO3UUJ8DrqEJJ", "6JWc4iAiJ9FjyK0B59ABb4" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/albums/contains")
                .WithExactQueryString(new Dictionary<string, string> { ["ids"] = string.Join(",", ids) })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "[]");

            // Act
            var result = await this.Client.Me.Library.Albums.CheckAsync(ids);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetSavedTracks()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/tracks")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Me.Library.Tracks.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetSavedTracksWithAllParams()
        {
            // Arrange
            const int limit = 20;
            const int offset = 10;
            const string market = "BS";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/tracks")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["limit"] = limit.ToString(CultureInfo.InvariantCulture),
                    ["offset"] = offset.ToString(CultureInfo.InvariantCulture),
                    ["market"] = market
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Me.Library.Tracks.GetAsync(limit, offset, market);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldSaveTracks()
        {
            // Arrange
            var ids = new[] { "3n3Ppam7vgaVa1iaRUc9Lp", "3twNvmDtFQtAd5gMKedhLD" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/tracks")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 1 &&
                    j.TryGetProperty("ids", out var array) && array.EnumerateArray().ToArray().Select(x => x.GetString()).SequenceEqual(ids))
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Library.Tracks.SaveAsync(ids);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldRemoveTracks()
        {
            // Arrange
            var ids = new[] { "3n3Ppam7vgaVa1iaRUc9Lp", "3twNvmDtFQtAd5gMKedhLD" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Delete, $"me/tracks")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 1 &&
                    j.TryGetProperty("ids", out var array) && array.EnumerateArray().ToArray().Select(x => x.GetString()).SequenceEqual(ids))
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Library.Tracks.RemoveAsync(ids);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldCheckSavedTracks()
        {
            // Arrange
            var ids = new[] { "3n3Ppam7vgaVa1iaRUc9Lp", "3twNvmDtFQtAd5gMKedhLD" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/tracks/contains")
                .WithExactQueryString(new Dictionary<string, string> { ["ids"] = string.Join(",", ids) })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "[]");

            // Act
            var result = await this.Client.Me.Library.Tracks.CheckAsync(ids);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetSavedShows()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/shows")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            await this.Client.Me.Library.Shows.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldGetSavedShowsWithAllParams()
        {
            // Arrange
            const int limit = 20;
            const int offset = 10;
            const string market = "BS";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/shows")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["limit"] = limit.ToString(CultureInfo.InvariantCulture),
                    ["offset"] = offset.ToString(CultureInfo.InvariantCulture),
                    ["market"] = market
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            await this.Client.Me.Library.Shows.GetAsync(limit, offset, market);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldSaveShows()
        {
            // Arrange
            var ids = new[] { "5CfCWKI5pZ28U0uOzXkDHe", "5as3aKmN2k11yfDDDSrvaZ" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/shows")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 1 &&
                    j.TryGetProperty("ids", out var array) && array.EnumerateArray().ToArray().Select(x => x.GetString()).SequenceEqual(ids))
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Library.Shows.SaveAsync(ids);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldRemoveShows()
        {
            // Arrange
            var ids = new[] { "5CfCWKI5pZ28U0uOzXkDHe", "5as3aKmN2k11yfDDDSrvaZ" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Delete, $"me/shows")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 1 &&
                    j.TryGetProperty("ids", out var array) && array.EnumerateArray().ToArray().Select(x => x.GetString()).SequenceEqual(ids))
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Library.Shows.RemoveAsync(ids);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldCheckSavedShows()
        {
            // Arrange
            var ids = new[] { "5CfCWKI5pZ28U0uOzXkDHe", "5as3aKmN2k11yfDDDSrvaZ" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/shows/contains")
                .WithExactQueryString(new Dictionary<string, string> { ["ids"] = string.Join(",", ids) })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "[]");

            // Act
            var result = await this.Client.Me.Library.Shows.CheckAsync(ids);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }
    }
}