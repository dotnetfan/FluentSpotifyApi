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
    public class FollowingTests : TestsBase
    {
        [TestMethod]
        public async Task ShouldGetFollowedArtists()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/following")
                .WithExactQueryString(new Dictionary<string, string> { ["type"] = "artist" })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Me.Following.Artists.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetFollowedArtistsWithAllParams()
        {
            // Arrange
            const string after = "0OdUWJ0sBjDrqHygGUXeCF";
            const int limit = 20;

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/following")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["type"] = "artist",
                    ["after"] = after,
                    ["limit"] = limit.ToString(CultureInfo.InvariantCulture)
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Me.Following.Artists.GetAsync(after, limit);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldFollowArtists()
        {
            // Arrange
            var ids = new[] { "0OdUWJ0sBjDrqHygGUXeCF", "0oSGxfWSnnOXhD2fKuz2Gy" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/following")
                .WithExactQueryString(new Dictionary<string, string> { ["type"] = "artist" })
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 1 &&
                    j.TryGetProperty("ids", out var array) && array.EnumerateArray().ToArray().Select(x => x.GetString()).SequenceEqual(ids))
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Following.Artists.FollowAsync(ids);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldUnfollowArtists()
        {
            // Arrange
            var ids = new[] { "0OdUWJ0sBjDrqHygGUXeCF", "0oSGxfWSnnOXhD2fKuz2Gy" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Delete, $"me/following")
                .WithExactQueryString(new Dictionary<string, string> { ["type"] = "artist" })
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 1 &&
                    j.TryGetProperty("ids", out var array) && array.EnumerateArray().ToArray().Select(x => x.GetString()).SequenceEqual(ids))
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Following.Artists.UnfollowAsync(ids);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldCheckFollowedArtists()
        {
            // Arrange
            var ids = new[] { "0OdUWJ0sBjDrqHygGUXeCF", "0oSGxfWSnnOXhD2fKuz2Gy" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/following/contains")
                .WithExactQueryString(new Dictionary<string, string> { ["type"] = "artist", ["ids"] = string.Join(",", ids) })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "[]");

            // Act
            var result = await this.Client.Me.Following.Artists.CheckAsync(ids);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldFollowUsers()
        {
            // Arrange
            var ids = new[] { "user1", "user2" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/following")
                .WithExactQueryString(new Dictionary<string, string> { ["type"] = "user" })
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 1 &&
                    j.TryGetProperty("ids", out var array) && array.EnumerateArray().ToArray().Select(x => x.GetString()).SequenceEqual(ids))
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Following.Users.FollowAsync(ids);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldUnfollowUsers()
        {
            // Arrange
            var ids = new[] { "user1", "user2" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Delete, $"me/following")
                .WithExactQueryString(new Dictionary<string, string> { ["type"] = "user" })
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 1 &&
                    j.TryGetProperty("ids", out var array) && array.EnumerateArray().ToArray().Select(x => x.GetString()).SequenceEqual(ids))
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Following.Users.UnfollowAsync(ids);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldCheckFollowedUsers()
        {
            // Arrange
            var ids = new[] { "user1", "user2" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/following/contains")
                .WithExactQueryString(new Dictionary<string, string> { ["type"] = "user", ["ids"] = string.Join(",", ids) })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "[]");

            // Act
            var result = await this.Client.Me.Following.Users.CheckAsync(ids);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldFollowPlaylist()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"playlists/{playlistId}/followers")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j => !j.EnumerateObject().Any())
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Following.Playlists.FollowAsync(playlistId);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldFollowPlaylistWithAllParams()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";
            const bool isPublic = true;

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"playlists/{playlistId}/followers")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 1 &&
                    j.TryGetProperty("public", out var isPublicProperty) && isPublicProperty.GetBoolean() == isPublic)
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Following.Playlists.FollowAsync(playlistId, isPublic);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldUnfollowPlaylist()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Delete, $"playlists/{playlistId}/followers")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Following.Playlists.UnfollowAsync(playlistId);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldCheckFollowedPlaylist()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"playlists/{playlistId}/followers/contains")
                .WithExactQueryString(new Dictionary<string, string> { ["ids"] = UserId })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "[ true ]");

            // Act
            var result = await this.Client.Me.Following.Playlists.CheckAsync(playlistId);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().BeTrue();
        }
    }
}
