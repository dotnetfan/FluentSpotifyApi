using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.Exceptions;
using FluentSpotifyApi.Core.Internal.Extensions;
using FluentSpotifyApi.Core.Options;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    /// <summary>
    /// The class for getting authorization from the Spotify Accounts Service.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization.IAuthorizationProvider{T}" />
    public class AuthorizationProvider<T> : IAuthorizationProvider<T>
    {
        private readonly IAuthorizationInteractionClient<T> authorizationInteractionClient;

        private readonly ICsrfTokenProvider csfrTokenProvider;

        private readonly IAuthorizationCallbackUriProvider authorizationCallbackUrlProvider;

        private readonly IOptionsProvider<IAuthorizationOptions> authorizationOptionsProvider;

        private readonly string responseType;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationProvider{T}" /> class.
        /// </summary>
        /// <param name="authorizationInteractionClient">The authorization interaction service.</param>
        /// <param name="csfrTokenProvider">The CSFR token provider.</param>
        /// <param name="authorizationCallbackUrlProvider">The authentication callback URL provider.</param>
        /// <param name="authorizationOptionsProvider">The authorization options provider.</param>
        /// <param name="responseType">Type of the response.</param>
        public AuthorizationProvider(
            IAuthorizationInteractionClient<T> authorizationInteractionClient, 
            ICsrfTokenProvider csfrTokenProvider, 
            IAuthorizationCallbackUriProvider authorizationCallbackUrlProvider, 
            IOptionsProvider<IAuthorizationOptions> authorizationOptionsProvider,
            string responseType)
        {
            this.authorizationInteractionClient = authorizationInteractionClient;
            this.authorizationCallbackUrlProvider = authorizationCallbackUrlProvider;
            this.csfrTokenProvider = csfrTokenProvider;
            this.authorizationCallbackUrlProvider = authorizationCallbackUrlProvider;
            this.authorizationOptionsProvider = authorizationOptionsProvider;
            this.responseType = responseType;
        }

        /// <summary>
        /// Gets the authorization from the Spotify Accounts Service.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="SpotifyAuthorizationException">
        /// Thrown when the authorization fails.
        /// </exception>
        public async Task<T> GetAsync(CancellationToken cancellationToken)
        {
            var authorizationOptions = this.authorizationOptionsProvider.Get();

            var originalCsrfToken = this.csfrTokenProvider.Get();
            var callbackUrl = this.authorizationCallbackUrlProvider.Get();

            var parameters = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("client_id", authorizationOptions.ClientId),
                    new KeyValuePair<string, object>("response_type", this.responseType),
                    new KeyValuePair<string, object>("scope", string.Join(" ", authorizationOptions.GetScopesList())),
                    new KeyValuePair<string, object>("show_dialog", authorizationOptions.ShowDialog),
                    new KeyValuePair<string, object>("state", originalCsrfToken),
                    new KeyValuePair<string, object>("redirect_uri", callbackUrl)
                };

            var authenticationUrlBuilder = new UriBuilder(authorizationOptions.AuthorizationEndpoint)
            {
                Query = parameters.GetQueryString()
            };

            var response = await this.authorizationInteractionClient.AuthorizeAsync(
                authenticationUrlBuilder.Uri, 
                callbackUrl, 
                cancellationToken).ConfigureAwait(false);

            if (!string.Equals(response.CsrfToken, originalCsrfToken, StringComparison.Ordinal))
            {
                throw new SpotifyAuthorizationException("Invalid state has been returned from the Spotify Accounts Service.");
            }

            return response.Payload;
        }
    }
}
