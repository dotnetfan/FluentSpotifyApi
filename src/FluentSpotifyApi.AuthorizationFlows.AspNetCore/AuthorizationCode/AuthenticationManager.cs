using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode
{
    internal class AuthenticationManager : IAuthenticationManager
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        private readonly string authenticationScheme;

        public AuthenticationManager(IHttpContextAccessor httpContextAccessor, string authenticationScheme)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.authenticationScheme = authenticationScheme;
        }

        public async Task<IAuthenticateResult> GetAsync(CancellationToken cancellationToken)
        {
            var result = await this.httpContextAccessor.HttpContext.AuthenticateAsync(this.authenticationScheme).ConfigureAwait(false);

            if (!(result.Principal?.Identity?.IsAuthenticated).GetValueOrDefault())
            {
                throw new UnauthorizedAccessException();
            }

            return new AuthenticateResultWrapper(result);
        }

        public async Task UpdateAsync(IAuthenticateResult authenticateResult, CancellationToken cancellationToken)
        {
            var httpContext = this.httpContextAccessor.HttpContext;

            await httpContext.SignOutAsync(this.authenticationScheme).ConfigureAwait(false);
            await httpContext.SignInAsync(this.authenticationScheme, authenticateResult.Principal, authenticateResult.Properties).ConfigureAwait(false);
        }

        private class AuthenticateResultWrapper : IAuthenticateResult
        {
            private readonly AuthenticateResult authenticateResult;

            public AuthenticateResultWrapper(AuthenticateResult authenticateResult)
            {
                this.authenticateResult = authenticateResult;
            }

            public ClaimsPrincipal Principal => this.authenticateResult.Principal;

            public AuthenticationProperties Properties => this.authenticateResult.Properties;
        }
    }
}
