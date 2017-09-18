using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Messages;

namespace FluentSpotifyApi.Builder.Artists
{
    /// <summary>
    /// The builder for "artists/{id}/top-tracks".
    /// </summary>
    public interface IArtistTopTracksBuilder
    {
        /// <summary>
        /// Get Spotify catalog information about an artist’s top tracks by country.
        /// </summary>
        /// <param name="country">ISO 3166-1 alpha-2 country code.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<FullTracksMessage> GetAsync(string country, CancellationToken cancellationToken = default(CancellationToken));
    }
}
