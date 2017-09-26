using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Messages;

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
        Task<DevicesMessage> GetAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
