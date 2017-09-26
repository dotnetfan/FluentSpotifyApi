using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Client;
using FluentSpotifyApi.Core.Options;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token
{
    /// <summary>
    /// The HTTP client for getting OAuth tokens.
    /// </summary>
    public class TokenHttpClient : ITokenHttpClient
    {
        private readonly IAuthorizationFlowsHttpClient authorizationFlowsHttpClient;

        private readonly IOptionsProvider<ITokenClientOptions> tokenClientOptionsProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenHttpClient"/> class.
        /// </summary>
        /// <param name="authorizationFlowsHttpClient">The authorization flows HTTP client.</param>
        /// <param name="tokenClientOptionsProvider">The token client options provider.</param>
        public TokenHttpClient(IAuthorizationFlowsHttpClient authorizationFlowsHttpClient, IOptionsProvider<ITokenClientOptions> tokenClientOptionsProvider)
        {
            this.authorizationFlowsHttpClient = authorizationFlowsHttpClient;
            this.tokenClientOptionsProvider = tokenClientOptionsProvider;
        }

        /// <summary>
        /// Gets authorization tokens from authorization code.
        /// </summary>
        /// <param name="authorizationCode">The authorization code.</param>
        /// <param name="redirectUrl">The redirect URL.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<AuthorizationTokensDto> GetAuthorizationTokensAsync(string authorizationCode, Uri redirectUrl, CancellationToken cancellationToken)
        {
            var tokenClientOptions = this.tokenClientOptionsProvider.Get();

            return this.authorizationFlowsHttpClient.SendAsync<AuthorizationTokensDto>(
                new UriParts { BaseUri = tokenClientOptions.TokenEndpoint },
                HttpMethod.Post,                
                new[] { this.GetAuthorizationHeader(tokenClientOptions) },
                new { grant_type = "authorization_code", code = authorizationCode, redirect_uri = redirectUrl },
                cancellationToken);
        }

        /// <summary>
        /// Gets access token from refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<AccessTokenDto> GetAccessTokenFromRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var tokenClientOptions = this.tokenClientOptionsProvider.Get();

            return this.authorizationFlowsHttpClient.SendAsync<AccessTokenDto>(
                new UriParts { BaseUri = tokenClientOptions.TokenEndpoint },
                HttpMethod.Post,                
                new[] { this.GetAuthorizationHeader(tokenClientOptions) },
                new { grant_type = "refresh_token", refresh_token = refreshToken },
                cancellationToken);
        }

        /// <summary>
        /// Gets access token from client credentials.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<AccessTokenDto> GetAccessTokenFromClientCredentialsAsync(CancellationToken cancellationToken)
        {
            var tokenClientOptions = this.tokenClientOptionsProvider.Get();

            return this.authorizationFlowsHttpClient.SendAsync<AccessTokenDto>(
                new UriParts { BaseUri = tokenClientOptions.TokenEndpoint },
                HttpMethod.Post,                
                new[] { this.GetAuthorizationHeader(tokenClientOptions) },
                new { grant_type = "client_credentials" },
                cancellationToken);
        }

        private KeyValuePair<string, string> GetAuthorizationHeader(ITokenClientOptions tokenClientOptions)
        {
            return new KeyValuePair<string, string>(
                "Authorization", 
                $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{tokenClientOptions.ClientId}:{tokenClientOptions.ClientSecret}"))}");
        }
    }
}
