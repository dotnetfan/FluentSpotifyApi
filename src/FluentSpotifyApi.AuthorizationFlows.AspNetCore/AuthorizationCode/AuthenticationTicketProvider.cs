using System.Globalization;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Utils;
using FluentSpotifyApi.AuthorizationFlows.Core;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.AuthorizationFlows.Core.Exceptions;
using FluentSpotifyApi.AuthorizationFlows.Core.Extensions;
using FluentSpotifyApi.AuthorizationFlows.Core.Model;
using FluentSpotifyApi.AuthorizationFlows.Core.Time;
using FluentSpotifyApi.Core.User;
using FluentSpotifyApi.Core.Utils;
using Microsoft.AspNetCore.Authentication;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode
{
    internal class AuthenticationTicketProvider : IAuthenticationTicketProvider
    {
        private readonly IAuthenticationManager authenticationManager;

        private readonly IClock clock;

        private readonly ISpotifyTokenClient tokenClient;

        private readonly ISemaphoreProvider semaphoreProvider;

        public AuthenticationTicketProvider(
            IAuthenticationManager authenticationManager,
            IClock clock,
            ISpotifyTokenClient tokenClient,
            ISemaphoreProvider semaphoreProvider)
        {
            this.authenticationManager = authenticationManager;
            this.clock = clock;
            this.tokenClient = tokenClient;
            this.semaphoreProvider = semaphoreProvider;
        }

        public async Task<IAuthenticationTicket> GetAsync(bool ensureValidAccessToken, CancellationToken cancellationToken)
        {
            var result = await SpotifySemaphoreUtils.ExecuteAsync(
                this.semaphoreProvider.Get(),
                async innerCt =>
                {
                    var authenticateResult = await this.authenticationManager.GetAsync(innerCt).ConfigureAwait(false);
                    var authenticationTicket = new AuthenticationTicket(authenticateResult);

                    if (ensureValidAccessToken && authenticationTicket.AccessToken.IsExpired(this.clock))
                    {
                        authenticationTicket = await this.GetNewAccessTokenAsync(authenticationTicket, authenticateResult, innerCt).ConfigureAwait(false);
                    }

                    return authenticationTicket;
                },
                cancellationToken).ConfigureAwait(false);

            return result;
        }

        public async Task<IAuthenticationTicket> RenewAccessTokenAsync(IAuthenticationTicket currentAuthenticationTicket, CancellationToken cancellationToken)
        {
            var result = await SpotifySemaphoreUtils.ExecuteAsync(
                this.semaphoreProvider.Get(),
                async innerCt =>
                {
                    var authenticateResult = await this.authenticationManager.GetAsync(innerCt).ConfigureAwait(false);
                    var authenticationTicket = new AuthenticationTicket(authenticateResult);

                    return await this.GetNewAccessTokenAsync(authenticationTicket, authenticateResult, innerCt).ConfigureAwait(false);
                },
                cancellationToken).ConfigureAwait(false);

            return result;
        }

        private async Task<AuthenticationTicket> GetNewAccessTokenAsync(AuthenticationTicket authenticationTicket, IAuthenticateResult authenticateResult, CancellationToken cancellationToken)
        {
            var accessTokenResponse = await this.tokenClient.GetAccessTokenFromRefreshTokenAsync(authenticationTicket.RefreshToken, cancellationToken).ConfigureAwait(false);

            var accessToken = accessTokenResponse.GetAccessTokenModel(this.clock);
            authenticateResult.Properties.UpdateTokenValue(TokenNames.AccessToken, accessToken.Token);
            authenticateResult.Properties.UpdateTokenValue(TokenNames.ExpiresAt, accessToken.ExpiresAt.ToString("o", CultureInfo.InvariantCulture));

            await this.authenticationManager.UpdateAsync(authenticateResult, cancellationToken).ConfigureAwait(false);

            return new AuthenticationTicket(authenticateResult);
        }

        private class AuthenticationTicket : IAuthenticationTicket
        {
            public AuthenticationTicket(IAuthenticateResult authenticateResult)
            {
                if (!SpotifyRequiredTokensUtils.TryGet(authenticateResult.Properties, out var tokens))
                {
                    throw new SpotifyMalformedAuthenticationTicketException("Unable to get authentication tokens.");
                }

                this.RefreshToken = tokens.Value.RefreshToken;

                this.AccessToken = new AccessToken(
                    tokens.Value.AccessToken,
                    tokens.Value.ExpiresAt);

                this.User = new PrincipalUser(authenticateResult.Principal);
            }

            public string RefreshToken { get; }

            public IUser User { get; }

            public AccessToken AccessToken { get; }

            private class PrincipalUser : IUser
            {
                public PrincipalUser(ClaimsPrincipal claimsPrincipal)
                {
                    if (!SpotifyRequiredClaimsUtils.TryGet(claimsPrincipal, out var userId))
                    {
                        throw new SpotifyMalformedAuthenticationTicketException("Unable to get User ID.");
                    }

                    this.Id = userId;
                }

                public string Id { get; }
            }
        }
    }
}
