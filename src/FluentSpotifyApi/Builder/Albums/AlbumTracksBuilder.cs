using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Tracks;

namespace FluentSpotifyApi.Builder.Albums
{
    internal class AlbumTracksBuilder : BuilderBase, IAlbumTracksBuilder
    {
        public AlbumTracksBuilder(BuilderBase parent)
            : base(parent, "tracks".Yield())
        {
        }

        public Task<Page<SimplifiedTrack>> GetAsync(string market, int? limit, int? offset, CancellationToken cancellationToken)
        {
            return this.GetAsync<Page<SimplifiedTrack>>(cancellationToken, queryParams: new { market, limit, offset });
        }
    }
}
