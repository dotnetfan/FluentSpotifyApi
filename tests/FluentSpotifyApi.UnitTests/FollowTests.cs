using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace FluentSpotifyApi.UnitTests
{
    [TestClass]
    [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1000:KeywordsMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
    public class FollowTests : TestBase
    {
        [TestMethod]
        public async Task ShouldGetFollowedArtistsAsync()
        {
            // Arrange
            const int limit = 20;
            const string after = "HSDUIHSUDHGSYGD";

            var mockResults = this.MockGet<FollowedArtists>();

            // Act
            var result = await this.Client.Me.Following.Artists().GetAsync(limit: limit, after: after);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", limit), ("after", after), ("type", "artist") });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "following" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetFollowedArtistsWithDefaultsAsync()
        {
            // Arrange
            var mockResults = this.MockGet<FollowedArtists>();

            // Act
            var result = await this.Client.Me.Following.Artists().GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", 20), ("type", "artist") });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "following" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldFollowArtistsAsync()
        {
            // Arrange
            const int resultSize = 135;
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();

            var mockResults = this.MockPut();

            // Act
            await this.Client.Me.Following.Artists(ids).FollowAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("type", "artist") });
            mockResults.First().RequestPayload.Value<JArray>("ids").Cast<JValue>().Select(item => item.Value).ToArray().Should().Equal(ids.ToArray());
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "following" });
        }

        [TestMethod]
        public async Task ShouldUnfollowArtistsAsync()
        {
            // Arrange
            const int resultSize = 135;
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();

            var mockResults = this.MockDelete();

            // Act
            await this.Client.Me.Following.Artists(ids).UnfollowAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("type", "artist") });
            mockResults.First().RequestPayload.Value<JArray>("ids").Cast<JValue>().Select(item => item.Value).ToArray().Should().Equal(ids.ToArray());
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "following" });
        }

        [TestMethod]
        public async Task ShouldCheckFollowedArtistsAsync()
        {
            // Arrange
            const int resultSize = 135;
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();
            var expectedResult = Enumerable.Range(0, resultSize).Select(item => item % 2 == 0 ? true : false).ToList();

            var mockResults = this.MockGet<bool[]>(i => new List<bool>(expectedResult).ToArray());

            // Act
            var result = await this.Client.Me.Following.Artists(ids).CheckAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("type", "artist"), ("ids", string.Join(",", ids)) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "following", "contains" });
            result.Should().Equal(expectedResult);
        }

        [TestMethod]
        public async Task ShouldFollowUsersAsync()
        {
            // Arrange
            const int resultSize = 135;
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();

            var mockResults = this.MockPut();

            // Act
            await this.Client.Me.Following.Users(ids).FollowAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("type", "user") });
            mockResults.First().RequestPayload.Value<JArray>("ids").Cast<JValue>().Select(item => item.Value).ToArray().Should().Equal(ids.ToArray());
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "following" });
        }

        [TestMethod]
        public async Task ShouldUnfollowUsersAsync()
        {
            // Arrange
            const int resultSize = 135;
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();

            var mockResults = this.MockDelete();

            // Act
            await this.Client.Me.Following.Users(ids).UnfollowAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("type", "user") });
            mockResults.First().RequestPayload.Value<JArray>("ids").Cast<JValue>().Select(item => item.Value).ToArray().Should().Equal(ids.ToArray());
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "following" });
        }

        [TestMethod]
        public async Task ShouldCheckFollowedUsersAsync()
        {
            // Arrange
            const int resultSize = 135;
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();
            var expectedResult = Enumerable.Range(0, resultSize).Select(item => item % 2 == 0 ? true : false).ToList();

            var mockResults = this.MockGet<bool[]>(i => new List<bool>(expectedResult).ToArray());

            // Act
            var result = await this.Client.Me.Following.Users(ids).CheckAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("type", "user"), ("ids", string.Join(",", ids)) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "following", "contains" });
            result.Should().Equal(expectedResult);
        }

        [TestMethod]
        public async Task ShouldFollowPlaylistAsync()
        {
            // Arrange
            const string ownerId = "testuser";
            const string playlistId = "JHUDGYSGYSGDG7872";
            const bool isPublic = true;

            var mockResults = this.MockPut();

            // Act
            await this.Client.Me.Following.Playlist(ownerId, playlistId).FollowAsync(isPublic: isPublic);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("public", isPublic) });
            mockResults.First().RouteValues.Should().Equal(new[] { "users", ownerId, "playlists", playlistId, "followers" });
        }

        [TestMethod]
        public async Task ShouldFollowPlaylistWithDefaultsAsync()
        {
            // Arrange
            const string ownerId = "testuser";
            const string playlistId = "JHUDGYSGYSGDG7872";

            var mockResults = this.MockPut();

            // Act
            await this.Client.Me.Following.Playlist(ownerId, playlistId).FollowAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("public", true) });
            mockResults.First().RouteValues.Should().Equal(new[] { "users", ownerId, "playlists", playlistId, "followers" });
        }

        [TestMethod]
        public async Task ShouldUnfollowPlaylistAsync()
        {
            // Arrange
            const string ownerId = "testuser";
            const string playlistId = "JHUDGYSGYSGDG7872";

            var mockResults = this.MockDelete();

            // Act
            await this.Client.Me.Following.Playlist(ownerId, playlistId).UnfollowAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "users", ownerId, "playlists", playlistId, "followers" });
        }

        [TestMethod]
        public async Task ShouldCheckFollowedPlaylistAsync()
        {
            // Arrange
            const string ownerId = "testuser";
            const string playlistId = "JHUDGYSGYSGDG7872";

            var mockResults = this.MockGet<bool[]>(i => new[] { true });

            // Act
            var result = await this.Client.Me.Following.Playlist(ownerId, playlistId).CheckAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("ids", UserId) });
            mockResults.First().RouteValues.Should().Equal(new[] { "users", ownerId, "playlists", playlistId, "followers", "contains" });

            result.Should().Be(mockResults.First().Result.First());
        }
    }
}
