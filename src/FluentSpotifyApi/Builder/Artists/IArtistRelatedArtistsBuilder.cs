using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Messages;

namespace FluentSpotifyApi.Builder.Artists
{
    /// <summary>
    /// The builder for "artists/{id}/related-artists" endpoint.
    /// </summary>
    public interface IArtistRelatedArtistsBuilder
    {
        /// <summary>
        /// Get Spotify catalog information about artists similar to a given artist. 
        /// Similarity is based on analysis of the Spotify community’s listening history.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<FullArtistsMessage> GetAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
