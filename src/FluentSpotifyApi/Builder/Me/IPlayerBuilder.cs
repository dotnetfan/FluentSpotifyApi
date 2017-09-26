using FluentSpotifyApi.Builder.Me.Player;

namespace FluentSpotifyApi.Builder.Me
{
    /// <summary>
    /// The builder for "me/player" endpoint.
    /// This endpoint is used for controlling Spotify devices.
    /// </summary>
    public interface IPlayerBuilder
    {
        /// <summary>
        /// Gets builder for "me/player/devices" endpoint.
        /// </summary>
        IDevicesBuilder Devices { get; }

        /// <summary>
        /// Gets builder for active device's playback.
        /// </summary>
        IActiveDevicePlaybackBuilder Playback();

        /// <summary>
        /// Gets builder for specified device's playback.
        /// </summary>
        /// <param name="deviceId">The device ID.</param>
        IDevicePlaybackBuilder Playback(string deviceId);
    }
}
