using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.Core.Model;

namespace FluentSpotifyApi.AuthorizationFlows.Core
{
    /// <summary>
    /// Provides access to the authentication ticket.
    /// </summary>
    public interface IAuthenticationTicketProvider
    {
        /// <summary>
        /// Gets the authentication ticket.
        /// </summary>
        /// <param name="ensureValidAccessToken">Ensures valid access token. Defaults to <c>true</c>.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IAuthenticationTicket> GetAsync(bool ensureValidAccessToken = true, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Renews the access token.
        /// </summary>
        /// <param name="currentAuthenticationTicket">The current authentication ticket.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IAuthenticationTicket> RenewAccessTokenAsync(IAuthenticationTicket currentAuthenticationTicket, CancellationToken cancellationToken = default(CancellationToken));
    }
}
