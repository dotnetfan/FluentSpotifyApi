using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Me.Player
{
    /// <summary>
    /// The builder for active device's playback.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Builder.Me.Player.IPlaybackBuilder" />
    public interface IActiveDevicePlaybackBuilder : IPlaybackBuilder
    {
        /// <summary>
        /// Get information about the user’s current playback state, including track, track progress, and active device.
        /// </summary>
        /// <param name="market">An ISO 3166-1 alpha-2 country code.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PlayingContext> GetAsync(string market = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get the object currently being played on the user’s Spotify account.
        /// </summary>
        /// <param name="market">An ISO 3166-1 alpha-2 country code.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PlayingObject> GetCurrentTrackAsync(string market = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Transfer playback to a new device and determine if it should start playing.
        /// </summary>
        /// <param name="deviceId">The ID of the device on which playback should be started/transferred.</param>
        /// <param name="play">Determines if playback should start playing.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task TransferAsync(string deviceId, bool play = false, CancellationToken cancellationToken = default(CancellationToken));
    }
}
