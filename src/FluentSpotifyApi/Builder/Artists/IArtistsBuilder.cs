using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Artists;

namespace FluentSpotifyApi.Builder.Artists
{
    /// <summary>
    /// The builder for "artists?ids={ids}" endpoint.
    /// </summary>
    public interface IArtistsBuilder
    {
        /// <summary>
        /// Gets Spotify catalog information for several artists based on their Spotify IDs.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<ArtistsResponse> GetAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
