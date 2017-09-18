using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Messages;

namespace FluentSpotifyApi.Builder.Artists
{
    /// <summary>
    /// The builder for "artists?ids={ids}" endpoint.
    /// </summary>
    public interface IArtistsBuilder
    {
        /// <summary>
        /// Get Spotify catalog information for several artists based on their Spotify IDs.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<FullArtistsMessage> GetAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
