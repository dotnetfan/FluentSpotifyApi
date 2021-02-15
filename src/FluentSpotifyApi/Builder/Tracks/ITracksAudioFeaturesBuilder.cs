using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Audio;

namespace FluentSpotifyApi.Builder.Tracks
{
    /// <summary>
    /// The builder for "audio-features?ids={ids}" endpoint.
    /// </summary>
    public interface ITracksAudioFeaturesBuilder
    {
        /// <summary>
        /// Gets audio features for multiple tracks based on their Spotify IDs.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<AudioFeaturesListResponse> GetAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
