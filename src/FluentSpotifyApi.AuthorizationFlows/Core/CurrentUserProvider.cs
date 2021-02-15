using System;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.User;

namespace FluentSpotifyApi.AuthorizationFlows.Core
{
    internal class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IAuthenticationTicketProvider authenticationTicketProvider;

        public CurrentUserProvider(IAuthenticationTicketProvider authenticationTicketProvider)
        {
            this.authenticationTicketProvider = authenticationTicketProvider;
        }

        public async Task<IUser> GetAsync(CancellationToken cancellationToken)
        {
            var authenticationTicket = await this.authenticationTicketProvider.GetAsync(ensureValidAccessToken: false,  cancellationToken: cancellationToken).ConfigureAwait(false);
            return authenticationTicket.User ?? throw new NotSupportedException("Current authorization flow does not support access to authenticated Spotify user.");
        }
    }
}
