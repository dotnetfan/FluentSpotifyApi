using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Builder;
using FluentSpotifyApi.Model.Playlists;
using FluentSpotifyApi.Model.Tracks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.UnitTests.Builder
{
    [TestClass]
    public class PlaylistsTests : TestsBase
    {
        [TestMethod]
        public async Task ShouldGetPlaylist()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"playlists/{playlistId}")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Playlists(playlistId).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetPlaylistWithAllParams()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";
            const string fields = "(description,tracks(items(track(name))))";
            const string market = "BR";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"playlists/{playlistId}")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["fields"] = fields,
                    ["market"] = market
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Playlists(playlistId).GetAsync(
                buildFields: fieldsBuilder => fieldsBuilder
                    .Include(p => p.Description)
                    .Include(p => ((Track)p.Tracks.Items[0].Track).Name),
                market: market);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldCheckFollowers()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";
            var ids = new[] { "user1", "user2" };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"playlists/{playlistId}/followers/contains")
                .WithExactQueryString(new Dictionary<string, string> { ["ids"] = string.Join(",", ids) })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "[]");

            // Act
            var result = await this.Client.Playlists(playlistId).CheckFollowersAsync(ids);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldUpdatePlaylist()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";
            var request = new ChangePlaylistDetailsRequest();

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"playlists/{playlistId}")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j => !j.EnumerateObject().Any())
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Playlists(playlistId).ChangeDetailsAsync(request);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldUpdatePlaylistWithAllParams()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";
            var request = new ChangePlaylistDetailsRequest()
            {
                Name = "Test Playlist",
                Description = "Test Playlist Description",
                Collaborative = true,
                Public = true
            };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"playlists/{playlistId}")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 4 &&
                    j.TryGetProperty("name", out var name) && name.GetString() == request.Name &&
                    j.TryGetProperty("description", out var description) && description.GetString() == request.Description &&
                    j.TryGetProperty("collaborative", out var collaborative) && collaborative.GetBoolean() == request.Collaborative &&
                    j.TryGetProperty("public", out var @public) && @public.GetBoolean() == request.Public)
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Playlists(playlistId).ChangeDetailsAsync(request);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldGetPlaylistCover()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";
            var jpeg = new byte[] { 3, 4, 6, 8, 44, 124 };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"playlists/{playlistId}/images")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "[]");

            // Act
            var result = await this.Client.Playlists(playlistId).Images.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldUpdatePlaylistCover()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";
            var jpeg = new byte[] { 3, 4, 6, 8, 44, 124 };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"playlists/{playlistId}/images")
                .WithExactQueryString(string.Empty)
                .With(r =>
                {
                    if (r.Content == null)
                    {
                        return false;
                    }

                    if (r.Content.Headers.ContentType.MediaType != "image/jpeg")
                    {
                        return false;
                    }

                    var content = r.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return content.Equals(Convert.ToBase64String(jpeg));
                })
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Playlists(playlistId).Images.UpdateAsync(jpeg);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldGetPlaylistItems()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"playlists/{playlistId}/tracks")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Playlists(playlistId).Items.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetPlaylistItemsWithAllParams()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";
            const string fields = "(items(added_at,added_by(!display_name)))";
            const string market = "BR";
            const int limit = 20;
            const int offset = 10;

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"playlists/{playlistId}/tracks")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["fields"] = fields,
                    ["market"] = market,
                    ["limit"] = limit.ToString(CultureInfo.InvariantCulture),
                    ["offset"] = offset.ToString(CultureInfo.InvariantCulture),
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Playlists(playlistId).Items.GetAsync(
                buildFields: fieldsBuilder => fieldsBuilder
                    .Include(t => t.Items[0].AddedAt)
                    .Exclude(t => t.Items[0].AddedBy.DisplayName),
                market: market,
                limit: limit,
                offset: offset);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldAddItemsToPlaylist()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";
            var uris = new[] { SpotifyUri.OfTrack("3n3Ppam7vgaVa1iaRUc9Lp"), SpotifyUri.OfEpisode("512ojhOuo1ktJprKbVcKyQ") };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Post, $"playlists/{playlistId}/tracks")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 1 &&
                    j.TryGetProperty("uris", out var array) && array.EnumerateArray().ToArray().Select(x => x.GetString()).SequenceEqual(uris))
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Playlists(playlistId).Items.AddAsync(uris);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldAddItemsToPlaylistAtPosition()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";
            var uris = new[] { SpotifyUri.OfTrack("3n3Ppam7vgaVa1iaRUc9Lp"), SpotifyUri.OfEpisode("512ojhOuo1ktJprKbVcKyQ") };
            const int position = 2;

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Post, $"playlists/{playlistId}/tracks")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 2 &&
                    j.TryGetProperty("position", out var positionProperty) && positionProperty.GetInt32() == position &&
                    j.TryGetProperty("uris", out var array) && array.EnumerateArray().ToArray().Select(x => x.GetString()).SequenceEqual(uris))
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Playlists(playlistId).Items.AddAsync(uris, position: position);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldRemoveAllOccurrencesOfItemsFromPlaylist()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";
            var uris = new[] { SpotifyUri.OfTrack("3n3Ppam7vgaVa1iaRUc9Lp"), SpotifyUri.OfEpisode("512ojhOuo1ktJprKbVcKyQ") };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Delete, $"playlists/{playlistId}/tracks")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 1 &&
                    j.TryGetProperty("tracks", out var array) &&
                        array.EnumerateArray().ToArray().All(x => x.EnumerateObject().Count() == 1) &&
                        array.EnumerateArray().ToArray().Select(x => x.GetProperty("uri").GetString()).SequenceEqual(uris))
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Playlists(playlistId).Items.RemoveAsync(uris);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldRemoveSpecificOccurrencesOfItemsFromPlaylist()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";
            const string snapshotId = "initialsnapshot";
            var urisWithPositions = new[]
            {
                new UriWithPositions { Uri = SpotifyUri.OfTrack("3n3Ppam7vgaVa1iaRUc9Lp"), Positions = new[] { 1, 2 } },
                new UriWithPositions { Uri = SpotifyUri.OfEpisode("512ojhOuo1ktJprKbVcKyQ"), Positions = null },
                new UriWithPositions { Uri = SpotifyUri.OfTrack("3twNvmDtFQtAd5gMKedhLD"), Positions = new[] { 5 } },
            };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Delete, $"playlists/{playlistId}/tracks")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                {
                    if (!(j.EnumerateObject().Count() == 2 &&
                    j.TryGetProperty("snapshot_id", out var snapshotProperty) && snapshotProperty.GetString() == snapshotId &&
                    j.TryGetProperty("tracks", out var array)))
                    {
                        return false;
                    }

                    return
                        array.EnumerateArray().Count() == 3 &&
                        array[0].EnumerateObject().Count() == 2 &&
                        array[0].GetProperty("uri").GetString() == urisWithPositions[0].Uri &&
                        array[0].GetProperty("positions").EnumerateArray().Select(x => x.GetInt32()).SequenceEqual(urisWithPositions[0].Positions) &&
                        array[1].EnumerateObject().Count() == 1 &&
                        array[1].GetProperty("uri").GetString() == urisWithPositions[1].Uri &&
                        array[2].EnumerateObject().Count() == 2 &&
                        array[2].GetProperty("uri").GetString() == urisWithPositions[2].Uri &&
                        array[2].GetProperty("positions").EnumerateArray().Select(x => x.GetInt32()).SequenceEqual(urisWithPositions[2].Positions);
                })
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Playlists(playlistId).Items.RemoveAsync(urisWithPositions, snapshotId);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldRemoveItemsAtGivenPositionsFromPlaylist()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";
            const string snapshotId = "initialsnapshot";
            var positions = new[] { 3, 5, 7, 9 };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Delete, $"playlists/{playlistId}/tracks")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 2 &&
                    j.TryGetProperty("snapshot_id", out var snapshotProperty) && snapshotProperty.GetString() == snapshotId &&
                    j.TryGetProperty("positions", out var array) && array.EnumerateArray().ToArray().Select(x => x.GetInt32()).SequenceEqual(positions))
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Playlists(playlistId).Items.RemoveAsync(positions, snapshotId);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldReorderPlaylistItems()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";
            const int rangeStart = 3;
            const int insertBefore = 4;

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"playlists/{playlistId}/tracks")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 2 &&
                    j.TryGetProperty("range_start", out var rangeStartProperty) && rangeStartProperty.GetInt32() == rangeStart &&
                    j.TryGetProperty("insert_before", out var insertBeforeProperty) && insertBeforeProperty.GetInt32() == insertBefore)
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Playlists(playlistId).Items.ReorderAsync(rangeStart, insertBefore);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldReorderPlaylistItemsWithAllParams()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";
            const int rangeStart = 3;
            const int insertBefore = 4;
            const int rangeLength = 4;
            const string snapshotId = "snapshot";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"playlists/{playlistId}/tracks")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 4 &&
                    j.TryGetProperty("range_start", out var rangeStartProperty) && rangeStartProperty.GetInt32() == rangeStart &&
                    j.TryGetProperty("insert_before", out var insertBeforeProperty) && insertBeforeProperty.GetInt32() == insertBefore &&
                    j.TryGetProperty("range_length", out var rangeLengthProperty) && rangeLengthProperty.GetInt32() == rangeLength &&
                    j.TryGetProperty("snapshot_id", out var snapshotProperty) && snapshotProperty.GetString() == snapshotId)
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Playlists(playlistId).Items.ReorderAsync(rangeStart, insertBefore, rangeLength, snapshotId);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldReplacePlaylistItems()
        {
            // Arrange
            const string playlistId = "40C5k2GWBlficlUyQKmR0S";
            var uris = new[] { SpotifyUri.OfTrack("3n3Ppam7vgaVa1iaRUc9Lp"), SpotifyUri.OfEpisode("512ojhOuo1ktJprKbVcKyQ") };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"playlists/{playlistId}/tracks")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 1 &&
                    j.TryGetProperty("uris", out var array) && array.EnumerateArray().ToArray().Select(x => x.GetString()).SequenceEqual(uris))
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Playlists(playlistId).Items.ReplaceAsync(uris);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }
    }
}
