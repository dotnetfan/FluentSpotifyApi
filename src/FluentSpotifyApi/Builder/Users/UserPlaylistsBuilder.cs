using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Utils;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Playlists;

namespace FluentSpotifyApi.Builder.Users
{
    internal class UserPlaylistsBuilder : BuilderBase, IUserPlaylistsBuilder
    {
        public UserPlaylistsBuilder(BuilderBase parent)
            : base(parent, "playlists".Yield())
        {
        }

        public Task<Page<SimplifiedPlaylist>> GetAsync(int? limit, int? offset, CancellationToken cancellationToken)
        {
            return this.GetAsync<Page<SimplifiedPlaylist>>(cancellationToken, queryParams: new { limit, offset });
        }

        public Task<Playlist> CreateAsync(CreatePlaylistRequest createPlaylistRequest, CancellationToken cancellationToken)
        {
            SpotifyArgumentAssertUtils.ThrowIfNull(createPlaylistRequest, nameof(createPlaylistRequest));

            return this.SendBodyAsync<CreatePlaylistRequest, Playlist>(HttpMethod.Post, createPlaylistRequest, cancellationToken);
        }
    }
}
