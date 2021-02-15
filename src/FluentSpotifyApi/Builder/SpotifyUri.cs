using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.Builder
{
    /// <summary>
    /// Provides Spotify URI for specified entity.
    /// </summary>
    public static class SpotifyUri
    {
        /// <summary>
        /// Gets Spotify URI of artist from ID.
        /// </summary>
        /// <param name="artistId">The artist ID.</param>
        public static string OfArtist(string artistId)
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(artistId, nameof(artistId));
            return $"spotify:artist:{artistId}";
        }

        /// <summary>
        /// Gets Spotify URI of album from ID.
        /// </summary>
        /// <param name="albumId">The album ID.</param>
        public static string OfAlbum(string albumId)
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(albumId, nameof(albumId));
            return $"spotify:album:{albumId}";
        }

        /// <summary>
        /// Gets Spotify URI of track from ID.
        /// </summary>
        /// <param name="trackId">The track ID.</param>
        public static string OfTrack(string trackId)
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(trackId, nameof(trackId));
            return $"spotify:track:{trackId}";
        }

        /// <summary>
        /// Gets Spotify URI of show from ID.
        /// </summary>
        /// <param name="showId">The show ID.</param>
        public static string OfShow(string showId)
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(showId, nameof(showId));
            return $"spotify:show:{showId}";
        }

        /// <summary>
        /// Gets Spotify URI of episode from ID.
        /// </summary>
        /// <param name="episodeId">The episode ID.</param>
        public static string OfEpisode(string episodeId)
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(episodeId, nameof(episodeId));
            return $"spotify:episode:{episodeId}";
        }

        /// <summary>
        /// Gets Spotify URI of playlist from ID.
        /// </summary>
        /// <param name="playlistId">The playlist ID.</param>
        public static string OfPlaylist(string playlistId)
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(playlistId, nameof(playlistId));
            return $"spotify:playlist:{playlistId}";
        }

        /// <summary>
        /// Gets Spotify URI of user from ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        public static string OfUser(string userId)
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(userId, nameof(userId));
            return $"spotify:user:{userId}";
        }
    }
}
