using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Builder;
using FluentSpotifyApi.Builder.Me.Player;
using FluentSpotifyApi.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.UnitTests.Builder.Me
{
    [TestClass]
    public class PlayerTests : TestsBase
    {
        [TestMethod]
        public async Task ShouldGetRecentlyPlayedTracks()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/player/recently-played")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Me.Player.RecentlyPlayedTracks.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetRecentlyPlayedTracksWithAllParams()
        {
            // Arrange
            const int limit = 30;
            var after = new DateTime(2010, 3, 4, 10, 0, 0, DateTimeKind.Utc);
            var before = new DateTime(2005, 1, 4, 22, 0, 0, DateTimeKind.Utc);

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/player/recently-played")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["limit"] = limit.ToString(CultureInfo.InvariantCulture),
                    ["after"] = after.ToTimestampMilliseconds().ToString(CultureInfo.InvariantCulture),
                    ["before"] = before.ToTimestampMilliseconds().ToString(CultureInfo.InvariantCulture),
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Me.Player.RecentlyPlayedTracks.GetAsync(limit, after, before);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetDevices()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/player/devices")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Me.Player.Devices.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetCurrentPlayback()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/player")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Me.Player.Playback().GetCurrentAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetCurrentPlaybackWithAllParams()
        {
            // Arrange
            const string market = "BS";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/player")
                .WithExactQueryString(new Dictionary<string, string> { ["market"] = market })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Me.Player.Playback().GetCurrentAsync(market);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetCurrentPlaybackItem()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/player/currently-playing")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Me.Player.Playback().GetCurrentItemAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldGetCurrentPlaybackItemWithAllParams()
        {
            // Arrange
            const string market = "BS";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"me/player/currently-playing")
                .WithExactQueryString(new Dictionary<string, string> { ["market"] = market })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Me.Player.Playback().GetCurrentItemAsync(market);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ShouldTransferPlayback()
        {
            // Arrange
            const string deviceId = "device1";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 1 &&
                    j.TryGetProperty("device_ids", out var array) && array.EnumerateArray().ToArray().Select(x => x.GetString()).SequenceEqual(new[] { deviceId }))
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback().TransferAsync(deviceId: deviceId);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldTransferPlaybackWithAllParams()
        {
            // Arrange
            const string deviceId = "device1";
            const bool play = true;

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player")
                .WithExactQueryString(new Dictionary<string, string> { ["play"] = "true" })
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 1 &&
                    j.TryGetProperty("device_ids", out var array) && array.EnumerateArray().ToArray().Select(x => x.GetString()).SequenceEqual(new[] { deviceId }))
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback().TransferAsync(deviceId, play);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldStartNewPlaybackForActiveDeviceFromContextUri()
        {
            // Arrange
            var uri = SpotifyUri.OfAlbum("6akEvsycLGftJxYudPjmqK");

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player/play")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 1 &&
                    j.TryGetProperty("context_uri", out var contextUri) && contextUri.GetString() == uri)
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback().StartNewAsync(uri);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldStartNewPlaybackForDeviceFromContextUriWithAllParams()
        {
            // Arrange
            const string deviceId = "device1";
            const int offset = 33;
            var positon = TimeSpan.FromSeconds(10);
            var uri = SpotifyUri.OfAlbum("6akEvsycLGftJxYudPjmqK");

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player/play")
                .WithExactQueryString(new Dictionary<string, string> { ["device_id"] = deviceId })
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 3 &&
                    j.TryGetProperty("context_uri", out var contextUri) && contextUri.GetString() == uri &&
                    j.TryGetProperty("offset", out var offsetProperty) && offsetProperty.EnumerateObject().Count() == 1 &&
                        offsetProperty.TryGetProperty("position", out var positionProperty) && positionProperty.GetInt32() == offset &&
                    j.TryGetProperty("position_ms", out var positionMs) && positionMs.GetInt32() == 10000)
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback(deviceId).StartNewAsync(uri, offset, positon);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldStartNewPlaybackForActiveDeviceFromUris()
        {
            // Arrange
            var uris = new[] { SpotifyUri.OfAlbum("6akEvsycLGftJxYudPjmqK"), SpotifyUri.OfEpisode("512ojhOuo1ktJprKbVcKyQ") };

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player/play")
                .WithExactQueryString(string.Empty)
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 1 &&
                    j.TryGetProperty("uris", out var array) && array.EnumerateArray().ToArray().Select(x => x.GetString()).SequenceEqual(uris))
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback().StartNewAsync(uris);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldStartNewPlaybackForDeviceFromUrisWithAllParams()
        {
            // Arrange
            const string deviceId = "device1";
            var positon = TimeSpan.FromSeconds(10);
            var uris = new[] { SpotifyUri.OfAlbum("6akEvsycLGftJxYudPjmqK"), SpotifyUri.OfEpisode("512ojhOuo1ktJprKbVcKyQ") };
            var offset = uris[1];

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player/play")
                .WithExactQueryString(new Dictionary<string, string> { ["device_id"] = deviceId })
                .WithJsonContent(j =>
                    j.EnumerateObject().Count() == 3 &&
                    j.TryGetProperty("uris", out var array) && array.EnumerateArray().ToArray().Select(x => x.GetString()).SequenceEqual(uris) &&
                    j.TryGetProperty("offset", out var offsetProperty) && offsetProperty.EnumerateObject().Count() == 1 &&
                        offsetProperty.TryGetProperty("uri", out var uriProperty) && uriProperty.GetString() == offset &&
                    j.TryGetProperty("position_ms", out var positionMs) && positionMs.GetInt32() == 10000)
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            await this.Client.Me.Player.Playback(deviceId).StartNewAsync(uris, offset, positon);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldResumePlaybackForActiveDevice()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player/play")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback().ResumeAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldResumePlaybackForSpecificDevice()
        {
            // Arrange
            const string deviceId = "device1";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player/play")
                .WithExactQueryString(new Dictionary<string, string> { ["device_id"] = deviceId })
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback(deviceId).ResumeAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldPausePlaybackForActiveDevice()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player/pause")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback().PauseAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldPausePlaybackForSpecificDevice()
        {
            // Arrange
            const string deviceId = "device1";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player/pause")
                .WithExactQueryString(new Dictionary<string, string> { ["device_id"] = deviceId })
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback(deviceId).PauseAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldSkipToNextForActiveDevice()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Post, $"me/player/next")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback().SkipToNextAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldSkipToNextForSpecificDevice()
        {
            // Arrange
            const string deviceId = "device1";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Post, $"me/player/next")
                .WithExactQueryString(new Dictionary<string, string> { ["device_id"] = deviceId })
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback(deviceId).SkipToNextAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldSkipToPreviousForActiveDevice()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Post, $"me/player/previous")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback().SkipToPreviousAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldSkipToPreviousForSpecificDevice()
        {
            // Arrange
            const string deviceId = "device1";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Post, $"me/player/previous")
                .WithExactQueryString(new Dictionary<string, string> { ["device_id"] = deviceId })
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback(deviceId).SkipToPreviousAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldSeekToPositionForActiveDevice()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player/seek")
                .WithExactQueryString(new Dictionary<string, string> { ["position_ms"] = "2000" })
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback().SeekAsync(position: TimeSpan.FromSeconds(2));

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldSeekToPositionForSpecificDevice()
        {
            // Arrange
            const string deviceId = "device1";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player/seek")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["device_id"] = deviceId,
                    ["position_ms"] = "2000"
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback(deviceId).SeekAsync(position: TimeSpan.FromSeconds(2));

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldSetPlaybackRepeatModeForActiveDevice()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player/repeat")
                .WithExactQueryString(new Dictionary<string, string> { ["state"] = "track" })
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback().SetRepeatModeAsync(repeatMode: RepeatMode.Track);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldSetPlaybackRepeatModeForSpecificDevice()
        {
            // Arrange
            const string deviceId = "device1";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player/repeat")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["device_id"] = deviceId,
                    ["state"] = "track"
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback(deviceId).SetRepeatModeAsync(repeatMode: RepeatMode.Track);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldSetPlaybackVolumeForActiveDevice()
        {
            // Arrange
            const int volumePercent = 33;

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player/volume")
                .WithExactQueryString(new Dictionary<string, string> { ["volume_percent"] = volumePercent.ToString(CultureInfo.InvariantCulture) })
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback().SetVolumeAsync(volumePercent);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldSetPlaybackVolumeForSpecificDevice()
        {
            // Arrange
            const string deviceId = "device1";
            const int volumePercent = 33;

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player/volume")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["device_id"] = deviceId,
                    ["volume_percent"] = volumePercent.ToString(CultureInfo.InvariantCulture)
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback(deviceId).SetVolumeAsync(volumePercent);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldTurnOnPlaybackShuffleForActiveDevice()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player/shuffle")
                .WithExactQueryString(new Dictionary<string, string> { ["state"] = "true" })
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback().TurnOnShuffleAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldTurnOnPlaybackShuffleForSpecificDevice()
        {
            // Arrange
            const string deviceId = "device1";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player/shuffle")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["device_id"] = deviceId,
                    ["state"] = "true",
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback(deviceId).TurnOnShuffleAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldTurnOffPlaybackShuffleForActiveDevice()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player/shuffle")
                .WithExactQueryString(new Dictionary<string, string> { ["state"] = "false" })
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback().TurnOffShuffleAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldTurnOffPlaybackShuffleForSpecificDevice()
        {
            // Arrange
            const string deviceId = "device1";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Put, $"me/player/shuffle")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["device_id"] = deviceId,
                    ["state"] = "false",
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback(deviceId).TurnOffShuffleAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldAddItemToPlaybackQueueForActiveDevice()
        {
            // Arrange
            var uri = SpotifyUri.OfAlbum("6akEvsycLGftJxYudPjmqK");

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Post, $"me/player/queue")
                .WithExactQueryString(new Dictionary<string, string> { ["uri"] = uri })
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback().Queue.AddAsync(uri);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldAddItemToPlaybackQueueForSpecificDevice()
        {
            // Arrange
            const string deviceId = "device1";
            var uri = SpotifyUri.OfAlbum("6akEvsycLGftJxYudPjmqK");

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Post, $"me/player/queue")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["device_id"] = deviceId,
                    ["uri"] = uri
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK);

            // Act
            await this.Client.Me.Player.Playback(deviceId).Queue.AddAsync(uri);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }
    }
}
