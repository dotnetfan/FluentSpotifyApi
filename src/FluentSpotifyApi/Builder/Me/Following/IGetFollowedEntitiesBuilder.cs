using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Builder.Me.Following
{
    /// <summary>
    /// The builder for "me/following endpoint".
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGetFollowedEntitiesBuilder<T>
    {
        /// <summary>
        /// Get the current user’s followed artists.
        /// </summary>
        /// <param name="limit">The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="after">The last artist ID retrieved from the previous request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<T> GetAsync(int limit = 20, string after = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
