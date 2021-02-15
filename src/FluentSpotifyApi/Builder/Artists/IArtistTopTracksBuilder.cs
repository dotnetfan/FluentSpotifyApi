using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Tracks;

namespace FluentSpotifyApi.Builder.Artists
{
    /// <summary>
    /// The builder for "artists/{id}/top-tracks".
    /// </summary>
    public interface IArtistTopTracksBuilder
    {
        /// <summary>
        /// Gets Spotify catalog information about an artist’s top tracks by country.
        /// </summary>
        /// <param name="country">An ISO 3166-1 alpha-2 country code or the string <c>from_token</c>. Synonym for <c>country</c>.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TracksResponse> GetAsync(string country, CancellationToken cancellationToken = default(CancellationToken));
    }
}
