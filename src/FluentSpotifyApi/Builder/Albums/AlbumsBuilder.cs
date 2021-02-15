using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Albums;

namespace FluentSpotifyApi.Builder.Albums
{
    internal class AlbumsBuilder : EntitiesBuilderBase, IAlbumsBuilder
    {
        public AlbumsBuilder(RootBuilder root, IEnumerable<string> ids)
            : base(root, "albums", ids)
        {
        }

        public Task<AlbumsResponse> GetAsync(string market, CancellationToken cancellationToken)
        {
            return this.GetListAsync<AlbumsResponse>(cancellationToken, queryParams: new { market });
        }
    }
}
