using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Albums;

namespace FluentSpotifyApi.Builder.Albums
{
    /// <summary>
    /// The builder for "albums?ids={ids}" endpoint.
    /// </summary>
    public interface IAlbumsBuilder
    {
        /// <summary>
        /// Gets Spotify catalog information for multiple albums identified by their Spotify IDs.
        /// </summary>
        /// <param name="market">An ISO 3166-1 alpha-2 country code or the string <c>from_token</c>. Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<AlbumsResponse> GetAsync(string market = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
