using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode
{
    internal interface IAuthenticationTicketRepository
    {
        Task<AuthenticationTicket> GetAsync(CancellationToken cancellationToken);

        Task SaveAsync(AuthenticationTicket authenticationTicket, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(CancellationToken cancellationToken);
    }
}
