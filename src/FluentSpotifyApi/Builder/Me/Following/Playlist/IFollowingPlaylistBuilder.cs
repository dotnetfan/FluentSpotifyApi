using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Builder.Me.Following.Playlist
{
    /// <summary>
    /// The builder for "users/{ownerId}/playlists/{playlistId}/followers" endpoint.
    /// </summary>
    public interface IFollowingPlaylistBuilder
    {
        /// <summary>
        /// Add the current user as a follower of a playlist.
        /// </summary>
        /// <param name="isPublic">If true the playlist will be included in user's public playlists, if false it will remain private. Default: true.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task FollowAsync(bool isPublic = true, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Remove the current user as a follower of a playlist.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UnfollowAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Check to see if current user is following a specified playlist.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> CheckAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
