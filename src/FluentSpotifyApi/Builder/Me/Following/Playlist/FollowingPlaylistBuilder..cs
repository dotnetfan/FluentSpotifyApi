using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Builder.Me.Following.Playlist
{
    internal class FollowingPlaylistBuilder : BuilderBase, IFollowingPlaylistBuilder
    {
        public FollowingPlaylistBuilder(ContextData contextData, string ownerId, string playlistId) 
            : base(contextData, null, "users", new[] { ownerId, "playlists", playlistId, "followers" })
        {
        }

        Task IFollowingPlaylistBuilder.FollowAsync(bool isPublic, CancellationToken cancellationToken)
        {
            return this.SendAsync<object>(HttpMethod.Put, cancellationToken, queryStringParameters: new { parameter = new KeyValuePair<string, object>("public", isPublic) });
        }

        Task IFollowingPlaylistBuilder.UnfollowAsync(CancellationToken cancellationToken)
        {
            return this.SendAsync<object>(HttpMethod.Delete, cancellationToken);
        }

        async Task<bool> IFollowingPlaylistBuilder.CheckAsync(CancellationToken cancellationToken)
        {
            var result = await this.GetAsync<bool[]>(cancellationToken, queryStringParameters: new { ids = UserTransformer }, additionalRouteValues: new[] { "contains" });

            return (result?.FirstOrDefault()).GetValueOrDefault();
        }
    }
}
