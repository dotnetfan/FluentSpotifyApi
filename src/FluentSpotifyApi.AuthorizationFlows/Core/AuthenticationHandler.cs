using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.Core.Model;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.AuthorizationFlows.Core
{
    internal class AuthenticationHandler : DelegatingHandler
    {
        private readonly IAuthenticationTicketProvider authenticationTicketProvider;

        public AuthenticationHandler(IAuthenticationTicketProvider authenticationTicketProvider)
        {
            this.authenticationTicketProvider = authenticationTicketProvider;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var ticket = await this.authenticationTicketProvider.GetAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            var response = await this.SendAuthenticatedAsync(request, ticket, cancellationToken).ConfigureAwait(false);
            if (await this.IsExpiredAccessTokenError(response, cancellationToken).ConfigureAwait(false))
            {
                ticket = await this.authenticationTicketProvider.RenewAccessTokenAsync(ticket, cancellationToken).ConfigureAwait(false);
                response = await this.SendAuthenticatedAsync(request, ticket, cancellationToken).ConfigureAwait(false);
            }

            return response;
        }

        private async Task<HttpResponseMessage> SendAuthenticatedAsync(HttpRequestMessage request, IAuthenticationTicket ticket, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ticket.AccessToken.Token);
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            return response;
        }

        private async Task<bool> IsExpiredAccessTokenError(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            if (response.StatusCode != HttpStatusCode.Unauthorized)
            {
                return false;
            }

            try
            {
#if (NETSTANDARD2_0)
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#else
                var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#endif
                var errorResponse = JsonSerializer.Deserialize<RegularErrorResponse>(content);

                // Spotify API does not have error codes
                return "The access token expired".Equals(errorResponse.Error.Message, StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
