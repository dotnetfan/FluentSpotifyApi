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
    /// The <see cref="RestoreSessionOrAuthorizeUserAsync"/> or <see cref="RestoreSessionAsync"/> has to be called 
    /// before any Spotify Web API request is made. Otherwise <see cref="UnauthorizedAccessException"/> is thrown.
    /// </summary>
    public interface IAuthenticationManager
    {
        /// <summary>
        /// Executes authorization code flow (i.e. executes user authorization and gets authorization tokens and user information from Spotify Service), 
        /// stores session (i.e. authorization tokens and user information) in the local secure storage and caches it in memory. In case there is 
        /// already a session stored in the local secure storage, the authorization code flow is not executed and session is restored 
        /// from the storage instead. This method does nothing when there is already a session cached in memory.
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
        /// Restores session from the local secure storage. This method does nothing when there is already a session cached in memory.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="Exceptions.SessionNotFoundException">
        /// Thrown when there is no session stored in the local secure storage.
        /// </exception>
        Task RestoreSessionAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets session state.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SessionState> GetSessionStateAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets information about current user. 
        /// </summary>
        /// <returns>
        /// Information about current user if <see cref="RestoreSessionOrAuthorizeUserAsync"/> or <see cref="RestoreSessionAsync"/> has been called,
        /// <c>null</c> otherwise.</returns>
        PrivateUser GetUser();
      
        /// <summary>
        /// Removes session from the local secure storage and from memory.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task RemoveSessionAsync(CancellationToken cancellationToken = default(CancellationToken));        
    }
}
