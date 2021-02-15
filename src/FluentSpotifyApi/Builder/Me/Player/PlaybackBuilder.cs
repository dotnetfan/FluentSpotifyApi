using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Serialization;
using FluentSpotifyApi.Core.Utils;
using FluentSpotifyApi.Extensions;

namespace FluentSpotifyApi.Builder.Me.Player
{
    internal abstract class PlaybackBuilder : BuilderBase, IPlaybackBuilder
    {
        private readonly string deviceId;

        public PlaybackBuilder(BuilderBase parent)
            : base(parent)
        {
            this.deviceId = null;
        }

        public PlaybackBuilder(BuilderBase parent, string deviceId)
            : base(parent)
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(deviceId, nameof(deviceId));

            this.deviceId = deviceId;
        }

        public IPlaybackQueueBuilder Queue => new PlaybackQueueBuilder(this, this.deviceId);

        public async Task StartNewAsync(string contextUri, int? offset = null, TimeSpan? position = null, CancellationToken cancellationToken = default)
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(contextUri, nameof(contextUri));

            var request = new StartNewPlaybackRequest
            {
                ContextUri = contextUri,
                Offset = offset != null ? new Offset { Position = offset } : null,
                Position = position
            };

            await this.SendBodyAsync(
                HttpMethod.Put,
                request,
                cancellationToken,
                additionalRouteValues: new[] { "play" },
                queryParams: new { device_id = this.deviceId });
        }

        public async Task StartNewAsync(IEnumerable<string> uris, string offset = null, TimeSpan? position = null, CancellationToken cancellationToken = default)
        {
            SpotifyArgumentAssertUtils.ThrowIfNull(uris, nameof(uris));

            var request = new StartNewPlaybackRequest
            {
                Uris = uris.ToArray(),
                Offset = offset != null ? new Offset { Uri = offset } : null,
                Position = position
            };

            await this.SendBodyAsync(
                HttpMethod.Put,
                request,
                cancellationToken,
                additionalRouteValues: new[] { "play" },
                queryParams: new { device_id = this.deviceId });
        }

        public Task ResumeAsync(CancellationToken cancellationToken)
        {
            return this.SendAsync(
                HttpMethod.Put,
                cancellationToken,
                additionalRouteValues: new[] { "play" },
                queryParams: new { device_id = this.deviceId });
        }

        public Task PauseAsync(CancellationToken cancellationToken)
        {
            return this.SendAsync(
                HttpMethod.Put,
                cancellationToken,
                additionalRouteValues: new[] { "pause" },
                queryParams: new { device_id = this.deviceId });
        }

        public Task SkipToNextAsync(CancellationToken cancellationToken)
        {
            return this.SendAsync(
                HttpMethod.Post,
                cancellationToken,
                additionalRouteValues: new[] { "next" },
                queryParams: new { device_id = this.deviceId });
        }

        public Task SkipToPreviousAsync(CancellationToken cancellationToken)
        {
            return this.SendAsync(
                HttpMethod.Post,
                cancellationToken,
                additionalRouteValues: new[] { "previous" },
                queryParams: new { device_id = this.deviceId });
        }

        public Task SeekAsync(TimeSpan position, CancellationToken cancellationToken)
        {
            return this.SendAsync(
                HttpMethod.Put,
                cancellationToken,
                additionalRouteValues: new[] { "seek" },
                queryParams: new { device_id = this.deviceId, position_ms = position.ToWholeMilliseconds() });
        }

        public Task SetRepeatModeAsync(RepeatMode repeatMode, CancellationToken cancellationToken)
        {
            return this.SendAsync(
                HttpMethod.Put,
                cancellationToken,
                additionalRouteValues: new[] { "repeat" },
                queryParams: new { device_id = this.deviceId, state = repeatMode.GetEnumMemberValue() });
        }

        public Task SetVolumeAsync(int volumePercent, CancellationToken cancellationToken)
        {
            return this.SendAsync(
                HttpMethod.Put,
                cancellationToken,
                additionalRouteValues: new[] { "volume" },
                queryParams: new { device_id = this.deviceId, volume_percent = volumePercent });
        }

        public Task TurnOnShuffleAsync(CancellationToken cancellationToken)
        {
            return this.SendAsync(
                HttpMethod.Put,
                cancellationToken,
                additionalRouteValues: new[] { "shuffle" },
                queryParams: new { device_id = this.deviceId, state = true });
        }

        public Task TurnOffShuffleAsync(CancellationToken cancellationToken)
        {
            return this.SendAsync(
                HttpMethod.Put,
                cancellationToken,
                additionalRouteValues: new[] { "shuffle" },
                queryParams: new { device_id = this.deviceId, state = false });
        }

        private class DeviceIds
        {
            [JsonPropertyName("device_ids")]
            public string[] Ids { get; set; }
        }

        private class StartNewPlaybackRequest
        {
            [JsonPropertyName("uris")]
            public string[] Uris { get; set; }

            [JsonPropertyName("context_uri")]
            public string ContextUri { get; set; }

            [JsonPropertyName("offset")]
            public Offset Offset { get; set; }

            [JsonPropertyName("position_ms")]
            [JsonConverter(typeof(SpotifyTimeSpanMillisecondsConverter))]
            public TimeSpan? Position { get; set; }
        }

        private class Offset
        {
            [JsonPropertyName("position")]
            public int? Position { get; set; }

            [JsonPropertyName("uri")]
            public string Uri { get; set; }
        }
    }
}
