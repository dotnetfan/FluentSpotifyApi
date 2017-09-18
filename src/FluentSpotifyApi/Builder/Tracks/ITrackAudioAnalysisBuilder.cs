using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Audio;

namespace FluentSpotifyApi.Builder.Tracks
{
    /// <summary>
    /// The builder for "audio-analysis/{id}" endpoint.
    /// </summary>
    public interface ITrackAudioAnalysisBuilder
    {
        /// <summary>
        /// Get a detailed audio analysis for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<AudioAnalysis> GetAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
