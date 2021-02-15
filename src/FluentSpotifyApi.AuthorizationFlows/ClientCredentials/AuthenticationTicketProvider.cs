using System;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.Core;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.AuthorizationFlows.Core.Extensions;
using FluentSpotifyApi.AuthorizationFlows.Core.Model;
using FluentSpotifyApi.AuthorizationFlows.Core.Time;
using FluentSpotifyApi.Core.User;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.AuthorizationFlows.ClientCredentials
{
    internal class AuthenticationTicketProvider : IAuthenticationTicketProvider, IDisposable
    {
        private readonly ISpotifyTokenClient tokenHttpClient;

        private readonly IClock clock;

        private readonly SemaphoreSlim semaphore;

        private AuthenticationTicket authenticationTicket;

        public AuthenticationTicketProvider(ISpotifyTokenClient tokenClient, IClock clock)
        {
            this.tokenHttpClient = tokenClient;
            this.clock = clock;

            this.semaphore = new SemaphoreSlim(1);
        }

        public async Task<IAuthenticationTicket> GetAsync(bool ensureValidAccessToken, CancellationToken cancellationToken)
        {
            var result = await SpotifySemaphoreUtils.ExecuteAsync(
                this.semaphore,
                async innerCt =>
                {
                    if (this.authenticationTicket == null || (ensureValidAccessToken && this.authenticationTicket.AccessToken.IsExpired(this.clock)))
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
            SpotifyArgumentAssertUtils.ThrowIfNull(currentAuthenticationTicket, nameof(currentAuthenticationTicket));

            var result = await SpotifySemaphoreUtils.ExecuteAsync(
                this.semaphore,
                async innerCt =>
                {
                    if (this.authenticationTicket == null ||
                        this.authenticationTicket.AccessToken.IsExpired(this.clock) ||
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
            var accessToken = (await this.tokenHttpClient.GetAccessTokenFromClientCredentialsAsync(cancellationToken).ConfigureAwait(false))
                .GetAccessTokenModel(this.clock);

            this.authenticationTicket = new AuthenticationTicket(accessToken);
        }

        private class AuthenticationTicket : IAuthenticationTicket
        {
            public AuthenticationTicket(AccessToken accessToken)
            {
                this.AccessToken = accessToken;
            }

            public IUser User => null;

            public AccessToken AccessToken { get; }
        }
    }
}
