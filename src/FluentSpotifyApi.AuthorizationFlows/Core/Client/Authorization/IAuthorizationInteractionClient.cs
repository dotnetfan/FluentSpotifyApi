using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    /// <summary>
    /// The authorization interaction service.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAuthorizationInteractionClient<T>
    {
        /// <summary>
        /// Gets the authorization from the Spotify Accounts Service.
        /// </summary>
        /// <param name="authorizationUrl">The authorization URL.</param>
        /// <param name="callbackUrl">The callback URL.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<AuthorizationResponse<T>> AuthorizeAsync(Uri authorizationUrl, Uri callbackUrl, CancellationToken cancellationToken);
    }
}
