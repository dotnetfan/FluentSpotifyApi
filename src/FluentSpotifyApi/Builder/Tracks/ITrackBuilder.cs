using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Tracks
{
    /// <summary>
    /// The builder for "tracks/{id}" endpoint.
    /// </summary>
    public interface ITrackBuilder
    {
        /// <summary>
        /// Gets builder for "audio-analysis/{id}" endpoint.
        /// </summary>
        ITrackAudioAnalysisBuilder AudioAnalysis { get; }

        /// <summary>
        /// Gets builder for "audio-features/{id}" endpoint.
        /// </summary>
        ITrackAudioFeaturesBuilder AudioFeatures { get; }

        /// <summary>
        /// Get Spotify catalog information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="market">An ISO 3166-1 alpha-2 country code.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<FullTrack> GetAsync(string market = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
