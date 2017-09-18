using System;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Exceptions;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.Core.Exceptions;

namespace FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Server.Extensions
{
    /// <summary>
    /// The set of <see cref="ITokenHttpClient"/> extensions.
    /// </summary>
    public static class TokenHttpClientExtensions
    {
        /// <summary>
        /// Gets the access token or throw invalid refresh token exception.
        /// </summary>
        /// <param name="tokenHttpClient">The token HTTP client.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public static async Task<AccessTokenDto> GetAccessTokenOrThrowInvalidRefreshTokenExceptionAsync(
            this ITokenHttpClient tokenHttpClient, 
            string refreshToken,
            CancellationToken cancellationToken)
        {
            try
            {
                return await tokenHttpClient.GetAccessTokenFromRefreshTokenAsync(
                    refreshToken,
                    cancellationToken).ConfigureAwait(false);
            }
            catch (SpotifyHttpResponseWithAuthenticationErrorException e) when (
                e.ErrorCode == System.Net.HttpStatusCode.BadRequest &&
                "invalid_grant".Equals(e.Payload?.Error, StringComparison.OrdinalIgnoreCase))
            {
                throw new SpotifyInvalidRefreshTokenException(e);
            }
        }
    }
}
