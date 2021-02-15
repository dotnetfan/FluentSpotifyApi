using System.Runtime.Serialization;

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
        [EnumMember(Value = "album")]
        Album,

        /// <summary>
        /// The artist
        /// </summary>
        [EnumMember(Value = "artist")]
        Artist,

        /// <summary>
        /// The playlist
        /// </summary>
        [EnumMember(Value = "playlist")]
        Playlist,

        /// <summary>
        /// The track
        /// </summary>
        [EnumMember(Value = "track")]
        Track,

        /// <summary>
        /// The track
        /// </summary>
        [EnumMember(Value = "show")]
        Show,

        /// <summary>
        /// The track
        /// </summary>
        [EnumMember(Value = "episode")]
        Episode
    }
}
