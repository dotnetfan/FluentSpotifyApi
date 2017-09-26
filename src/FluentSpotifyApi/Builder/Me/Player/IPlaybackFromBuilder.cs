using System.Collections.Generic;

namespace FluentSpotifyApi.Builder.Me.Player
{
    /// <summary>
    /// The builder for starting new playback from specified entities.
    /// </summary>
    public interface IPlaybackFromBuilder
    {
        /// <summary>
        /// Gets builder for starting new playback from specified artist ID.
        /// </summary>
        /// <param name="id">The Spotify ID of the artist.</param>
        /// <returns></returns>
        IPlaybackPlayBuilder Artist(string id);

        /// <summary>
        /// Gets builder for starting new playback from specified album ID.
        /// </summary>
        /// <param name="id">The Spotify ID of the album.</param>
        /// <returns></returns>
        IPlaybackPlayWithOffsetBuilder Album(string id);

        /// <summary>
        /// Gets builder for starting new playback from specified playlist ID.
        /// </summary>
        /// <param name="ownerId">The Spotify user ID of the person who owns the playlist.</param>
        /// <param name="playlistId">The Spotify ID of the playlist.</param>
        /// <returns></returns>
        IPlaybackPlayWithOffsetBuilder Playlist(string ownerId, string playlistId);

        /// <summary>
        /// Gets builder for starting new playback from specified track IDs.
        /// </summary>
        /// <param name="ids">The Spotify IDs of the tracks.</param>
        /// <returns></returns>
        IPlaybackPlayWithOffsetBuilder Tracks(IEnumerable<string> ids);
    }
}
