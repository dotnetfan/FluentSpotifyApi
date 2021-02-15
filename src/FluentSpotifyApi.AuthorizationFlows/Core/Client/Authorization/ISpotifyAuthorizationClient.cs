using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    /// <summary>
    /// The interface for authorizing Spotify users using Spotify Authorization Code Flow.
    /// </summary>
    public interface ISpotifyAuthorizationClient
    {
        /// <summary>
        /// Performs user authorization using Spotify Authorization Code Flow.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<AuthorizationResult> AuthorizeAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Performs user authorization using Spotify Authorization Code Flow with PKCE.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<PkceAuthorizationResult> AuthorizeWithPkceAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
