using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Artists;

namespace FluentSpotifyApi.Builder.Artists
{
    internal class ArtistsBuilder : EntitiesBuilderBase, IArtistsBuilder
    {
        public ArtistsBuilder(RootBuilder root, IEnumerable<string> ids)
            : base(root, "artists", ids)
        {
        }

        public async Task<ArtistsResponse> GetAsync(CancellationToken cancellationToken)
        {
            return await this.GetListAsync<ArtistsResponse>(cancellationToken);
        }
    }
}
