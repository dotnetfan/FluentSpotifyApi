using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Player;

namespace FluentSpotifyApi.Builder.Me.Player
{
    /// <summary>
    /// The builder for "me/player/devices" endpoint.
    /// </summary>
    public interface IDevicesBuilder
    {
        /// <summary>
        /// Get information about a user’s available devices.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<DevicesResponse> GetAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
