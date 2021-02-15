using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Artists;

namespace FluentSpotifyApi.Builder.Artists
{
    internal class ArtistBuilder : EntityBuilderBase, IArtistBuilder
    {
        public ArtistBuilder(RootBuilder root, string id)
            : base(root, "artists", id)
        {
        }

        public IArtistAlbumsBuilder Albums => new ArtistAlbumsBuilder(this);

        public IArtistTopTracksBuilder TopTracks => new ArtistTopTracksBuilder(this);

        public IArtistRelatedArtistsBuilder RelatedArtists => new ArtistRelatedArtistsBuilder(this);

        public Task<Artist> GetAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<Artist>(cancellationToken);
        }
    }
}
