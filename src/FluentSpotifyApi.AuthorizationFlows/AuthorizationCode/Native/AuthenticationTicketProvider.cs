using System;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Exceptions;
using FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native.Extensions;
using FluentSpotifyApi.AuthorizationFlows.Core;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.User;
using FluentSpotifyApi.AuthorizationFlows.Core.Date;
using FluentSpotifyApi.AuthorizationFlows.Core.Extensions;
using FluentSpotifyApi.AuthorizationFlows.Core.Model;
using FluentSpotifyApi.Core.Internal.Extensions;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native
{
    internal class AuthenticationTicketProvider : IAuthenticationTicketProvider, IAuthenticationManager, IDisposable
    {      
        private readonly IAuthenticationTicketStorage authenticationTicketStorage;

        private readonly IAuthorizationProvider<string> authorizationProvider;

        private readonly ITokenProxyClient tokenProxyClient;

        private readonly IUserHttpClient userHttpClient;

        private readonly IDateTimeOffsetProvider dateTimeOffsetProvider;

        private readonly SemaphoreSlim semaphore;

        private readonly object locker;

        private AuthenticationTicket authenticationTicket;

        public AuthenticationTicketProvider(
            IAuthenticationTicketStorage authenticationTicketStorage,
            IAuthorizationProvider<string> authorizationProvider, 
            ITokenProxyClient tokenProxyClient, 
            IUserHttpClient userHttpClient,
            IDateTimeOffsetProvider dateTimeOffsetProvider)
        {
            this.authenticationTicketStorage = authenticationTicketStorage;
            this.authorizationProvider = authorizationProvider;
            this.tokenProxyClient = tokenProxyClient;
            this.userHttpClient = userHttpClient;
            this.dateTimeOffsetProvider = dateTimeOffsetProvider;

            this.semaphore = new SemaphoreSlim(1);
            this.locker = new object();
        }

        public async Task<IAuthenticationTicket> GetAsync(CancellationToken cancellationToken)
        {
            var result = await this.semaphore.ExecuteAsync(
                async innerCt =>
                {
                    this.ValidateAuthenticationTicket();

                    if (!this.authenticationTicket.AccessToken.IsValid(this.dateTimeOffsetProvider))
                    {
                        await this.LoadNewAccessTokenAsync(innerCt).ConfigureAwait(false);
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
                    this.ValidateAuthenticationTicket();

                    if (!this.authenticationTicket.AccessToken.IsValid(this.dateTimeOffsetProvider) || this.authenticationTicket == currentAuthenticationTicket)
                    {
                        await this.LoadNewAccessTokenAsync(innerCt).ConfigureAwait(false);
                    }

                    return this.authenticationTicket;
                },
                cancellationToken).ConfigureAwait(false);

            return result;
        }
       
        public Task RestoreSessionOrAuthorizeUserAsync(CancellationToken cancellationToken)
        {
            return this.semaphore.ExecuteAsync(
                async innerCt =>
                {
                    if (this.authenticationTicket == null)
                    {
                        if (!(await this.TryRestoreAuthenticationTicketAsync(innerCt).ConfigureAwait(false)))
                        {
                            await this.LoadAuthenticationTicketAsync(innerCt).ConfigureAwait(false);
                        }
                    }
                },
                cancellationToken);
        }

        public Task RestoreSessionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.semaphore.ExecuteAsync(
                async innerCt =>
                {
                    if (this.authenticationTicket == null)
                    {
                        if (!(await this.TryRestoreAuthenticationTicketAsync(innerCt).ConfigureAwait(false)))
                        {
                            throw new SessionNotFoundException();
                        }
                    }
                }, 
                cancellationToken);
        }

        public Task<SessionState> GetSessionStateAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.semaphore.ExecuteAsync(
                async innerCt =>
                {
                    if (this.authenticationTicket != null)
                    {
                        return SessionState.CachedInMemory;
                    }
                    else if ((await this.authenticationTicketStorage.GetAsync(innerCt).ConfigureAwait(false)) != null)
                    {
                        return SessionState.StoredInLocalStorage;
                    }
                    else
                    {
                        return SessionState.NotFound;
                    }
                },
                cancellationToken);
        }

        public PrivateUser GetUser() => this.GetAuthenticationTicketSafe()?.User.CloneUsingJsonSerializer();

        public Task RemoveSessionAsync(CancellationToken cancellationToken)
        {
            return this.semaphore.ExecuteAsync(
                async innerCt =>
                {
                    await this.UnloadAuthenticationTicketAsync(innerCt).ConfigureAwait(false);
                },
                cancellationToken);
        }

        public void Dispose()
        {
            this.semaphore.Dispose();
        }

        private void ValidateAuthenticationTicket()
        {
            if (this.authenticationTicket == null)
            {
                throw new UnauthorizedAccessException();
            }
        }

        private async Task<bool> TryRestoreAuthenticationTicketAsync(CancellationToken cancellationToken)
        {
            var storedAuthenticationTicket = await this.authenticationTicketStorage.GetAsync(cancellationToken).ConfigureAwait(false);
            if (storedAuthenticationTicket != null)
            {
                this.UpdateAuthenticationTicketSafe(storedAuthenticationTicket);
                return true;
            }

            return false;
        }

        private async Task LoadAuthenticationTicketAsync(CancellationToken cancellationToken)
        {
            var authorizationCode = await this.authorizationProvider.GetAsync(cancellationToken).ConfigureAwait(false);
            var proxyAuthorizationTokens = await this.tokenProxyClient.GetAuthorizationTokensAsync(authorizationCode, cancellationToken).ConfigureAwait(false);
            var user = await this.userHttpClient.GetCurrentUserAsync(proxyAuthorizationTokens.AccessToken.Token, cancellationToken).ConfigureAwait(false);

            var result = new AuthenticationTicket(
                proxyAuthorizationTokens.AuthorizationKey,
                proxyAuthorizationTokens.AccessToken.ToModelToken(this.dateTimeOffsetProvider),
                user);

            await this.authenticationTicketStorage.SaveAsync(result, cancellationToken).ConfigureAwait(false);

            this.UpdateAuthenticationTicketSafe(result);
        }

        private async Task LoadNewAccessTokenAsync(CancellationToken cancellationToken)
        {
            this.UpdateAuthenticationTicketSafe(new AuthenticationTicket(
                this.authenticationTicket.AuthorizationKey, 
                null, 
                this.authenticationTicket.User));

            var proxyAccessToken = await this.tokenProxyClient
                .GetAccessTokenAsync(this.authenticationTicket.AuthorizationKey, cancellationToken)
                .ConfigureAwait(false);

            this.UpdateAuthenticationTicketSafe(new AuthenticationTicket(
                this.authenticationTicket.AuthorizationKey, 
                proxyAccessToken.ToModelToken(this.dateTimeOffsetProvider), 
                this.authenticationTicket.User));
        }

        private async Task UnloadAuthenticationTicketAsync(CancellationToken cancellationToken)
        {
            await this.authenticationTicketStorage.DeleteAsync(cancellationToken).ConfigureAwait(false);

            this.UpdateAuthenticationTicketSafe(null);
        }

        private AuthenticationTicket GetAuthenticationTicketSafe()
        {
            lock (this.locker)
            {
                return this.authenticationTicket;
            }
        }

        private void UpdateAuthenticationTicketSafe(AuthenticationTicket value)
        {
            lock (this.locker)
            {
                this.authenticationTicket = value;
            }
        }
    }
}
