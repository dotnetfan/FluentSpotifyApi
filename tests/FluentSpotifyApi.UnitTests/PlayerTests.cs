using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Builder.Me.Player;
using FluentSpotifyApi.Core.Internal.Extensions;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace FluentSpotifyApi.UnitTests
{
    [TestClass]
    public class PlayerTests : TestBase
    {
        [TestMethod]
        public async Task ShouldGetDevicesAsync()
        {
            // Arrange
            var mockResults = this.MockGet<DevicesMessage>();

            // Act
            var result = await this.Client.Me.Player.Devices.GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "devices" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetCurrentPlaybackAsync()
        {
            // Arrange
            const string market = "BS";

            var mockResults = this.MockGet<PlayingContext>();

            // Act
            var result = await this.Client.Me.Player.Playback().GetAsync(market: market);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[] { ("market", market) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetCurrentPlaybackWithDefaultsAsync()
        {
            // Arrange
            var mockResults = this.MockGet<PlayingContext>();

            // Act
            var result = await this.Client.Me.Player.Playback().GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetCurrentlyPlayingTrackAsync()
        {
            // Arrange
            const string market = "BS";

            var mockResults = this.MockGet<PlayingObject>();

            // Act
            var result = await this.Client.Me.Player.Playback().GetCurrentTrackAsync(market: market);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[] { ("market", market) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "currently-playing" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetCurrentlyPlayingTrackWithDefaultsAsync()
        {
            // Arrange
            var mockResults = this.MockGet<PlayingObject>();

            // Act
            var result = await this.Client.Me.Player.Playback().GetCurrentTrackAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "currently-playing" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldTransferPlaybackAsync()
        {
            // Arrange
            const string deviceId = "1vCfHas5f2uS3yhpwWbIA6";
            const bool play = true;

            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback().TransferAsync(deviceId: deviceId, play: play);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[] { ("play", play) });
            mockResults.First().RequestPayload.Value<JArray>("device_ids").Cast<JValue>().Select(item => item.Value).ToArray().Should().Equal(deviceId.Yield().ToArray());
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player" });
        }

        [TestMethod]
        public async Task ShouldTransferPlaybackWithDefaultsAsync()
        {
            // Arrange
            const string deviceId = "1vCfHas5f2uS3yhpwWbIA6";

            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback().TransferAsync(deviceId: deviceId);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[] { ("play", false) });
            mockResults.First().RequestPayload.Value<JArray>("device_ids").Cast<JValue>().Select(item => item.Value).ToArray().Should().Equal(deviceId.Yield().ToArray());
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player" });
        }

        [TestMethod]
        public async Task ShouldStartArtistPlaybackForActiveDeviceAsync()
        {
            // Arrange
            const string artistId = "1vDFSs5f2uS3yhpdsWbIA6";

            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback().From.Artist(artistId).PlayAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[0]);
            mockResults.First().RequestPayload.Value<string>("context_uri").Should().Be($"spotify:artist:{artistId}");
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "play" });
        }

        [TestMethod]
        public async Task ShouldStartArtistPlaybackForSpecificDeviceAsync()
        {
            // Arrange
            const string deviceId = "1vCfHas5f2uS3yhpwWbIA6";
            const string artistId = "1vDFSs5f2uS3yhpdsWbIA6";

            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback(deviceId).From.Artist(artistId).PlayAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[] { ("device_id", deviceId) });
            mockResults.First().RequestPayload.Value<string>("context_uri").Should().Be($"spotify:artist:{artistId}");
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "play" });
        }

        [TestMethod]
        public async Task ShouldStartAlbumPlaybackAsync()
        {
            // Arrange
            const string albumId = "1hDFs5f2uS3yhpdsWbIA6";

            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback().From.Album(albumId).PlayAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[0]);
            mockResults.First().RequestPayload.Value<string>("context_uri").Should().Be($"spotify:album:{albumId}");
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "play" });
        }

        [TestMethod]
        public async Task ShouldStartAlbumPlaybackAtPositionAsync()
        {
            // Arrange
            const string albumId = "1hDFs5f2uS3yhpdsWbIA6";
            const int position = 2;

            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback().From.Album(albumId).StartAt(position).PlayAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[0]);
            mockResults.First().RequestPayload.Value<string>("context_uri").Should().Be($"spotify:album:{albumId}");
            mockResults.First().RequestPayload.Value<JObject>("offset").Value<int>("position").Should().Be(position);
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "play" });
        }

        [TestMethod]
        public async Task ShouldStartAlbumPlaybackAtSpecificTrackAsync()
        {
            // Arrange
            const string albumId = "1hDFs5f2uS3yhpdsWbIA6";
            const string trackId = "1dsfgwf2uS3yspdttbIA6";

            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback().From.Album(albumId).StartAt(trackId).PlayAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[0]);
            mockResults.First().RequestPayload.Value<string>("context_uri").Should().Be($"spotify:album:{albumId}");
            mockResults.First().RequestPayload.Value<JObject>("offset").Value<string>("uri").Should().Be($"spotify:track:{trackId}");
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "play" });
        }

        [TestMethod]
        public async Task ShouldStartPlaylistPlaybackAsync()
        {
            // Arrange
            const string ownerId = "testuser";
            const string playlistId = "xhDFs5f2uS3yhpdsWbIA6";

            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback().From.Playlist(ownerId, playlistId).PlayAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[0]);
            mockResults.First().RequestPayload.Value<string>("context_uri").Should().Be($"spotify:user:{ownerId}:playlist:{playlistId}");
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "play" });
        }

        [TestMethod]
        public async Task ShouldStartTracksPlaybackAsync()
        {
            // Arrange
            const string track1 = "xhDFs5asedgS3yhpdsWbIA6";
            const string track2 = "xhDFs5fdauSq3yhpdsWbIA6";

            var tracks = new[] { track1, track2 };

            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback().From.Tracks(tracks).PlayAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[0]);
            mockResults.First().RequestPayload.Value<JArray>("uris").Cast<JValue>().Select(item => item.Value).ToArray().Should().Equal(tracks.Select(item => $"spotify:track:{item}").ToArray());
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "play" });
        }

        [TestMethod]
        public async Task ShouldResumePlaybackForActiveDeviceAsync()
        {
            // Arrange
            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback().ResumeAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "play" });
        }

        [TestMethod]
        public async Task ShouldResumePlaybackForSpecificDeviceAsync()
        {
            // Arrange
            const string deviceId = "1vCfHas5f2uS3yhpwWbIA6";

            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback(deviceId).ResumeAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[] { ("device_id", deviceId) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "play" });
        }

        [TestMethod]
        public async Task ShouldPausePlaybackForActiveDeviceAsync()
        {
            // Arrange
            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback().PauseAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "pause" });
        }

        [TestMethod]
        public async Task ShouldPausePlaybackForSpecificDeviceAsync()
        {
            // Arrange
            const string deviceId = "1vCfHas5f2uS3yhpwWbIA6";

            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback(deviceId).PauseAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[] { ("device_id", deviceId) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "pause" });
        }

        [TestMethod]
        public async Task ShouldSkipToNextForActiveDeviceAsync()
        {
            // Arrange
            var mockResults = this.MockPost<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback().SkipToNextAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "next" });
        }

        [TestMethod]
        public async Task ShouldSkipToNextForSpecificDeviceAsync()
        {
            // Arrange
            const string deviceId = "1vCfHas5f2uS3yhpwWbIA6";

            var mockResults = this.MockPost<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback(deviceId).SkipToNextAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[] { ("device_id", deviceId) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "next" });
        }

        [TestMethod]
        public async Task ShouldSkipToPreviousForActiveDeviceAsync()
        {
            // Arrange
            var mockResults = this.MockPost<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback().SkipToPreviousAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "previous" });
        }

        [TestMethod]
        public async Task ShouldSkipToPreviousForSpecificDeviceAsync()
        {
            // Arrange
            const string deviceId = "1vCfHas5f2uS3yhpwWbIA6";

            var mockResults = this.MockPost<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback(deviceId).SkipToPreviousAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[] { ("device_id", deviceId) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "previous" });
        }

        [TestMethod]
        public async Task ShouldSeekToPositionForActiveDeviceAsync()
        {
            // Arrange
            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback().SeekAsync(position: TimeSpan.FromSeconds(2));

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[] { ("position_ms", 2000) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "seek" });
        }

        [TestMethod]
        public async Task ShouldSeekToPositionForSpecificDeviceAsync()
        {
            // Arrange
            const string deviceId = "1vCfHas5f2uS3yhpwWbIA6";
            
            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback(deviceId).SeekAsync(position: TimeSpan.FromSeconds(2));

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[] { ("device_id", deviceId), ("position_ms", 2000) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "seek" });
        }

        [TestMethod]
        public async Task ShouldSetPlaybackRepeatModeForActiveDeviceAsync()
        {
            // Arrange
            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback().SetRepeatModeAsync(repeatMode: RepeatMode.Track);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[] { ("state", "track") });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "repeat" });
        }

        [TestMethod]
        public async Task ShouldSetPlaybackRepeatModeForSpecificDeviceAsync()
        {
            // Arrange
            const string deviceId = "1vCfHas5f2uS3yhpwWbIA6";

            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback(deviceId).SetRepeatModeAsync(repeatMode: RepeatMode.Track);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[] { ("state", "track"), ("device_id", deviceId) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "repeat" });
        }

        [TestMethod]
        public async Task ShouldSetPlaybackVolumeForActiveDeviceAsync()
        {
            // Arrange
            const int volumePercent = 33;

            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback().SetVolumeAsync(volumePercent);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[] { ("volume_percent", volumePercent) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "volume" });
        }

        [TestMethod]
        public async Task ShouldSetPlaybackVolumeForSpecificDeviceAsync()
        {
            // Arrange
            const string deviceId = "1vCfHas5f2uS3yhpwWbIA6";
            const int volumePercent = 33;

            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback(deviceId).SetVolumeAsync(volumePercent);

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[] { ("volume_percent", volumePercent), ("device_id", deviceId) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "volume" });
        }

        [TestMethod]
        public async Task ShouldTurnOnPlaybackShuffleForActiveDeviceAsync()
        {
            // Arrange
            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback().TurnOnShuffleAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[] { ("state", true) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "shuffle" });
        }

        [TestMethod]
        public async Task ShouldTurnOnPlaybackShuffleForSpecificDeviceAsync()
        {
            // Arrange
            const string deviceId = "1vCfHas5f2uS3yhpwWbIA6";

            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback(deviceId).TurnOnShuffleAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[] { ("state", true), ("device_id", deviceId) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "shuffle" });
        }

        [TestMethod]
        public async Task ShouldTurnOffPlaybackShuffleForActiveDeviceAsync()
        {
            // Arrange
            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback().TurnOffShuffleAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[] { ("state", false) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "shuffle" });
        }

        [TestMethod]
        public async Task ShouldTurnOffPlaybackShuffleForSpecificDeviceAsync()
        {
            // Arrange
            const string deviceId = "1vCfHas5f2uS3yhpwWbIA6";

            var mockResults = this.MockPut<PlayerBase>();

            // Act
            await this.Client.Me.Player.Playback(deviceId).TurnOffShuffleAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new (string Key, object Value)[] { ("state", false), ("device_id", deviceId) });
            mockResults.First().RouteValues.Should().Equal(new[] { "me", "player", "shuffle" });
        }
    }
}
