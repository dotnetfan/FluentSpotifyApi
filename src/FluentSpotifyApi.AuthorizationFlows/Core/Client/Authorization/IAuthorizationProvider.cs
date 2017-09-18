using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    /// <summary>
    /// The interface for getting authorization from the Spotify Accounts Service.
    /// </summary>
    public interface IAuthorizationProvider<T>
    {
        /// <summary>
        /// Gets the authorization from the Spotify Accounts Service.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<T> GetAsync(CancellationToken cancellationToken);
    }
}
