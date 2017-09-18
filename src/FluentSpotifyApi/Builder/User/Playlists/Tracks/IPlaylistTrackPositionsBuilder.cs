using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.User.Playlists.Tracks
{
    /// <summary>
    /// The builder for "users/{userId}/playlists/{id}/tracks" endpoint with track positions.
    /// </summary>
    public interface IPlaylistTrackPositionsBuilder
    {
        /// <summary>
        /// Removes tracks at given positions. 
        /// </summary>
        /// <param name="snapshotId">The playlist's snapshot ID against which you want to make the changes.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PlaylistSnapshot> RemoveAsync(string snapshotId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
