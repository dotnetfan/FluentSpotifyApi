using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token
{
    /// <summary>
    /// The client for getting OAuth tokens.
    /// </summary>
    public interface ISpotifyTokenClient
    {
        /// <summary>
        /// Gets tokens from authorization code.
        /// </summary>
        /// <param name="authorizationCode">The authorization code.</param>
        /// <param name="redirectUri">The redirect URI.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<FullTokensResponse> GetTokensFromAuthorizationResultAsync(string authorizationCode, Uri redirectUri, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets tokens from authorization code and code verifier.
        /// </summary>
        /// <param name="authorizationCode">The authorization code.</param>
        /// <param name="codeVerifier">The code verifier.</param>
        /// <param name="redirectUri">The redirect URI.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<FullTokensResponse> GetTokensFromAuthorizationResultAsync(string authorizationCode, string codeVerifier, Uri redirectUri, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets access token from refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<ScopedAccessTokenResponse> GetAccessTokenFromRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets access token from one-time refresh token. New refresh token is returned as part of the response.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<FullTokensResponse> GetNewTokensFromOneTimeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets access token from client credentials.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<AccessTokenResponse> GetAccessTokenFromClientCredentialsAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
