using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Utils;
using FluentSpotifyApi.Model.Player;

namespace FluentSpotifyApi.Builder.Me.Player
{
    internal class ActiveDevicePlaybackBuilder : PlaybackBuilder, IActiveDevicePlaybackBuilder
    {
        public ActiveDevicePlaybackBuilder(BuilderBase parent)
            : base(parent)
        {
        }

        public Task<CurrentlyPlayingContext> GetCurrentAsync(string market, CancellationToken cancellationToken)
        {
            return this.GetAsync<CurrentlyPlayingContext>(cancellationToken, queryParams: new { market });
        }

        public Task<CurrentlyPlayingItem> GetCurrentItemAsync(string market, CancellationToken cancellationToken)
        {
            return this.GetAsync<CurrentlyPlayingItem>(cancellationToken, additionalRouteValues: new[] { "currently-playing" }, queryParams: new { market });
        }

        public Task TransferAsync(string deviceId, bool? play, CancellationToken cancellationToken)
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(deviceId, nameof(deviceId));

            return this.SendBodyAsync(HttpMethod.Put, new DeviceIds { Ids = new[] { deviceId } }, cancellationToken, queryParams: new { play });
        }

        private class DeviceIds
        {
            [JsonPropertyName("device_ids")]
            public string[] Ids { get; set; }
        }
    }
}
