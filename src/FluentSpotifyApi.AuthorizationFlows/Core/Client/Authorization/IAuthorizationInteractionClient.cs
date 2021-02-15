using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    /// <summary>
    /// The authorization interaction service.
    /// </summary>
    public interface IAuthorizationInteractionClient
    {
        /// <summary>
        /// Gets the authorization URI from the Spotify Accounts Service.
        /// </summary>
        /// <param name="authorizationUri">The authorization URI.</param>
        /// <param name="redirectUri">The redirect URI.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Uri> AuthorizeAsync(Uri authorizationUri, Uri redirectUri, CancellationToken cancellationToken = default(CancellationToken));
    }
}
