using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token
{
    /// <summary>
    /// The HTTP client for getting OAuth tokens.
    /// </summary>
    public interface ITokenHttpClient
    {
        /// <summary>
        /// Gets authorization tokens from authorization code.
        /// </summary>
        /// <param name="authorizationCode">The authorization code.</param>
        /// <param name="redirectUrl">The redirect URL.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<AuthorizationTokensDto> GetAuthorizationTokensAsync(string authorizationCode, Uri redirectUrl, CancellationToken cancellationToken);

        /// <summary>
        /// Gets access token from refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<AccessTokenDto> GetAccessTokenFromRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);

        /// <summary>
        /// Gets access token from client credentials.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<AccessTokenDto> GetAccessTokenFromClientCredentialsAsync(CancellationToken cancellationToken);        
    }
}
