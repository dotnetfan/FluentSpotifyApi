using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Tracks;

namespace FluentSpotifyApi.Builder.Tracks
{
    /// <summary>
    /// The builder for "tracks?ids={ids}" endpoint.
    /// </summary>
    public interface ITracksBuilder
    {
        /// <summary>
        /// Gets builder for "audio-features?ids={ids}" endpoint.
        /// </summary>
        ITracksAudioFeaturesBuilder AudioFeatures { get; }

        /// <summary>
        /// Gets Spotify catalog information for multiple tracks based on their Spotify IDs.
        /// </summary>
        /// <param name="market">An ISO 3166-1 alpha-2 country code or the string <c>from_token</c>. Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<TracksResponse> GetAsync(string market = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
