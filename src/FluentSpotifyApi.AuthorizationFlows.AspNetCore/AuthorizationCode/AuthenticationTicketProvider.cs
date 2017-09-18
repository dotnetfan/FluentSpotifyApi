using System;
using System.Globalization;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Extensions;
using FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Server.Extensions;
using FluentSpotifyApi.AuthorizationFlows.Core;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.AuthorizationFlows.Core.Date;
using FluentSpotifyApi.AuthorizationFlows.Core.Extensions;
using FluentSpotifyApi.AuthorizationFlows.Core.Model;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Core.Options;
using Microsoft.AspNetCore.Authentication;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode
{
    internal class AuthenticationTicketProvider : IAuthenticationTicketProvider
    {
        private const string RefreshTokenKey = "refresh_token";

        private const string AccessTokenKey = "access_token";

        private const string ExpiresAtKey = "expires_at";

        private readonly IAuthenticationManager authenticationManager;

        private readonly IDateTimeOffsetProvider dateTimeOffsetProvider;

        private readonly ITokenHttpClient tokenHttpClient;

        private readonly IOptionsProvider<AspNetCoreAuthorizationCodeFlowOptions> optionsProvider;

        public AuthenticationTicketProvider(
            IAuthenticationManager authenticationManager, 
            IDateTimeOffsetProvider dateTimeOffsetProvider,
            ITokenHttpClient tokenHttpClient,
            IOptionsProvider<AspNetCoreAuthorizationCodeFlowOptions> optionsProvider) 
        {
            this.authenticationManager = authenticationManager;
            this.dateTimeOffsetProvider = dateTimeOffsetProvider;
            this.tokenHttpClient = tokenHttpClient;
            this.optionsProvider = optionsProvider;
        }

        public async Task<IAuthenticationTicket> GetAsync(CancellationToken cancellationToken)
        {
            this.ValidateOptionsMapping();

            var authenticateResult = await this.authenticationManager.GetAsync(cancellationToken).ConfigureAwait(false);
            var authenticationTicket = new AuthenticationTicket(authenticateResult);

            if (!authenticationTicket.AccessToken.IsValid(this.dateTimeOffsetProvider))
            {
                authenticationTicket = await this.GetNewAccessTokenAsync(authenticationTicket, authenticateResult, cancellationToken).ConfigureAwait(false);
            }

            return authenticationTicket;
        }

        public async Task<IAuthenticationTicket> RenewAccessTokenAsync(IAuthenticationTicket currentAuthenticationTicket, CancellationToken cancellationToken)
        {
            this.ValidateOptionsMapping();

            var authenticateResult = await this.authenticationManager.GetAsync(cancellationToken).ConfigureAwait(false);
            var authenticationTicket = new AuthenticationTicket(authenticateResult);

            return await this.GetNewAccessTokenAsync(authenticationTicket, authenticateResult, cancellationToken).ConfigureAwait(false);
        }

        private async Task<AuthenticationTicket> GetNewAccessTokenAsync(AuthenticationTicket authenticationTicket, IAuthenticateResult authenticateResult, CancellationToken cancellationToken)
        {
            var accessTokenDto = await this.tokenHttpClient
                    .GetAccessTokenOrThrowInvalidRefreshTokenExceptionAsync(authenticationTicket.RefreshToken, cancellationToken)
                    .ConfigureAwait(false);

            var accessToken = accessTokenDto.ToModelToken(this.dateTimeOffsetProvider);

            authenticateResult.Properties.UpdateTokenValue(AccessTokenKey, accessToken.Token);
            authenticateResult.Properties.UpdateTokenValue(ExpiresAtKey, accessToken.ExpiresAt.ToString("o", CultureInfo.InvariantCulture));

            await this.authenticationManager.UpdateAsync(authenticateResult, cancellationToken).ConfigureAwait(false);

            return new AuthenticationTicket(authenticateResult);
        }

        private void ValidateOptionsMapping()
        {
            // Gets options to ensure they are correctly mapped to the underlying Spotify Options.
            this.optionsProvider.Get();
        }

        private class AuthenticationTicket : IAuthenticationTicket
        {
            public AuthenticationTicket(IAuthenticateResult authenticateResult)
            {
                var refreshToken = authenticateResult.Properties.GetTokenValue(RefreshTokenKey);
                var accessToken = authenticateResult.Properties.GetTokenValue(AccessTokenKey);
                var expiresAt = authenticateResult.Properties.GetTokenValue(ExpiresAtKey);

                if (string.IsNullOrEmpty(refreshToken) || string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(expiresAt))
                {
                    throw new InvalidOperationException("Authentication tokens were not found.");
                }

                this.RefreshToken = refreshToken;

                this.AccessToken = new AccessToken(
                    accessToken,
                    DateTimeOffset.ParseExact(expiresAt, "o", CultureInfo.InvariantCulture));

                this.User = new PrincipalUser(authenticateResult.Principal);
            }

            public string RefreshToken { get; private set; }

            public IUser User { get; private set; }

            public AccessToken AccessToken { get; private set; }

            private class PrincipalUser : IUser
            {
                public PrincipalUser(ClaimsPrincipal claimsPrincipal)
                {
                    var userId = claimsPrincipal.GetNameIdentifier();

                    if (string.IsNullOrEmpty(userId))
                    {
                        throw new InvalidOperationException("User ID was not found.");
                    }

                    this.Id = userId;
                }

                public string Id { get; private set; }
            }
        }
    }
}
