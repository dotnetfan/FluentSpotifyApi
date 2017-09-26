using System;
using System.Collections.Generic;
using System.Text;

namespace FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native
{
    /// <summary>
    /// The session state
    /// </summary>
    public enum SessionState
    {
        /// <summary>
        /// There is a session cached in memory after <see cref="IAuthenticationManager.RestoreSessionOrAuthorizeUserAsync(System.Threading.CancellationToken)"/>
        /// or <see cref="IAuthenticationManager.RestoreSessionAsync(System.Threading.CancellationToken)"/> has been called.
        /// The session can be removed by calling <see cref="IAuthenticationManager.RemoveSessionAsync(System.Threading.CancellationToken)"/>.
        /// </summary>
        CachedInMemory,

        /// <summary>
        /// There is a session stored in the local secure storage that hasn't been restored yet. The session can be restored by 
        /// calling <see cref="IAuthenticationManager.RestoreSessionOrAuthorizeUserAsync(System.Threading.CancellationToken)"/> or
        /// <see cref="IAuthenticationManager.RestoreSessionAsync(System.Threading.CancellationToken)"/>.
        /// The session can be removed by calling <see cref="IAuthenticationManager.RemoveSessionAsync(System.Threading.CancellationToken)"/>.
        /// </summary>
        StoredInLocalStorage,

        /// <summary>
        /// No session has been found. A new session can be created by calling 
        /// <see cref="IAuthenticationManager.RestoreSessionOrAuthorizeUserAsync(System.Threading.CancellationToken)"/>.
        /// </summary>
        NotFound,      
    }
}
