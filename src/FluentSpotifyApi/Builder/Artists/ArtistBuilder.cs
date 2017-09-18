using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Artists
{
    internal class ArtistBuilder : EntityBuilderBase, IArtistBuilder
    {
        public ArtistBuilder(ContextData contextData, string endpointName, string id) : base(contextData, endpointName, id)
        {
        }

        public IArtistAlbumsBuilder Albums => new ArtistAlbumsBuilder(ContextData, RouteValuesPrefix);

        public IArtistTopTracksBuilder TopTracks => new ArtistTopTracksBuilder(ContextData, RouteValuesPrefix);

        public IArtistRelatedArtistsBuilder RelatedArtists => new ArtistRelatedArtistsBuilder(ContextData, RouteValuesPrefix);

        public Task<FullArtist> GetAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<FullArtist>(cancellationToken);
        }
    }
}
