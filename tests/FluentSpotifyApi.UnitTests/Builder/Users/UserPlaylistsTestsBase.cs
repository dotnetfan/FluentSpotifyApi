using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Builder.Users;
using FluentSpotifyApi.Model.Playlists;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.UnitTests.Builder.Users
{
    [TestClass]
    public abstract class UserPlaylistsTestsBase : TestsBase
    {
        [TestMethod]
        public async Task ShouldGetPlaylists()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"users/{TestsBase.UserId}/playlists")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var builder = this.GetPlaylistsBuilder();
            var result = await builder.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetPlaylistsWithAllParams()
        {
            // Arrange
            const int limit = 30;
            const int offset = 10;

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"users/{TestsBase.UserId}/playlists")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["limit"] = limit.ToString(CultureInfo.InvariantCulture),
                    ["offset"] = offset.ToString(CultureInfo.InvariantCulture),
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var builder = this.GetPlaylistsBuilder();
            var result = await builder.GetAsync(limit: limit, offset: offset);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldCreatePlaylist()
        {
            // Arrange
            var request = new CreatePlaylistRequest() { Name = "Test Playlist" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Post, $"users/{TestsBase.UserId}/playlists")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j => j.EnumerateObject().Count() == 1 && j.TryGetProperty("name", out var name) && name.GetString() == request.Name)
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var builder = this.GetPlaylistsBuilder();
            var result = await builder.CreateAsync(request);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldCreatePlaylistWithAllParams()
        {
            // Arrange
            var request = new CreatePlaylistRequest()
            {
                Name = "Test Playlist",
                Description = "Test Playlist Description",
                Collaborative = true,
                Public = true
            };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Post, $"users/{TestsBase.UserId}/playlists")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 4 &&
                    j.TryGetProperty("name", out var name) && name.GetString() == request.Name &&
                    j.TryGetProperty("description", out var description) && description.GetString() == request.Description &&
                    j.TryGetProperty("collaborative", out var collaborative) && collaborative.GetBoolean() == request.Collaborative &&
                    j.TryGetProperty("public", out var @public) && @public.GetBoolean() == request.Public)
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var builder = this.GetPlaylistsBuilder();
            var result = await builder.CreateAsync(request);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        protected abstract IUserPlaylistsBuilder GetPlaylistsBuilder();
    }
}