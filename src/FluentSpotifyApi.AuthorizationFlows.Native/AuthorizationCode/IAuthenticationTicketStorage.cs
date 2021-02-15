using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode
{
    /// <summary>
    /// The interface for storing authentication ticket securely.
    /// </summary>
    public interface IAuthenticationTicketStorage
    {
        /// <summary>
        /// Tries to get authentication ticket from the storage.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>(true, value) in case there is an authentication ticket in storage. (false, null) otherwise.</returns>
        Task<(bool IsSuccess, string Value)> TryGetAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Saves authentication ticket to the storage.
        /// </summary>
        /// <param name="value">The authentication ticket.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task SaveAsync(string value, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Removes authentication ticket from the storage.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><c>true</c> if an authentication has been found, <c>false</c> otherwise.</returns>
        Task<bool> RemoveAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
