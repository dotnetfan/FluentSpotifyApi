using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native
{
    internal interface IAuthenticationTicketStorage
    {
        Task<AuthenticationTicket> GetAsync(CancellationToken cancellationToken);

        Task SaveAsync(AuthenticationTicket authenticationTicket, CancellationToken cancellationToken);

        Task DeleteAsync(CancellationToken cancellationToken);
    }
}
