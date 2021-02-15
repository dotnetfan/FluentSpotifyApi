using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Extensions;

namespace FluentSpotifyApi.Builder.Me.Player
{
    internal class PlaybackQueueBuilder : BuilderBase, IPlaybackQueueBuilder
    {
        private readonly string deviceId;

        public PlaybackQueueBuilder(BuilderBase parent, string deviceId)
            : base(parent, "queue".Yield())
        {
            this.deviceId = deviceId;
        }

        public Task AddAsync(string uri, CancellationToken cancellationToken = default)
            => this.SendAsync(HttpMethod.Post, cancellationToken, queryParams: new { device_id = this.deviceId, uri });
    }
}
