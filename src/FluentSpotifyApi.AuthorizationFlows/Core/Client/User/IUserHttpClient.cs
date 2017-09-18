using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.User
{
    /// <summary>
    /// The HTTP client for getting information about current user.
    /// </summary>
    public interface IUserHttpClient
    {
        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PrivateUser> GetCurrentUserAsync(string accessToken, CancellationToken cancellationToken);
    }
}
