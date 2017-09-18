using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Albums
{
    /// <summary>
    /// The builder for "albums/{id}/tracks" endpoint.
    /// </summary>
    public interface IAlbumTracksBuilder
    {
        /// <summary>
        /// Get Spotify catalog information about an album’s tracks.
        /// </summary>
        /// <param name="limit">The maximum number of tracks to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first track to return. Default: 0 (the first object).</param>
        /// <param name="market">An ISO 3166-1 alpha-2 country code.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Page<SimpleTrack>> GetAsync(int limit = 20, int offset = 0, string market = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
