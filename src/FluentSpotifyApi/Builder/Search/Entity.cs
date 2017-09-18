using System.ComponentModel;

namespace FluentSpotifyApi.Builder.Search
{
    /// <summary>
    /// The entity.
    /// </summary>
    public enum Entity
    {
        /// <summary>
        /// The album
        /// </summary>
        [Description("album")]
        Album,

        /// <summary>
        /// The artist
        /// </summary>
        [Description("artist")]
        Artist,

        /// <summary>
        /// The playlist
        /// </summary>
        [Description("playlist")]
        Playlist,

        /// <summary>
        /// The track
        /// </summary>
        [Description("track")]
        Track
    }
}
