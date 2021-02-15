using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode
{
    /// <summary>
    /// The authentication manager for Spotify Authorization Code Flow for native apps.
    /// The <see cref="RestoreSessionOrAuthorizeUserAsync"/> or <see cref="RestoreSessionAsync"/> has to be called
    /// before any Spotify Web API request is made. Otherwise <see cref="Core.Exceptions.SpotifyUnauthenticatedException"/> is thrown.
    /// </summary>
    public interface IAuthenticationManager
    {
        /// <summary>
        /// Performs Spotify Authorization Code Flow (i.e. executes user authorization and gets authorization tokens and user information from Spotify Service),
        /// stores session (i.e. authorization tokens and user information) in the local secure storage and caches it in memory. In case there is
        /// already a session stored in the local secure storage, the authorization code flow is not executed and session is restored
        /// from the storage instead. This method does nothing when there is already a session cached in memory.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<RestoreSessionOrAuthorizeUserResult> RestoreSessionOrAuthorizeUserAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Restores session from the local secure storage. This method does nothing when there is already a session cached in memory.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="Exceptions.SpotifySessionNotFoundException">
        /// Thrown when there is no session stored in the local secure storage.
        /// </exception>
        /// <returns><c>false</c> if there is already a session cached in memory, <c>true</c> otherwise.</returns>
        Task<bool> RestoreSessionAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets session state.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<SessionState> GetSessionStateAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets user claims constructed from <see cref="SpotifyAuthorizationCodeFlowOptions.UserClaimResolvers"/>.
        /// </summary>
        /// <exception cref="Core.Exceptions.SpotifyUnauthenticatedException">
        /// Thrown when <see cref="RestoreSessionOrAuthorizeUserAsync"/> or <see cref="RestoreSessionAsync"/> has not been called yet.
        /// </exception>
        UserClaims GetUserClaims();

        /// <summary>
        /// Removes session from the local secure storage and from memory.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><c>true</c> if a session was found, <c>false</c> otherwise.</returns>
        Task<bool> RemoveSessionAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
