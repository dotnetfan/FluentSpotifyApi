using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Expressions.Fields;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Playlists;

namespace FluentSpotifyApi.Builder.Playlists
{
    /// <summary>
    /// The builder for "playlists/{id}/tracks" endpoint.
    /// </summary>
    public interface IPlaylistItemsBuilder
    {
        /// <summary>
        /// Gets full details of the items of a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="fields">A comma-separated list of the fields to return. If omitted, all fields are returned.</param>
        /// <param name="market">
        /// An ISO 3166-1 alpha-2 country code or the string <c>from_token</c>. Provide this parameter if you want to apply Track Relinking.
        /// For episodes, if a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter.
        /// </param>
        /// <param name="limit">The maximum number of items to return. Default: 100. Minimum: 1. Maximum: 100.</param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object).</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<Page<PlaylistTrack>> GetAsync(string fields = null, string market = null, int? limit = null, int? offset = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get full details of the items of a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="buildFields">
        /// The action for building fields.
        /// The <see cref="FieldsProvider.Get{TInput}(Action{IFieldsBuilder{TInput}})"/> method can be used to get fields in string format.
        /// </param>
        /// <param name="market">
        /// An ISO 3166-1 alpha-2 country code or the string <c>from_token</c>. Provide this parameter if you want to apply Track Relinking.
        /// For episodes, if a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter.
        /// </param>
        /// <param name="limit">The maximum number of items to return. Default: 100. Minimum: 1. Maximum: 100.</param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object).</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<Page<PlaylistTrack>> GetAsync(Action<IFieldsBuilder<Page<PlaylistTrack>>> buildFields, string market = null, int? limit = null, int? offset = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Adds one or more items to a user’s playlist.
        /// </summary>
        /// <param name="uris">
        /// The list of Spotify URIs to add, can be track or episode URIs. Maximum: 100.
        /// Use <see cref="SpotifyUri"/> to get URI from ID.
        /// </param>
        /// <param name="position">The position to insert the items, a zero-based index. If omitted, the items will be appended to the playlist.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PlaylistSnapshot> AddAsync(IEnumerable<string> uris, int? position = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Replaces all the items in a playlist, overwriting its existing items.
        /// This powerful request can be useful for replacing items, re-ordering existing items, or clearing the playlist.
        /// </summary>
        /// <param name="uris">
        /// The list of Spotify URIs, can be track or episodes URIs. Maximum: 100.
        /// Use <see cref="SpotifyUri"/> to get URI from ID.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PlaylistSnapshot> ReplaceAsync(IEnumerable<string> uris, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Removes one or more items from a user’s playlist.
        /// </summary>
        /// <param name="uris">
        /// The list of Spotify URIs to remove, can be track or episode URIs. Maximum: 100.
        /// Use <see cref="SpotifyUri"/> to get URI from ID.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PlaylistSnapshot> RemoveAsync(IEnumerable<string> uris, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Removes items by occurrences at given positions.
        /// </summary>
        /// <param name="urisWithPositions">
        /// The list of Spotify URIs at given positions in playlist to remove, can be track or episode URIs. Maximum: 100.
        /// Use <see cref="SpotifyUri"/> to get URI from ID.
        /// </param>
        /// <param name="snapshotId">The playlist's snapshot ID against which you want to make the changes.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PlaylistSnapshot> RemoveAsync(IEnumerable<UriWithPositions> urisWithPositions, string snapshotId = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Removes items by occurrences at given positions.
        /// </summary>
        /// <param name="positions">The item positions in playlist.</param>
        /// <param name="snapshotId">The playlist's snapshot ID against which you want to make the changes.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PlaylistSnapshot> RemoveAsync(IEnumerable<int> positions, string snapshotId = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Reorders an item or a group of items in a playlist.
        /// </summary>
        /// <param name="rangeStart">The position of the first item to be reordered.</param>
        /// <param name="insertBefore">
        /// The position where the items should be inserted.
        /// To reorder the items to the end of the playlist, simply set <paramref name="insertBefore"/> to the position after the last item.
        /// </param>
        /// <param name="rangeLength">
        /// The amount of items to be reordered. Defaults to 1 if not set.
        /// The range of items to be reordered begins from the <paramref name="rangeStart"/> position, and includes the <paramref name="rangeLength"/> subsequent items.
        /// </param>
        /// <param name="snapshotId">The playlist’s snapshot ID against which you want to make the changes.</param>
        /// <param name="cancellationToken">The playlist's snapshot ID against which you want to make the changes.</param>
        /// <returns></returns>
        Task<PlaylistSnapshot> ReorderAsync(int rangeStart, int insertBefore, int? rangeLength = null, string snapshotId = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
