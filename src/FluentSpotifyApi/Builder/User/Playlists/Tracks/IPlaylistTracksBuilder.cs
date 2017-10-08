using System;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Expressions.Fields;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.User.Playlists.Tracks
{
    /// <summary>
    /// The builder for "users/{userId}/playlists/{id}/tracks" endpoint.
    /// </summary>
    public interface IPlaylistTracksBuilder
    {
        /// <summary>
        /// Get full details of the tracks of a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="fields">A comma-separated list of the fields to return. If omitted, all fields are returned.</param>
        /// <param name="limit">The maximum number of tracks to return. Default: 100. Minimum: 1. Maximum: 100.</param>
        /// <param name="offset">The index of the first track to return. Default: 0 (the first object).</param>
        /// <param name="market">An ISO 3166-1 alpha-2 country code.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Page<PlaylistTrack>> GetAsync(string fields = null, int limit = 100, int offset = 0, string market = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get full details of the tracks of a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="buildFields">
        /// The action for building fields.
        /// The <see cref="FieldsProvider.Get{TInput}(Action{IFieldsBuilder{TInput}})"/> method can be used to get fields in string format.
        /// </param>
        /// <param name="limit">The maximum number of tracks to return. Default: 100. Minimum: 1. Maximum: 100.</param>
        /// <param name="offset">The index of the first track to return. Default: 0 (the first object).</param>
        /// <param name="market">An ISO 3166-1 alpha-2 country code.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Page<PlaylistTrack>> GetAsync(Action<IFieldsBuilder<Page<PlaylistTrack>>> buildFields, int limit = 100, int offset = 0, string market = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Reorder a track or a group of tracks in a playlist.
        /// </summary>
        /// <param name="rangeStart">The position of the first track to be reordered.</param>
        /// <param name="insertBefore">The position where the tracks should be inserted.</param>
        /// <param name="rangeLength">The amount of tracks to be reordered. Defaults to 1 if not set.</param>
        /// <param name="snapshotId">The snapshot identifier.</param>
        /// <param name="cancellationToken">The playlist's snapshot ID against which you want to make the changes.</param>
        /// <returns></returns>
        Task<PlaylistSnapshot> ReorderAsync(int rangeStart, int insertBefore, int rangeLength = 1, string snapshotId = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
