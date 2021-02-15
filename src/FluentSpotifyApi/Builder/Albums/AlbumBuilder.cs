using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Albums;

namespace FluentSpotifyApi.Builder.Albums
{
    internal class AlbumBuilder : EntityBuilderBase, IAlbumBuilder
    {
        public AlbumBuilder(RootBuilder root, string id)
            : base(root, "albums", id)
        {
        }

        public IAlbumTracksBuilder Tracks => new AlbumTracksBuilder(this);

        public Task<Album> GetAsync(string market, CancellationToken cancellationToken)
        {
            return this.GetAsync<Album>(cancellationToken, queryParams: new { market });
        }
    }
}
