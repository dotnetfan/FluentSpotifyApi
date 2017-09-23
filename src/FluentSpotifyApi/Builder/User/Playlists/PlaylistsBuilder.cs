using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.User.Playlists
{
    internal class PlaylistsBuilder : BuilderBase, IPlaylistsBuilder
    {
        public PlaylistsBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, string enpointName) : base(contextData, routeValuesPrefix, enpointName)
        {
        }

        public Task<Page<SimplePlaylist>> GetAsync(int limit, int offset, CancellationToken cancellationToken)
        {
            return this.GetAsync<Page<SimplePlaylist>>(cancellationToken, queryStringParameters: new { limit, offset });
        }

        public Task<FullPlaylist> CreateAsync(CreatePlaylistDto createPlaylistDto, CancellationToken cancellationToken)
        {
            return this.SendAsync<FullPlaylist, CreatePlaylistDto>(HttpMethod.Post, createPlaylistDto, cancellationToken);
        }
    }
}
