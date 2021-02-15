using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Utils;
using FluentSpotifyApi.Extensions;

namespace FluentSpotifyApi.Builder.Me.Following
{
    internal class FollowedPlaylistBuilder : BuilderBase, IFollowedPlaylistBuilder
    {
        public FollowedPlaylistBuilder(RootBuilder root)
            : base(root, "playlists".Yield())
        {
        }

        Task IFollowedPlaylistBuilder.FollowAsync(string id, bool? isPublic, CancellationToken cancellationToken)
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(id, nameof(id));

            return this.SendBodyAsync(HttpMethod.Put, new FollowRequest { Public = isPublic }, cancellationToken, additionalRouteValues: new[] { id, "followers" });
        }

        Task IFollowedPlaylistBuilder.UnfollowAsync(string id, CancellationToken cancellationToken)
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(id, nameof(id));

            return this.SendAsync(HttpMethod.Delete, cancellationToken, additionalRouteValues: new[] { id, "followers" });
        }

        async Task<bool> IFollowedPlaylistBuilder.CheckAsync(string id, CancellationToken cancellationToken)
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(id, nameof(id));

            var result = await this.GetAsync<bool[]>(
                cancellationToken,
                additionalRouteValues: new[] { id, "followers", "contains" },
                queryParams: new { ids = new CurrentUserIdPlaceholder() });

            return result.Single();
        }

        private class FollowRequest
        {
            [JsonPropertyName("public")]
            public bool? Public { get; set; }
        }
    }
}
