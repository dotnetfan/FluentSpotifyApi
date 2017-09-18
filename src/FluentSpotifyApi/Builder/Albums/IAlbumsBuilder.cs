using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Messages;

namespace FluentSpotifyApi.Builder.Albums
{
    /// <summary>
    /// The builder for "albums?ids={ids}" endpoint.
    /// </summary>
    public interface IAlbumsBuilder
    {
        /// <summary>
        /// Get Spotify catalog information for multiple albums identified by their Spotify IDs.
        /// </summary>
        /// <param name="market">An ISO 3166-1 alpha-2 country code.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<FullAlbumsMessage> GetAsync(string market = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
