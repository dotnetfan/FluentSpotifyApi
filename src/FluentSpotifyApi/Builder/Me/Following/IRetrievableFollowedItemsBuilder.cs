using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Builder.Me.Following
{
    /// <summary>
    /// The builder for "me/following endpoint".
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="IFollowedItemsBuilder"/>
    public interface IRetrievableFollowedItemsBuilder<T> : IFollowedItemsBuilder
    {
        /// <summary>
        /// Gets the current user’s followed items.
        /// </summary>
        /// <param name="after">The last item ID retrieved from the previous request.</param>
        /// <param name="limit">The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<T> GetAsync(string after = null, int? limit = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
