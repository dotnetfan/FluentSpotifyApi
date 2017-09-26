using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Internal.Extensions;
using FluentSpotifyApi.Model;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Builder.Me.Player
{
    internal class PlaybackBuilder : BuilderBase, IDevicePlaybackBuilder, IActiveDevicePlaybackBuilder
    {
        private readonly string deviceId;

        public PlaybackBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName) : this(contextData, routeValuesPrefix, endpointName, null)
        {
        }

        public PlaybackBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName, string deviceId) : base(contextData, routeValuesPrefix, endpointName)
        {
            this.deviceId = deviceId;
        }

        public IPlaybackFromBuilder From => new PlaybackFromBuilder(new PlaybackContext(this.ContextData, this.RouteValuesPrefix, this.deviceId));

        public Task ResumeAsync(CancellationToken cancellationToken)
        {
            return this.SendAsync<PlayerBase>(
                HttpMethod.Put, 
                cancellationToken, 
                optionalQueryStringParameters: this.GetOptionalQueryStringParameters(), 
                additionalRouteValues: new[] { "play" });
        }

        public Task PauseAsync(CancellationToken cancellationToken)
        {
            return this.SendAsync<PlayerBase>(
                HttpMethod.Put, 
                cancellationToken, 
                optionalQueryStringParameters: this.GetOptionalQueryStringParameters(), 
                additionalRouteValues: new[] { "pause" });
        }

        public Task SkipToNextAsync(CancellationToken cancellationToken)
        {
            return this.SendAsync<PlayerBase>(
                HttpMethod.Post, 
                cancellationToken, 
                optionalQueryStringParameters: this.GetOptionalQueryStringParameters(), 
                additionalRouteValues: new[] { "next" });
        }

        public Task SkipToPreviousAsync(CancellationToken cancellationToken)
        {
            return this.SendAsync<PlayerBase>(
                HttpMethod.Post, 
                cancellationToken, 
                optionalQueryStringParameters: this.GetOptionalQueryStringParameters(), 
                additionalRouteValues: new[] { "previous" });
        }

        public Task SeekAsync(TimeSpan position, CancellationToken cancellationToken)
        {
            return this.SendAsync<PlayerBase>(
                HttpMethod.Put, 
                cancellationToken, 
                queryStringParameters: new { position_ms = (int)position.TotalMilliseconds },
                optionalQueryStringParameters: this.GetOptionalQueryStringParameters(), 
                additionalRouteValues: new[] { "seek" });
        }

        public Task SetRepeatModeAsync(RepeatMode repeatMode, CancellationToken cancellationToken)
        {
            return this.SendAsync<PlayerBase>(
                HttpMethod.Put,
                cancellationToken,
                queryStringParameters: new { state = repeatMode.GetDescription() },
                optionalQueryStringParameters: this.GetOptionalQueryStringParameters(),
                additionalRouteValues: new[] { "repeat" });
        }

        public Task SetVolumeAsync(int volumePercent, CancellationToken cancellationToken)
        {
            return this.SendAsync<PlayerBase>(
                HttpMethod.Put,
                cancellationToken,
                queryStringParameters: new { volume_percent = volumePercent },
                optionalQueryStringParameters: this.GetOptionalQueryStringParameters(),
                additionalRouteValues: new[] { "volume" });
        }

        public Task TurnOnShuffleAsync(CancellationToken cancellationToken)
        {
            return this.SendAsync<PlayerBase>(
                HttpMethod.Put,
                cancellationToken,
                queryStringParameters: new { state = true },
                optionalQueryStringParameters: this.GetOptionalQueryStringParameters(),
                additionalRouteValues: new[] { "shuffle" });
        }

        public Task TurnOffShuffleAsync(CancellationToken cancellationToken)
        {
            return this.SendAsync<PlayerBase>(
                HttpMethod.Put,
                cancellationToken,
                queryStringParameters: new { state = false },
                optionalQueryStringParameters: this.GetOptionalQueryStringParameters(),
                additionalRouteValues: new[] { "shuffle" });
        }

        public Task<PlayingContext> GetAsync(string market, CancellationToken cancellationToken)
        {
            return this.GetAsync<PlayingContext>(cancellationToken, optionalQueryStringParameters: new { market });
        }

        public Task<PlayingObject> GetCurrentTrackAsync(string market, CancellationToken cancellationToken)
        {
            return this.GetAsync<PlayingObject>(cancellationToken, optionalQueryStringParameters: new { market }, additionalRouteValues: new[] { "currently-playing" });
        }

        public Task TransferAsync(string deviceId, bool play, CancellationToken cancellationToken)
        {
            return this.SendAsync<PlayerBase, DeviceIds>(HttpMethod.Put, new DeviceIds { Ids = new[] { deviceId } }, cancellationToken, queryStringParameters: new { play });
        }

        private static object GetOptionalQueryStringParameters(string deviceId)
        {
            return new { device_id = deviceId };
        }

        private object GetOptionalQueryStringParameters()
        {
            return GetOptionalQueryStringParameters(this.deviceId);
        }

        private class DeviceIds
        {
            [JsonProperty(PropertyName = "device_ids")]
            public string[] Ids { get; set; }
        }

        private class PlaybackFromBuilder : IPlaybackFromBuilder
        {
            private readonly PlaybackContext playbackContext;
                    
            public PlaybackFromBuilder(PlaybackContext playbackContext)
            {
                this.playbackContext = playbackContext;
            }

            public IPlaybackPlayWithOffsetBuilder Album(string id)
            {
                return new PlaybackPlayBuilder(this.playbackContext, new Body { ContextUri = SpotifyUri.ForAlbum(id) });
            }

            public IPlaybackPlayBuilder Artist(string id)
            {
                return new PlaybackPlayBuilder(this.playbackContext, new Body { ContextUri = SpotifyUri.ForArtist(id) });
            }

            public IPlaybackPlayWithOffsetBuilder Playlist(string ownerId, string playlistId)
            {
                return new PlaybackPlayBuilder(this.playbackContext, new Body { ContextUri = SpotifyUri.ForPlaylist(ownerId, playlistId) });
            }

            public IPlaybackPlayWithOffsetBuilder Tracks(IEnumerable<string> ids)
            {
                return new PlaybackPlayBuilder(this.playbackContext, new Body { TrackUris = ids.Select(item => SpotifyUri.ForTrack(item)).ToArray() });
            }

            private class PlaybackPlayBuilder : IPlaybackPlayWithOffsetBuilder
            {
                private readonly PlaybackContext playbackContext;

                private readonly Body body;

                public PlaybackPlayBuilder(PlaybackContext playbackContext, Body body)
                {
                    this.playbackContext = playbackContext;
                    this.body = body;
                }

                public Task PlayAsync(CancellationToken cancellationToken)
                {
                    var builder = new PlayBuilder(this.playbackContext, this.body);
                    return builder.PlayAsync(cancellationToken);    
                }

                public IPlaybackPlayBuilder StartAt(int position)
                {
                    return new PlaybackPlayBuilder(this.playbackContext, new Body(this.body) { Offset = new Offset { Position = position } });
                }

                public IPlaybackPlayBuilder StartAt(string trackId)
                {
                    return new PlaybackPlayBuilder(this.playbackContext, new Body(this.body) { Offset = new Offset { TrackUri = SpotifyUri.ForTrack(trackId) } });
                }

                private class PlayBuilder : BuilderBase
                {
                    private readonly string deviceId;

                    private readonly Body body;

                    public PlayBuilder(PlaybackContext playbackContext, Body body) : base(playbackContext.ContextData, playbackContext.RouteValuesPrefix, "play")
                    {
                        this.deviceId = playbackContext.DeviceId;
                        this.body = body;
                    }

                    public Task PlayAsync(CancellationToken cancellationToken)
                    {
                        return this.SendAsync<PlayerBase, Body>(HttpMethod.Put, this.body, cancellationToken, optionalQueryStringParameters: PlaybackBuilder.GetOptionalQueryStringParameters(this.deviceId));
                    }
                }
            }

            private class Body
            {
                public Body()
                {
                }

                public Body(Body original)
                {
                    this.TrackUris = original.TrackUris;
                    this.ContextUri = original.ContextUri;
                    this.Offset = original.Offset;
                }

                [JsonProperty(PropertyName = "uris", NullValueHandling = NullValueHandling.Ignore)]
                public string[] TrackUris { get; set; }

                [JsonProperty(PropertyName = "context_uri", NullValueHandling = NullValueHandling.Ignore)]
                public string ContextUri { get; set; }

                [JsonProperty(PropertyName = "offset", NullValueHandling = NullValueHandling.Ignore)]
                public Offset Offset { get; set; }
            }

            private class Offset
            {
                [JsonProperty(PropertyName = "position", NullValueHandling = NullValueHandling.Ignore)]
                public int? Position { get; set; }

                [JsonProperty(PropertyName = "uri", NullValueHandling = NullValueHandling.Ignore)]
                public string TrackUri { get; set; }
            }
        }

        private class PlaybackContext
        {
            public PlaybackContext(ContextData contextData, IEnumerable<object> routeValuesPrefix, string deviceId)
            {
                this.ContextData = contextData;
                this.RouteValuesPrefix = routeValuesPrefix;
                this.DeviceId = deviceId;
            }

            public ContextData ContextData { get; }

            public IEnumerable<object> RouteValuesPrefix { get; }

            public string DeviceId { get; }
        }
    }
}
