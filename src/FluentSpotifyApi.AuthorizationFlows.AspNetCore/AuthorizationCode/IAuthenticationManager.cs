using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode
{
    internal interface IAuthenticationManager
    {
        Task<IAuthenticateResult> GetAsync(CancellationToken cancellationToken);

        Task UpdateAsync(IAuthenticateResult authenticateResult, CancellationToken cancellationToken);
    }
}
