using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Core.User
{
    /// <summary>
    /// Provides access to authenticated Spotify user.
    /// </summary>
    public interface ICurrentUserProvider
    {
        /// <summary>
        /// Gets the authenticated Spotify user.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IUser> GetAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
