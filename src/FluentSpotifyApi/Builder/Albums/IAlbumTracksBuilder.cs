using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Tracks;

namespace FluentSpotifyApi.Builder.Albums
{
    /// <summary>
    /// The builder for "albums/{id}/tracks" endpoint.
    /// </summary>
    public interface IAlbumTracksBuilder
    {
        /// <summary>
        /// Gets Spotify catalog information about an album’s tracks. Optional parameters can be used to limit the number of tracks returned.
        /// </summary>
        /// <param name="market">An ISO 3166-1 alpha-2 country code or the string <c>from_token</c>. Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="limit">The maximum number of tracks to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first track to return. Default: 0 (the first object). Use with <paramref name="limit"/> to get the next set of tracks.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Page<SimplifiedTrack>> GetAsync(string market = null, int? limit = null, int? offset = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
