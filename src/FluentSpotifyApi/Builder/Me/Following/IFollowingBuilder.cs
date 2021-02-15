using FluentSpotifyApi.Model.Following;

namespace FluentSpotifyApi.Builder.Me.Following
{
    /// <summary>
    /// The builder for "me/following" and "playlists/{playlistId}/followers" endpoints.
    /// These endpoints allow you manage the artists, users and playlists that a Spotify user follows.
    /// </summary>
    public interface IFollowingBuilder
    {
        /// <summary>
        /// Gets builder for "me/following?type=artist" endpoint.
        /// </summary>
        IRetrievableFollowedItemsBuilder<FollowedArtists> Artists { get; }

        /// <summary>
        /// Gets builder for "me/following?type=user" endpoint.
        /// </summary>
        IFollowedItemsBuilder Users { get; }

        /// <summary>
        /// Gets builder for "playlists/{playlistId}/followers" endpoint.
        /// </summary>
        IFollowedPlaylistBuilder Playlists { get; }
    }
}
