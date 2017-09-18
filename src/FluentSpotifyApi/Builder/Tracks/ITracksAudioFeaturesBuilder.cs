using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Messages;

namespace FluentSpotifyApi.Builder.Tracks
{
    /// <summary>
    /// The builder for "audio-features?ids={ids}" endpoint.
    /// </summary>
    public interface ITracksAudioFeaturesBuilder
    {
        /// <summary>
        /// Get audio features for multiple tracks based on their Spotify IDs.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<AudioFeaturesListMessage> GetAsync(CancellationToken cancellationToken = default(CancellationToken));        
    }
}
