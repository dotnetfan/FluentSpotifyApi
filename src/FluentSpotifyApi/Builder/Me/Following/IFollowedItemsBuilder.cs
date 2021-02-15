using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Builder.Me.Following
{
    /// <summary>
    /// The builder for "me/following" endpoint with IDs.
    /// </summary>
    public interface IFollowedItemsBuilder
    {
        /// <summary>
        /// Adds the current user as a follower of the items.
        /// </summary>
        /// <param name="ids">Item IDs. A maximum of 50 IDs can be sent in one request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task FollowAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Removes the current user as a follower of the items.
        /// </summary>
        /// <param name="ids">Item IDs. A maximum of 50 IDs can be sent in one request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task UnfollowAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Checks to see if the current user is following the items.
        /// </summary>
        /// <param name="ids">Item IDs. A maximum of 50 IDs can be sent in one request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<bool[]> CheckAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default(CancellationToken));
    }
}
