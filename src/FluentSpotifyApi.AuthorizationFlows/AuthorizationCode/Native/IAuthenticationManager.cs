using System;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.Exceptions;
using FluentSpotifyApi.Core.Exceptions;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native
{
    /// <summary>
    /// The authentication manager for authorization code flow for native apps.
    /// The <see cref="RestoreSessionOrAuthorizeUserAsync"/> has to be called before
    /// any Spotify Web API request is made. Otherwise <see cref="UnauthorizedAccessException"/> is thrown.
    /// </summary>
    public interface IAuthenticationManager
    {
        /// <summary>
        /// Executes authorization code flow (i.e. executes user authorization and gets authorization tokens and user information from Spotify Service), 
        /// stores authentication ticket in the local secure storage and caches it in memory. In case there is already an authentication ticket 
        /// stored in local secure storage, the authorization code flow is not executed and authentication ticket is loaded from the storage instead. 
        /// This method does nothing when there is already an authentication ticket cached in memory.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="SpotifyAuthorizationException">
        /// Thrown when user authorization fails.
        /// </exception>
        /// <exception cref="SpotifyServiceException">
        /// Thrown when retrieval of user information fails. 
        /// </exception>
        /// <exception cref="Exception">
        /// Any exception thrown by <see cref="ITokenProxyClient.GetAuthorizationTokensAsync(string, CancellationToken)"/>.
        /// </exception>
        Task RestoreSessionOrAuthorizeUserAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets information about current user. 
        /// </summary>
        /// <returns>
        /// Information about current user if <see cref="RestoreSessionOrAuthorizeUserAsync"/> has been called,
        /// <c>null</c> otherwise.</returns>
        PrivateUser GetUser();

        /// <summary>
        /// Removes authentication ticket from local secure storage and from memory.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task RemoveSessionAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
