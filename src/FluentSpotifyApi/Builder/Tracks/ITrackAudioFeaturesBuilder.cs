using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Audio;

namespace FluentSpotifyApi.Builder.Tracks
{
    /// <summary>
    /// The builder for "audio-features/{id}" endpoint.
    /// </summary>
    public interface ITrackAudioFeaturesBuilder
    {
        /// <summary>
        /// Get audio feature information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<AudioFeatures> GetAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
