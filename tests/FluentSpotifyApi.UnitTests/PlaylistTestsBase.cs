using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Builder.User.Playlists;
using FluentSpotifyApi.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace FluentSpotifyApi.UnitTests
{
    [TestClass]
    [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1000:KeywordsMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
    public abstract class PlaylistTestsBase : TestBase
    {
        protected abstract IPlaylistBuilder GetPlaylistBuilder(string id);

        protected abstract IPlaylistsBuilder GetPlaylistsBuilder();

        protected async Task ShouldGetPlaylistsAsync()
        {
            // Arrange
            const int limit = 20;
            const int offset = 10;

            var mockResults = this.MockGet<Page<SimplePlaylist>>();

            // Act
            var builder = this.GetPlaylistsBuilder();
            var result = await builder.GetAsync(limit: limit, offset: offset);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", limit), ("offset", offset) });
            mockResults.First().RouteValues.Should().Equal(new[] { "users", TestBase.UserId, "playlists" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        protected async Task ShouldGetPlaylistsWithDefaultsAsync()
        {
            // Arrange
            var mockResults = this.MockGet<Page<SimplePlaylist>>();

            // Act
            var builder = this.GetPlaylistsBuilder();
            var result = await builder.GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", 20), ("offset", 0) });
            mockResults.First().RouteValues.Should().Equal(new[] { "users", TestBase.UserId, "playlists" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        protected async Task ShouldGetPlaylistAsync()
        {
            // Arrange
            const string playlistId = "JIHUIH8943432hU";
            const string fields = "(description,tracks(items(track(name))))";
            const string market = "BR";

            var mockResults = this.MockGet<FullPlaylist>();

            // Act
            var builder = this.GetPlaylistBuilder(playlistId);
            var result = await builder.GetAsync(
                buildFields: fieldsBuilder => fieldsBuilder
                    .Include(p => p.Description)
                    .Include(p => p.Tracks.Items[0].Track.Name), 
                market: market);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("fields", fields), ("market", market) });
            mockResults.First().RouteValues.Should().Equal(new[] { "users", UserId, "playlists", playlistId });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        protected async Task ShouldGetPlaylistWithDefaultsAsync()
        {
            // Arrange
            const string playlistId = "JIHUIH8943432hU";
        
            var mockResults = this.MockGet<FullPlaylist>();

            // Act
            var builder = this.GetPlaylistBuilder(playlistId);
            var result = await builder.GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "users", UserId, "playlists", playlistId });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        protected async Task ShouldGetPlaylistTracksAsync()
        {
            // Arrange
            const string playlistId = "JIHUIH8943432hU";
            const string fields = "(items(added_at,added_by(!display_name)))";
            const int limit = 20;
            const int offset = 10;
            const string market = "BR";

            var mockResults = this.MockGet<Page<PlaylistTrack>>();

            // Act
            var builder = this.GetPlaylistBuilder(playlistId);
            var result = await builder.Tracks().GetAsync(
                buildFields: fieldsBuilder => fieldsBuilder
                    .Include(t => t.Items[0].AddedAt)
                    .Exclude(t => t.Items[0].AddedBy.DisplayName), 
                limit: limit, 
                offset: offset, 
                market: market);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("fields", fields), ("market", market), ("limit", limit), ("offset", offset) });
            mockResults.First().RouteValues.Should().Equal(new[] { "users", TestBase.UserId, "playlists", playlistId, "tracks" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        protected async Task ShouldGetPlaylistTracksWithDefaultsAsync()
        {
            // Arrange
            const string playlistId = "JIHUIH8943432hU";

            var mockResults = this.MockGet<Page<PlaylistTrack>>();

            // Act
            var builder = this.GetPlaylistBuilder(playlistId);
            var result = await builder.Tracks().GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("limit", 100), ("offset", 0) });
            mockResults.First().RouteValues.Should().Equal(new[] { "users", TestBase.UserId, "playlists", playlistId, "tracks" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        protected async Task ShouldCreatePlaylistAsync()
        {
            // Arrange
            var dto = new CreatePlaylistDto() { Name = "Test Playlist", Description = "Test Playlist Description", Collaborative = true, Public = true };

            var mockResults = this.MockPost<FullPlaylist>();

            // Act
            var builder = this.GetPlaylistsBuilder();
            var result = await builder.CreateAsync(dto);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "users", UserId, "playlists" });
            mockResults.First().RequestPayload.Value<string>("name").Should().Be(dto.Name);
            mockResults.First().RequestPayload.Value<bool>("public").Should().Be(dto.Public);
            mockResults.First().RequestPayload.Value<bool>("collaborative").Should().Be(dto.Collaborative);
            mockResults.First().RequestPayload.Value<string>("description").Should().Be(dto.Description);
            result.Should().BeSameAs(mockResults.First().Result);
        }

        protected async Task ShouldCreatePlaylistWithDefaultsAsync()
        {
            // Arrange
            var dto = new CreatePlaylistDto() { Name = "Test Playlist", Description = "Test Playlist Description" };

            var mockResults = this.MockPost<FullPlaylist>();

            // Act
            var builder = this.GetPlaylistsBuilder();
            var result = await builder.CreateAsync(dto);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "users", UserId, "playlists" });
            mockResults.First().RequestPayload.Value<string>("name").Should().Be(dto.Name);
            mockResults.First().RequestPayload.Value<bool>("public").Should().Be(true);
            mockResults.First().RequestPayload.Value<bool>("collaborative").Should().Be(false);
            mockResults.First().RequestPayload.Value<string>("description").Should().Be(dto.Description);
            result.Should().BeSameAs(mockResults.First().Result);
        }

        protected async Task ShouldAddTrackToPlaylistAtPositionAsync()
        {
            // Arrange
            const int resultSize = 355;
            const string playlistId = "JIHUIH8943432hU";
            const int position = 2;
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();

            var mockResults = this.MockPost<PlaylistSnapshot>(index => new PlaylistSnapshot());

            // Act
            var builder = this.GetPlaylistBuilder(playlistId);
            var result = await builder.Tracks(ids).AddAsync(position: position);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[] { ("position", position) });
            mockResults.First().RequestPayload.Value<JArray>("uris").Cast<JValue>().Select(item => item.Value).ToArray().Should().Equal(ids.Select(item => $"spotify:track:{item}").ToArray());
            mockResults.First().RouteValues.Should().Equal(new[] { "users", UserId, "playlists", playlistId, "tracks" });
            result.Should().BeSameAs(mockResults.Last().Result);
        }

        protected async Task ShouldAddTrackToPlaylistAsync()
        {
            // Arrange
            const int resultSize = 355;
            const string playlistId = "JIHUIH8943432hU";
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();

            var mockResults = this.MockPost<PlaylistSnapshot>(index => new PlaylistSnapshot());

            // Act
            var builder = this.GetPlaylistBuilder(playlistId);
            var result = await builder.Tracks(ids).AddAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[0]);
            mockResults.First().RequestPayload.Value<JArray>("uris").Cast<JValue>().Select(item => item.Value).ToArray().Should().Equal(ids.Select(item => $"spotify:track:{item}").ToArray());
            mockResults.First().RouteValues.Should().Equal(new[] { "users", UserId, "playlists", playlistId, "tracks" });
            result.Should().BeSameAs(mockResults.Last().Result);
        }

        protected async Task ShouldRemoveAllOccurrencesOfTracksFromPlaylistAsync()
        {
            // Arrange
            const int resultSize = 355;
            const string playlistId = "JIHUIH8943432hU";
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();

            var mockResults = this.MockDelete<PlaylistSnapshot>(index => new PlaylistSnapshot());

            // Act
            var builder = this.GetPlaylistBuilder(playlistId);
            var result = await builder.Tracks(ids).RemoveAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[0]);
            mockResults.First().RequestPayload.Value<JArray>("tracks").Cast<JObject>().Select(item => item.Value<string>("uri")).ToArray().Should().Equal(ids.Select(item => $"spotify:track:{item}").ToArray());
            mockResults.First().RouteValues.Should().Equal(new[] { "users", UserId, "playlists", playlistId, "tracks" });
            result.Should().BeSameAs(mockResults.Last().Result);
        }

        protected async Task ShouldRemoveSpecificOccurrencesOfTracksFromPlaylistAsync()
        {
            // Arrange
            const int resultSize = 356;
            const string playlistId = "JIHUIH8943432hU";
            var idsWithPositions = Enumerable.Range(0, resultSize / 2).Select(item => (item.ToString(), new[] { item, resultSize - item })).ToList();

            var mockResults = this.MockDelete<PlaylistSnapshot>(index => new PlaylistSnapshot());

            // Act
            var builder = this.GetPlaylistBuilder(playlistId);
            var result = await builder.Tracks(idsWithPositions).RemoveAsync();

            // Assert
            mockResults.Should().HaveCount(1);

            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[0]);

            var currentList = mockResults.First().RequestPayload.Value<JArray>("tracks").Cast<JObject>().Select(item => (item.Value<string>("uri"), item.Value<JArray>("positions").Cast<JValue>().Select(value => (long)value.Value).Select(value => (int)value).ToArray())).ToList();
            var expectedList = idsWithPositions.Select(item => ($"spotify:track:{item.Item1}", item.Item2)).ToList();
            currentList.Should().HaveCount(expectedList.Count);
            for (var j = 0; j < currentList.Count; j++)
            {
                currentList[j].Item1.Should().Be(expectedList[j].Item1);
                currentList[j].Item2.Should().Equal(expectedList[j].Item2);
            }

            mockResults.First().RouteValues.Should().Equal(new[] { "users", UserId, "playlists", playlistId, "tracks" });

            result.Should().BeSameAs(mockResults.Last().Result);
        }

        protected async Task ShouldRemoveTracksAtGivenPositionsFromPlaylistAsync()
        {
            // Arrange
            const int resultSize = 355;
            const string playlistId = "JIHUIH8943432hU";
            const string snapshotId = "initialsnapshot";
            var ids = Enumerable.Range(0, resultSize).Select(item => item).ToList();

            var mockResults = this.MockDelete<PlaylistSnapshot>(index => new PlaylistSnapshot());

            // Act
            var builder = this.GetPlaylistBuilder(playlistId);
            var result = await builder.Tracks(ids).RemoveAsync(snapshotId: snapshotId);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[0]);
            mockResults.First().RequestPayload.Value<string>("snapshot_id").Should().Be(snapshotId);
            mockResults.First().RequestPayload.Value<JArray>("positions").Cast<JValue>().Select(item => (long)item.Value).Select(item => (int)item).ToArray().Should().Equal(ids);
            mockResults.First().RouteValues.Should().Equal(new[] { "users", UserId, "playlists", playlistId, "tracks" });
            result.Should().BeSameAs(mockResults.Last().Result);
        }

        protected async Task ShouldReorderPlaylistTracksAsync()
        {
            // Arrange
            const string playlistId = "JIHUIH8943432hU";
            const int rangeStart = 3;
            const int insertBefore = 4;
            const int rangeLength = 4;
            const string snapshotId = "snapshot";

            var mockResults = this.MockPut<PlaylistSnapshot>();

            // Act
            var builder = this.GetPlaylistBuilder(playlistId);
            var result = await builder.Tracks().ReorderAsync(rangeStart: rangeStart, insertBefore: insertBefore, rangeLength: rangeLength, snapshotId: snapshotId);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().RequestPayload.Value<int>("range_start").Should().Be(rangeStart);
            mockResults.First().RequestPayload.Value<int>("range_length").Should().Be(rangeLength);
            mockResults.First().RequestPayload.Value<int>("insert_before").Should().Be(insertBefore);
            mockResults.First().RequestPayload.Value<string>("snapshot_id").Should().Be(snapshotId);
            mockResults.First().RouteValues.Should().Equal(new[] { "users", TestBase.UserId, "playlists", playlistId, "tracks" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        protected async Task ShouldReorderPlaylistTracksWithDefaultsAsync()
        {
            // Arrange
            const string playlistId = "JIHUIH8943432hU";
            const int rangeStart = 3;
            const int insertBefore = 4;

            var mockResults = this.MockPut<PlaylistSnapshot>();

            // Act
            var builder = this.GetPlaylistBuilder(playlistId);
            var result = await builder.Tracks().ReorderAsync(rangeStart: rangeStart, insertBefore: insertBefore);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().RequestPayload.Value<int>("range_start").Should().Be(rangeStart);
            mockResults.First().RequestPayload.Value<int>("range_length").Should().Be(1);
            mockResults.First().RequestPayload.Value<int>("insert_before").Should().Be(insertBefore);
            mockResults.First().RequestPayload.Value<string>("snapshot_id").Should().Be(null);
            mockResults.First().RouteValues.Should().Equal(new[] { "users", TestBase.UserId, "playlists", playlistId, "tracks" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        protected async Task ShouldReplacePlaylistTracksAsync()
        {
            // Arrange
            const int resultSize = 355;
            const string playlistId = "JIHUIH8943432hU";
            var ids = Enumerable.Range(0, resultSize).Select(item => item.ToString()).ToList();

            var mockResults = this.MockPut<PlaylistSnapshot>();

            // Act
            var builder = this.GetPlaylistBuilder(playlistId);
            var result = await builder.Tracks(ids).ReplaceAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[0]);
            mockResults.First().RequestPayload.Value<JArray>("uris").Cast<JValue>().Select(item => (string)item.Value).Should().Equal(ids.Select(item => $"spotify:track:{item}"));
            mockResults.First().RouteValues.Should().Equal(new[] { "users", UserId, "playlists", playlistId, "tracks" });
            result.Should().BeSameAs(mockResults.Last().Result);
        }

        protected async Task ShouldUpdatePlaylistAsync()
        {
            // Arrange
            const string playlistId = "UGYSGU897UGY";
            var dto = new UpdatePlaylistDto() { Name = "Test Playlist", Description = "Test Playlist Description", Collaborative = true, Public = true };

            var mockResults = this.MockPut();

            // Act
            var builder = this.GetPlaylistBuilder(playlistId);
            await builder.UpdateAsync(dto);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "users", UserId, "playlists", playlistId });
            mockResults.First().RequestPayload.Value<string>("name").Should().Be(dto.Name);
            mockResults.First().RequestPayload.Value<bool>("public").Should().Be(dto.Public.Value);
            mockResults.First().RequestPayload.Value<bool>("collaborative").Should().Be(dto.Collaborative.Value);
            mockResults.First().RequestPayload.Value<string>("description").Should().Be(dto.Description);
        }

        protected async Task ShouldUpdatePlaylistCoverAsync()
        {
            // Arrange
            const string playlistId = "UGYSGU897UGY";
           
            var mockResults = this.MockPut();

            // Act
            var builder = this.GetPlaylistBuilder(playlistId);
            await builder.UpdateCoverAsync(ct => null);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "users", UserId, "playlists", playlistId, "images" });
        }
    }
}