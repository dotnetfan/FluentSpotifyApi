using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native
{
    /// <summary>
    /// Authorization code flow requires Client Secret in order to exchange authorization code for refresh token 
    /// and to get new access tokens from refresh token. Since it's not possible to store Client Secret securely
    /// in native apps, a back-end service that will have access to the Client Secret and will handle the token 
    /// exchange has to be created. This is the client interface for the back-end service which implementation 
    /// needs to be registered in <see cref="IServiceCollection"/>. The exceptions thrown by the interface methods
    /// are not shielded and are propagated back to the caller.
    /// </summary>
    public interface ITokenProxyClient
    {
        /// <summary>
        /// Gets the proxy authorization tokens from specified authorization code.
        /// </summary>
        /// <param name="authorizationCode">
        /// The authorization code that was returned from the <see cref="IAuthorizationProvider{T}.GetAsync(CancellationToken)"/> method.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<ProxyAuthorizationTokens> GetAuthorizationTokensAsync(string authorizationCode, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the proxy access token from specified authorization key.
        /// </summary>
        /// <param name="authorizationKey">
        /// The <see cref="ProxyAuthorizationTokens.AuthorizationKey"/> that was returned from the <see cref="GetAuthorizationTokensAsync"/> method.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<ProxyAccessToken> GetAccessTokenAsync(string authorizationKey, CancellationToken cancellationToken);
    }
}
