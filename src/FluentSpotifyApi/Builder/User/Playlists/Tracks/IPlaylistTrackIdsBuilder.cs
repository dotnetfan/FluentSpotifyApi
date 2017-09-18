using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.User.Playlists.Tracks
{
    /// <summary>
    /// The builder for "users/{userId}/playlists/{id}/tracks" endpoint with track IDs.
    /// </summary>
    public interface IPlaylistTrackIdsBuilder
    {
        /// <summary>
        /// Add one or more tracks to a user’s playlist.
        /// </summary>
        /// <param name="position">The position to insert the tracks, a zero-based index. If omitted, the tracks will be appended to the playlist.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PlaylistSnapshot> AddAsync(int? position = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Replace all the tracks in a playlist, overwriting its existing tracks. 
        /// This powerful request can be useful for replacing tracks, re-ordering existing tracks, or clearing the playlist.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PlaylistSnapshot> ReplaceAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Remove one or more tracks from a user’s playlist. 
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PlaylistSnapshot> RemoveAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
