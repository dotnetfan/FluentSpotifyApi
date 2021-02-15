using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Player;

namespace FluentSpotifyApi.Builder.Me.Player
{
    /// <summary>
    /// The builder for active device's playback.
    /// </summary>
    /// <seealso cref="IPlaybackBuilder"/>
    public interface IActiveDevicePlaybackBuilder : IPlaybackBuilder
    {
        /// <summary>
        /// Gets information about the user’s current playback state, including track or episode, progress, and active device.
        /// </summary>
        /// <param name="market">An ISO 3166-1 alpha-2 country code or the string <c>from_token</c>. Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<CurrentlyPlayingContext> GetCurrentAsync(string market = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the object currently being played on the user’s Spotify account.
        /// </summary>
        /// <param name="market">An ISO 3166-1 alpha-2 country code or the string <c>from_token</c>. Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<CurrentlyPlayingItem> GetCurrentItemAsync(string market = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Transfers playback to a new device and determine if it should start playing.
        /// </summary>
        /// <param name="deviceId">The ID of the device on which playback should be started/transferred.</param>
        /// <param name="play"><c>true</c>: ensure playback happens on new device. <c>false</c> or not provided: keep the current playback state.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task TransferAsync(string deviceId, bool? play = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
