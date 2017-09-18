using System.Collections.Generic;
using FluentSpotifyApi.Builder.Me.Following;
using FluentSpotifyApi.Builder.Me.Following.Playlist;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Me
{
    /// <summary>
    /// The builder for "me/following" and "users/{ownerId}/playlists/{playlistId}/followers" endpoints.
    /// These endpoints allow you manage the artists, users and playlists that a Spotify user follows.
    /// </summary>
    public interface IFollowingBuilder
    {
        /// <summary>
        /// Gets builder for "me/following?type=artist" endpoint.
        /// </summary>
        IGetFollowedEntitiesBuilder<FollowedArtists> Artists();

        /// <summary>
        /// Gets builder for "me/following?type=artist" endpoint with IDs.
        /// </summary>
        /// <param name="ids">
        /// The artist IDs. Maximum: 50.
        /// </param>
        IManageFollowedEntitiesBuilder Artists(IEnumerable<string> ids);

        /// <summary>
        /// Gets builder for "me/following?type=user" endpoint.
        /// </summary>
        /// <param name="ids">
        /// The user IDs. Maximum: 50.
        /// </param>
        IManageFollowedEntitiesBuilder Users(IEnumerable<string> ids);

        /// <summary>
        /// Gets builder for "users/{ownerId}/playlists/{playlistId}/followers" endpoint.
        /// </summary>
        /// <param name="ownerId">The Spotify user ID of the person who owns the playlist.</param>
        /// <param name="playlistId">The Spotify ID of the playlist.</param>
        IFollowingPlaylistBuilder Playlist(string ownerId, string playlistId);
    }
}
