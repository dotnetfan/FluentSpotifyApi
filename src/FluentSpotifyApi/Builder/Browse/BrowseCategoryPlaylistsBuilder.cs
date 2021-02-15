using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Model.Playlists;

namespace FluentSpotifyApi.Builder.Browse
{
    internal class BrowseCategoryPlaylistsBuilder : BuilderBase, IBrowseCategoryPlaylistsBuilder
    {
        public BrowseCategoryPlaylistsBuilder(BuilderBase parent)
            : base(parent, "playlists".Yield())
        {
        }

        public Task<SimplifiedPlaylistsPageResponse> GetAsync(string country, int? limit, int? offset, CancellationToken cancellationToken)
        {
            return this.GetAsync<SimplifiedPlaylistsPageResponse>(cancellationToken, queryParams: new { country, limit, offset });
        }
    }
}
