using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Model.Artists;

namespace FluentSpotifyApi.Builder.Artists
{
    internal class ArtistRelatedArtistsBuilder : BuilderBase, IArtistRelatedArtistsBuilder
    {
        public ArtistRelatedArtistsBuilder(BuilderBase parent)
            : base(parent, "related-artists".Yield())
        {
        }

        public Task<ArtistsResponse> GetAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<ArtistsResponse>(cancellationToken);
        }
    }
}
