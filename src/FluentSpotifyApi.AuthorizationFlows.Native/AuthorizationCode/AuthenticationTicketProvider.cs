using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.Core;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.User;
using FluentSpotifyApi.AuthorizationFlows.Core.Exceptions;
using FluentSpotifyApi.AuthorizationFlows.Core.Extensions;
using FluentSpotifyApi.AuthorizationFlows.Core.Model;
using FluentSpotifyApi.AuthorizationFlows.Core.Time;
using FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode.Exceptions;
using FluentSpotifyApi.Core.Options;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode
{
    internal class AuthenticationTicketProvider : IAuthenticationTicketProvider, IAuthenticationManager, IDisposable
    {
        private readonly ISpotifyAuthorizationClient authorizationClient;

        private readonly ISpotifyUserClient userClient;

        private readonly ISpotifyTokenClient tokenClient;

        private readonly IAuthenticationTicketRepository authenticationTicketRepository;

        private readonly IClock clock;

        private readonly IOptionsProvider<SpotifyAuthorizationCodeFlowOptions> optionsProvider;

        private readonly SemaphoreSlim semaphore;

        private readonly object locker;

        private AuthenticationTicket authenticationTicket;

        public AuthenticationTicketProvider(
            ISpotifyAuthorizationClient authorizationClient,
            ISpotifyUserClient userClient,
            ISpotifyTokenClient tokenClient,
            IAuthenticationTicketRepository authenticationTicketRepository,
            IClock clock,
            IOptionsProvider<SpotifyAuthorizationCodeFlowOptions> optionsProvider)
        {
            this.authorizationClient = authorizationClient;
            this.userClient = userClient;
            this.tokenClient = tokenClient;
            this.authenticationTicketRepository = authenticationTicketRepository;
            this.clock = clock;
            this.optionsProvider = optionsProvider;
            this.semaphore = new SemaphoreSlim(1);
            this.locker = new object();
        }

        public async Task<IAuthenticationTicket> GetAsync(bool ensureValidAccessToken, CancellationToken cancellationToken)
        {
            var result = await SpotifySemaphoreUtils.ExecuteAsync(
                this.semaphore,
                async innerCt =>
                {
                    if (this.authenticationTicket == null)
                    {
                        throw new SpotifyUnauthenticatedException();
                    }

                    if (ensureValidAccessToken && this.authenticationTicket.AccessToken.IsExpired(this.clock))
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
            SpotifyArgumentAssertUtils.ThrowIfNull(currentAuthenticationTicket, nameof(currentAuthenticationTicket));

            var result = await SpotifySemaphoreUtils.ExecuteAsync(
                this.semaphore,
                async innerCt =>
                {
                    if (this.authenticationTicket == null)
                    {
                        throw new SpotifyUnauthenticatedException();
                    }

                    if (this.authenticationTicket.AccessToken.IsExpired(this.clock) || this.authenticationTicket == currentAuthenticationTicket)
                    {
                        await this.LoadNewAccessTokenAsync(innerCt).ConfigureAwait(false);
                    }

                    return this.authenticationTicket;
                },
                cancellationToken).ConfigureAwait(false);

            return result;
        }

        public async Task<RestoreSessionOrAuthorizeUserResult> RestoreSessionOrAuthorizeUserAsync(CancellationToken cancellationToken)
        {
            return await SpotifySemaphoreUtils.ExecuteAsync(
                this.semaphore,
                async innerCt =>
                {
                    if (this.authenticationTicket == null)
                    {
                        if (!(await this.TryRestoreAuthenticationTicketAsync(innerCt).ConfigureAwait(false)))
                        {
                            await this.LoadAuthenticationTicketAsync(innerCt).ConfigureAwait(false);
                            return RestoreSessionOrAuthorizeUserResult.PerfomedUserAuthorization;
                        }

                        return RestoreSessionOrAuthorizeUserResult.RestoredSessionFromLocalStorage;
                    }

                    return RestoreSessionOrAuthorizeUserResult.NoAction;
                },
                cancellationToken).ConfigureAwait(false);
        }

        public async Task<bool> RestoreSessionAsync(CancellationToken cancellationToken)
        {
            return await SpotifySemaphoreUtils.ExecuteAsync(
                this.semaphore,
                async innerCt =>
                {
                    if (this.authenticationTicket == null)
                    {
                        if (!(await this.TryRestoreAuthenticationTicketAsync(innerCt).ConfigureAwait(false)))
                        {
                            throw new SpotifySessionNotFoundException();
                        }

                        return true;
                    }

                    return false;
                },
                cancellationToken).ConfigureAwait(false);
        }

        public async Task<SessionState> GetSessionStateAsync(CancellationToken cancellationToken)
        {
            return await SpotifySemaphoreUtils.ExecuteAsync(
                this.semaphore,
                async innerCt =>
                {
                    if (this.authenticationTicket != null)
                    {
                        return SessionState.CachedInMemory;
                    }
                    else if ((await this.authenticationTicketRepository.GetAsync(innerCt).ConfigureAwait(false)) != null)
                    {
                        return SessionState.StoredInLocalStorage;
                    }
                    else
                    {
                        return SessionState.NotFound;
                    }
                },
                cancellationToken).ConfigureAwait(false);
        }

        public UserClaims GetUserClaims()
        {
            var authenticationTicket = this.GetAuthenticationTicketSafe();
            if (authenticationTicket == null)
            {
                throw new SpotifyUnauthenticatedException();
            }

            return authenticationTicket.UserClaims;
        }

        public async Task<bool> RemoveSessionAsync(CancellationToken cancellationToken)
        {
            return await SpotifySemaphoreUtils.ExecuteAsync(
                this.semaphore,
                async innerCt =>
                {
                    return await this.UnloadAuthenticationTicketAsync(innerCt).ConfigureAwait(false);
                },
                cancellationToken).ConfigureAwait(false);
        }

        public void Dispose()
        {
            this.semaphore.Dispose();
        }

        private async Task<bool> TryRestoreAuthenticationTicketAsync(CancellationToken cancellationToken)
        {
            var storedAuthenticationTicket = await this.authenticationTicketRepository.GetAsync(cancellationToken).ConfigureAwait(false);
            if (storedAuthenticationTicket != null)
            {
                this.UpdateAuthenticationTicketSafe(storedAuthenticationTicket);
                return true;
            }

            return false;
        }

        private async Task LoadAuthenticationTicketAsync(CancellationToken cancellationToken)
        {
            var authorizationResult = await this.authorizationClient.AuthorizeWithPkceAsync(cancellationToken).ConfigureAwait(false);
            var tokens = await this.tokenClient.GetTokensFromAuthorizationResultAsync(
                authorizationResult.AuthorizationCode,
                authorizationResult.CodeVerifier,
                authorizationResult.RedirectUri,
                cancellationToken).ConfigureAwait(false);

            var user = await this.userClient.GetCurrentUserAsync(tokens.AccessToken, cancellationToken).ConfigureAwait(false);

            var options = this.optionsProvider.Get();
            var claimsFromResolvers = options.UserClaimResolvers
                .Select(r => (r.ClaimType, ClaimValue: r.Resolver(user)))
                .Where(c => !string.IsNullOrEmpty(c.ClaimValue))
                .Select(c => new UserClaim(c.ClaimType, c.ClaimValue))
                .ToList();

            var userClaims = new UserClaims(claimsFromResolvers);
            if (!userClaims.HasClaim(UserClaimTypes.Id))
            {
                throw new InvalidOperationException("User was ID not found. Ensure that Id claim is added via SpotifyAuthorizationCodeFlowOptions.");
            }

            var result = new AuthenticationTicket(
                tokens.RefreshToken,
                tokens.GetAccessTokenModel(this.clock),
                userClaims);

            await this.authenticationTicketRepository.SaveAsync(result, cancellationToken).ConfigureAwait(false);

            this.UpdateAuthenticationTicketSafe(result);
        }

        private async Task LoadNewAccessTokenAsync(CancellationToken cancellationToken)
        {
            var tokens = await this.tokenClient
                .GetNewTokensFromOneTimeRefreshTokenAsync(this.authenticationTicket.RefreshToken, cancellationToken)
                .ConfigureAwait(false);

            var newAuthTicket = new AuthenticationTicket(
                tokens.RefreshToken,
                tokens.GetAccessTokenModel(this.clock),
                this.authenticationTicket.UserClaims);

            await this.authenticationTicketRepository.SaveAsync(newAuthTicket, cancellationToken).ConfigureAwait(false);

            this.UpdateAuthenticationTicketSafe(newAuthTicket);
        }

        private async Task<bool> UnloadAuthenticationTicketAsync(CancellationToken cancellationToken)
        {
            var result = await this.authenticationTicketRepository.DeleteAsync(cancellationToken).ConfigureAwait(false);
            this.UpdateAuthenticationTicketSafe(null);

            return result;
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
