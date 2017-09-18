using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.Core.Model;
using FluentSpotifyApi.Core.Client;
using FluentSpotifyApi.Core.Exceptions;
using FluentSpotifyApi.Core.Internal.Extensions;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.AuthorizationFlows.Core
{
    internal class AuthenticatedHttpClient : IHttpClientWrapper
    {
        private readonly IHttpClientWrapper httpClientWrapper;

        private readonly IAuthenticationTicketProvider authenticationTicketProvider;

        public AuthenticatedHttpClient(IHttpClientWrapper httpClientWrapper, IAuthenticationTicketProvider authenticationTicketProvider)
        {
            this.httpClientWrapper = httpClientWrapper;
            this.authenticationTicketProvider = authenticationTicketProvider;
        }

        public async Task<TResult> SendAsync<TResult>(
            HttpRequest<TResult> httpRequest,
            CancellationToken cancellationToken)
        {
            Func<IAuthenticationTicket, CancellationToken, Task<TResult>> func = (authenticationTicket, innerCt) => this.httpClientWrapper.SendAsync(
                httpRequest
                    .ReplaceUriFromValuesBuilder(original => authenticationTicket.User != null ? original.AddValue<IUser>(authenticationTicket.User) : original)
                    .ReplaceRequestHeaders(original => original
                        .EmptyIfNull()
                        .Concat(new[] { new KeyValuePair<string, string>("Authorization", $"Bearer {authenticationTicket.AccessToken.Token}") })
                        .ToList()
                        .AsReadOnly()),   
                innerCt);

            var ticket = await this.authenticationTicketProvider.GetAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                return await func(ticket, cancellationToken).ConfigureAwait(false);
            }
            catch (SpotifyHttpResponseWithErrorCodeException e) when (e.ErrorCode == HttpStatusCode.Unauthorized)
            {
                ticket = await this.authenticationTicketProvider.RenewAccessTokenAsync(ticket, cancellationToken).ConfigureAwait(false);
                return await func(ticket, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
