using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization.Exceptions;
using FluentSpotifyApi.Core.Options;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    internal class SpotifyAuthorizationClient : ISpotifyAuthorizationClient
    {
        private readonly IOptionsProvider<ISpotifyAuthorizationClientOptions> authorizationOptionsProvider;

        private readonly ICsrfTokenProvider csfrTokenProvider;

        private readonly ICodeVerifierProvider codeVerifierProvider;

        private readonly IAuthorizationRedirectUriProvider authorizationRedirectUriProvider;

        private readonly IAuthorizationInteractionClient authorizationInteractionClient;

        public SpotifyAuthorizationClient(
            IOptionsProvider<ISpotifyAuthorizationClientOptions> authorizationOptionsProvider,
            ICsrfTokenProvider csfrTokenProvider,
            ICodeVerifierProvider codeVerifierProvider,
            IAuthorizationRedirectUriProvider authorizationCallbackUriProvider,
            IAuthorizationInteractionClient authorizationInteractionClient)
        {
            this.authorizationOptionsProvider = authorizationOptionsProvider;
            this.csfrTokenProvider = csfrTokenProvider;
            this.codeVerifierProvider = codeVerifierProvider;
            this.authorizationRedirectUriProvider = authorizationCallbackUriProvider;
            this.authorizationInteractionClient = authorizationInteractionClient;
        }

        public async Task<AuthorizationResult> AuthorizeAsync(CancellationToken cancellationToken)
        {
            var authorizationResult = await this.AuthorizeInnerAsync(null, cancellationToken).ConfigureAwait(false);

            return authorizationResult;
        }

        public async Task<PkceAuthorizationResult> AuthorizeWithPkceAsync(CancellationToken cancellationToken)
        {
            var codeVerifier = this.codeVerifierProvider.Get();
            var codeChallenge = this.GetCodeChallenge(codeVerifier);

            var authorizationResult = await this.AuthorizeInnerAsync(codeChallenge, cancellationToken).ConfigureAwait(false);

            return new PkceAuthorizationResult(authorizationResult.AuthorizationCode, codeVerifier, authorizationResult.RedirectUri);
        }

        private async Task<AuthorizationResult> AuthorizeInnerAsync(string codeChallenge, CancellationToken cancellationToken)
        {
            var authorizationOptions = this.authorizationOptionsProvider.Get();
            var originalCsrfToken = this.csfrTokenProvider.Get();
            var redirectUri = this.authorizationRedirectUriProvider.Get();

            var queryParams = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("client_id", authorizationOptions.ClientId),
                    new KeyValuePair<string, object>("response_type", "code"),
                    new KeyValuePair<string, object>("scope", string.Join(" ", authorizationOptions.Scopes ?? Enumerable.Empty<string>())),
                    new KeyValuePair<string, object>("show_dialog", authorizationOptions.ShowDialog),
                    new KeyValuePair<string, object>("state", originalCsrfToken),
                    new KeyValuePair<string, object>("redirect_uri", redirectUri)
                };

            if (codeChallenge != null)
            {
                queryParams.Add(new KeyValuePair<string, object>("code_challenge_method", "S256"));
                queryParams.Add(new KeyValuePair<string, object>("code_challenge", codeChallenge));
            }

            var authenticationUriBuilder = new UriBuilder(authorizationOptions.AuthorizationEndpoint)
            {
                Query = SpotifyUriUtils.GetQueryString(queryParams)
            };

            var enrichedRedirectUri = await this.authorizationInteractionClient.AuthorizeAsync(
                authenticationUriBuilder.Uri,
                redirectUri,
                cancellationToken).ConfigureAwait(false);

            var redirectQueryParams = HttpUtility.ParseQueryString(enrichedRedirectUri.Query);

            var states = redirectQueryParams.GetValues("state") ?? Array.Empty<string>();
            if (states.Length != 1 || states.First() != originalCsrfToken)
            {
                throw new SpotifyAuthorizationException("Invalid state has been returned from the Spotify Accounts Service.");
            }

            var error = redirectQueryParams.GetValues("error")?.FirstOrDefault();
            if (!string.IsNullOrEmpty(error))
            {
                throw new SpotifyAuthorizationException($"Error '{error}' has been returned from the Spotify Accounts Service.");
            }

            var codes = redirectQueryParams.GetValues("code") ?? Array.Empty<string>();
            if (codes.Length != 1 || string.IsNullOrEmpty(codes.First()))
            {
                throw new SpotifyAuthorizationException("An invalid authorization code has been returned from the Spotify Accounts Service.");
            }

            return new AuthorizationResult(codes.First(), redirectUri);
        }

        private string GetCodeChallenge(string codeVerifier)
        {
            byte[] hash;
            using (var sha256 = SHA256.Create())
            {
                hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
            }

            return SpotifyUriUtils.ConvertToBase64UriString(hash);
        }
    }
}