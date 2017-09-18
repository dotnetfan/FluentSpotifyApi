using System;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.Core;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.AuthorizationFlows.Core.Date;
using FluentSpotifyApi.AuthorizationFlows.Core.Extensions;
using FluentSpotifyApi.AuthorizationFlows.Core.Model;
using FluentSpotifyApi.Core.Internal.Extensions;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.AuthorizationFlows.ClientCredentials
{
    internal class AuthenticationTicketProvider : IAuthenticationTicketProvider, IDisposable
    {
        private readonly ITokenHttpClient tokenHttpClient;

        private readonly IDateTimeOffsetProvider dateTimeOffsetProvider;

        private readonly SemaphoreSlim semaphore;

        private AuthenticationTicket authenticationTicket;

        public AuthenticationTicketProvider(ITokenHttpClient tokenHttpClient, IDateTimeOffsetProvider dateTimeOffsetProvider)
        {
            this.tokenHttpClient = tokenHttpClient;
            this.dateTimeOffsetProvider = dateTimeOffsetProvider;

            this.semaphore = new SemaphoreSlim(1);
        }

        public async Task<IAuthenticationTicket> GetAsync(CancellationToken cancellationToken)
        {
            var result = await this.semaphore.ExecuteAsync(
                async innerCt =>
                {
                    if (this.authenticationTicket == null || !this.authenticationTicket.AccessToken.IsValid(this.dateTimeOffsetProvider))
                    {
                        await this.LoadAuthenticationTicketAsync(innerCt).ConfigureAwait(false);
                    }

                    return this.authenticationTicket;
                },
                cancellationToken).ConfigureAwait(false);

            return result;
        }

        public async Task<IAuthenticationTicket> RenewAccessTokenAsync(IAuthenticationTicket currentAuthenticationTicket, CancellationToken cancellationToken)
        {
            if (currentAuthenticationTicket == null)
            {
                throw new ArgumentNullException(nameof(currentAuthenticationTicket));
            }

            var result = await this.semaphore.ExecuteAsync(
                async innerCt =>
                {
                    if (this.authenticationTicket == null ||
                        !this.authenticationTicket.AccessToken.IsValid(this.dateTimeOffsetProvider) || 
                        this.authenticationTicket == currentAuthenticationTicket)
                    {
                        await this.LoadAuthenticationTicketAsync(innerCt).ConfigureAwait(false);
                    }

                    return this.authenticationTicket;
                },
                cancellationToken).ConfigureAwait(false);

            return result;
        }

        public void Dispose()
        {
            this.semaphore.Dispose();
        }

        private async Task LoadAuthenticationTicketAsync(CancellationToken cancellationToken)
        {
            this.authenticationTicket = null;

            var accessToken = (await this.tokenHttpClient.GetAccessTokenFromClientCredentialsAsync(cancellationToken).ConfigureAwait(false))
                .ToModelToken(this.dateTimeOffsetProvider);

            this.authenticationTicket = new AuthenticationTicket(accessToken);
        }

        private class AuthenticationTicket : IAuthenticationTicket
        {
            public AuthenticationTicket(AccessToken accessToken)
            {
                this.AccessToken = accessToken;
            }

            public IUser User => null;

            public AccessToken AccessToken { get; private set; }
        }
    }
}
