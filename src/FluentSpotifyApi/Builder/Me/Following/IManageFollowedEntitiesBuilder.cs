using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Builder.Me.Following
{
    /// <summary>
    /// The builder for "me/following" endpoint with IDs.
    /// </summary>
    public interface IManageFollowedEntitiesBuilder
    {
        /// <summary>
        /// Add the current user as a follower of one or more artists or other Spotify users.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task FollowAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Remove the current user as a follower of one or more artists or other Spotify users.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UnfollowAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Check to see if the current user is following one or more artists or other Spotify users.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool[]> CheckAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
