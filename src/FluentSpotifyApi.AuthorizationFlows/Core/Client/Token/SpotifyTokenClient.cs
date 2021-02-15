using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token.Exceptions;
using FluentSpotifyApi.Core.Options;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token
{
    internal class SpotifyTokenClient : ISpotifyTokenClient
    {
        private readonly ISpotifyTokenHttpClientFactory httpClientFactory;

        private readonly IOptionsProvider<ISpotifyTokenClientOptions> tokenClientOptionsProvider;

        public SpotifyTokenClient(ISpotifyTokenHttpClientFactory httpClientFactory, IOptionsProvider<ISpotifyTokenClientOptions> tokenClientOptionsProvider)
        {
            this.httpClientFactory = httpClientFactory;
            this.tokenClientOptionsProvider = tokenClientOptionsProvider;
        }

        public async Task<FullTokensResponse> GetTokensFromAuthorizationResultAsync(string authorizationCode, Uri redirectUri, CancellationToken cancellationToken)
        {
            var formParams = new Dictionary<string, object>
            {
                ["grant_type"] = "authorization_code",
                ["code"] = authorizationCode,
                ["redirect_uri"] = redirectUri
            };

            return await SpotifyHttpUtils.HandleTimeoutAsync<ISpotifyTokenClient, FullTokensResponse>(
                async innerCt =>
                {
                    using (var response = await this.httpClientFactory.CreateClient().PostAsync(null as Uri, this.GetFormUrlEncodedContent(formParams), innerCt).ConfigureAwait(false))
                    {
                        return await response.Content.ReadFromJsonAsync<FullTokensResponse>(cancellationToken: innerCt).ConfigureAwait(false);
                    }
                },
                cancellationToken).ConfigureAwait(false);
        }

        public async Task<FullTokensResponse> GetTokensFromAuthorizationResultAsync(string authorizationCode, string codeVerifier, Uri redirectUri, CancellationToken cancellationToken)
        {
            var tokenClientOptions = this.tokenClientOptionsProvider.Get();

            var formParams = new Dictionary<string, object>
            {
                ["client_id"] = tokenClientOptions.ClientId,
                ["grant_type"] = "authorization_code",
                ["code"] = authorizationCode,
                ["redirect_uri"] = redirectUri,
                ["code_verifier"] = codeVerifier
            };

            return await SpotifyHttpUtils.HandleTimeoutAsync<ISpotifyTokenClient, FullTokensResponse>(
                async innerCt =>
                {
                    using (var response = await this.httpClientFactory.CreateClient().PostAsync(null as Uri, this.GetFormUrlEncodedContent(formParams), innerCt).ConfigureAwait(false))
                    {
                        return await response.Content.ReadFromJsonAsync<FullTokensResponse>(cancellationToken: innerCt).ConfigureAwait(false);
                    }
                },
                cancellationToken).ConfigureAwait(false);
        }

        public async Task<ScopedAccessTokenResponse> GetAccessTokenFromRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var tokenClientOptions = this.tokenClientOptionsProvider.Get();

            var formParams = new Dictionary<string, object>
            {
                ["grant_type"] = "refresh_token",
                ["refresh_token"] = refreshToken
            };

            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.Content = this.GetFormUrlEncodedContent(formParams);
                request.Headers.Authorization = this.GetAuthorizationHeader(tokenClientOptions);

                try
                {
                    return await SpotifyHttpUtils.HandleTimeoutAsync<ISpotifyTokenClient, ScopedAccessTokenResponse>(
                        async innerCt =>
                        {
                            using (var response = await this.httpClientFactory.CreateClient().SendAsync(request, innerCt).ConfigureAwait(false))
                            {
                                return await response.Content.ReadFromJsonAsync<ScopedAccessTokenResponse>(cancellationToken: innerCt).ConfigureAwait(false);
                            }
                        },
                        cancellationToken).ConfigureAwait(false);
                }
                catch (SpotifyAuthenticationErrorException e) when (this.IsInvalidGrantException(e))
                {
                    throw new SpotifyInvalidRefreshTokenException(e.ErrorCode, e.Content, e.Error);
                }
            }
        }

        public async Task<FullTokensResponse> GetNewTokensFromOneTimeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var tokenClientOptions = this.tokenClientOptionsProvider.Get();

            var formParams = new Dictionary<string, object>
            {
                ["grant_type"] = "refresh_token",
                ["refresh_token"] = refreshToken,
                ["client_id"] = tokenClientOptions.ClientId,
            };

            try
            {
                return await SpotifyHttpUtils.HandleTimeoutAsync<ISpotifyTokenClient, FullTokensResponse>(
                    async innerCt =>
                    {
                        using (var response = await this.httpClientFactory.CreateClient().PostAsync(null as Uri, this.GetFormUrlEncodedContent(formParams), innerCt).ConfigureAwait(false))
                        {
                            return await response.Content.ReadFromJsonAsync<FullTokensResponse>(cancellationToken: innerCt).ConfigureAwait(false);
                        }
                    },
                    cancellationToken).ConfigureAwait(false);
            }
            catch (SpotifyAuthenticationErrorException e) when (this.IsInvalidGrantException(e))
            {
                throw new SpotifyInvalidRefreshTokenException(e.ErrorCode, e.Content, e.Error);
            }
        }

        public async Task<AccessTokenResponse> GetAccessTokenFromClientCredentialsAsync(CancellationToken cancellationToken)
        {
            var tokenClientOptions = this.tokenClientOptionsProvider.Get();

            var formParams = new Dictionary<string, object>
            {
                ["grant_type"] = "client_credentials"
            };

            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.Content = this.GetFormUrlEncodedContent(formParams);
                request.Headers.Authorization = this.GetAuthorizationHeader(tokenClientOptions);

                return await SpotifyHttpUtils.HandleTimeoutAsync<ISpotifyTokenClient, AccessTokenResponse>(
                    async innerCt =>
                    {
                        using (var response = await this.httpClientFactory.CreateClient().SendAsync(request, innerCt).ConfigureAwait(false))
                        {
                            return await response.Content.ReadFromJsonAsync<AccessTokenResponse>(cancellationToken: innerCt).ConfigureAwait(false);
                        }
                    },
                    cancellationToken).ConfigureAwait(false);
            }
        }

        private FormUrlEncodedContent GetFormUrlEncodedContent(IEnumerable<KeyValuePair<string, object>> formParams)
            => new FormUrlEncodedContent(formParams.Select(kvp => new KeyValuePair<string, string>(kvp.Key, SpotifyObjectUtils.ConvertToCanonicalString(kvp.Value))));

        private AuthenticationHeaderValue GetAuthorizationHeader(ISpotifyTokenClientOptions tokenClientOptions)
            => new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{tokenClientOptions.ClientId}:{tokenClientOptions.ClientSecret}")));

        private bool IsInvalidGrantException(SpotifyAuthenticationErrorException e)
            => e.ErrorCode == System.Net.HttpStatusCode.BadRequest &&
                "invalid_grant".Equals(e.Error?.Error, StringComparison.Ordinal);
    }
}
