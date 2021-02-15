using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Model.Tracks;

namespace FluentSpotifyApi.Builder.Artists
{
    internal class ArtistTopTracksBuilder : BuilderBase, IArtistTopTracksBuilder
    {
        public ArtistTopTracksBuilder(BuilderBase parent)
            : base(parent, "top-tracks".Yield())
        {
        }

        public Task<TracksResponse> GetAsync(string country, CancellationToken cancellationToken)
        {
            return this.GetAsync<TracksResponse>(cancellationToken, queryParams: new { country });
        }
    }
}
