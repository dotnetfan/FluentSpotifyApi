using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Builder.Me.Following
{
    /// <summary>
    /// The builder for "playlists/{playlistId}/followers" endpoint.
    /// </summary>
    public interface IFollowedPlaylistBuilder
    {
        /// <summary>
        /// Adds the current user as a follower of a playlist.
        /// </summary>
        /// <param name="id">The ID of playlist.</param>
        /// <param name="isPublic">
        /// Defaults to <c>true</c>. If <c>true</c> the playlist will be included in user’s public playlists, if false it will remain private.
        /// To be able to follow playlists privately, the user must have granted the <c>playlist-modify-private</c> scope.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task FollowAsync(string id, bool? isPublic = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Removes the current user as a follower of a playlist.
        /// </summary>
        /// <param name="id">The ID of playlist.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UnfollowAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Checks to see if current user is following a specified playlist.
        /// </summary>
        /// <param name="id">The ID of playlist.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> CheckAsync(string id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
