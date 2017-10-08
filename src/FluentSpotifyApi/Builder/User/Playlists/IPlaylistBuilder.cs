using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Builder.User.Playlists.Tracks;
using FluentSpotifyApi.Expressions.Fields;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.User.Playlists
{
    /// <summary>
    /// The builder for "users/{userId}/playlists/{id}" endpoint.
    /// </summary>
    public interface IPlaylistBuilder
    {
        /// <summary>
        /// Get a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="fields">A comma-separated list of the fields to return. If omitted, all fields are returned.</param>
        /// <param name="market">An ISO 3166-1 alpha-2 country code.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<FullPlaylist> GetAsync(string fields = null, string market = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="buildFields">
        /// The action for building fields.
        /// The <see cref="FieldsProvider.Get{TInput}(Action{IFieldsBuilder{TInput}})"/> method can be used to get fields in string format.
        /// </param>
        /// <param name="market">An ISO 3166-1 alpha-2 country code.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<FullPlaylist> GetAsync(Action<IFieldsBuilder<FullPlaylist>> buildFields, string market = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Change a playlist’s name and public/private state. (The user must, of course, own the playlist.)
        /// </summary>
        /// <param name="updatePlaylistDto">The DTO for updating playlist.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UpdateAsync(UpdatePlaylistDto updatePlaylistDto, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Replace the image used to represent a specific playlist.
        /// </summary>
        /// <param name="coverStreamProvider">The JPEG cover stream provider.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UpdateCoverAsync(Func<CancellationToken, Task<Stream>> coverStreamProvider, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Check to see if one or more Spotify users are following a specified playlist.
        /// </summary>
        /// <param name="userIds">
        /// The list of user IDs. Maximum: 5.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IList<bool>> CheckFollowersAsync(IEnumerable<string> userIds, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets builder for "users/{userId}/playlists/{id}/tracks" endpoint.
        /// </summary>
        IPlaylistTracksBuilder Tracks();

        /// <summary>
        /// Gets builder for "users/{userId}/playlists/{id}/tracks" endpoint with track IDs.
        /// </summary>
        /// <param name="ids">
        /// The track IDs. Maximum: 100.
        /// </param>
        IPlaylistTrackIdsBuilder Tracks(IEnumerable<string> ids);

        /// <summary>
        /// Gets builder for "users/{userId}/playlists/{id}/tracks" endpoint with track positions.
        /// </summary>
        /// <param name="positions">
        /// The track IDs. Maximum: 100.
        /// </param>
        IPlaylistTrackPositionsBuilder Tracks(IEnumerable<int> positions);

        /// <summary>
        /// Gets builder for "users/{userId}/playlists/{id}/tracks" endpoint with track (ID, position) pairs.
        /// </summary>
        /// <param name="idsWithPositions">
        /// The track IDs. Maximum: 100.
        /// </param>
        [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1009:ClosingParenthesisMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
        IPlaylistTrackIdsWithPositionsBuilder Tracks(IEnumerable<(string Id, int[] Positions)> idsWithPositions);
    }
}
