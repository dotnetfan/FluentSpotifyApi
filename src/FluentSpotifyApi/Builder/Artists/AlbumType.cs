using System.Runtime.Serialization;

namespace FluentSpotifyApi.Builder.Artists
{
    /// <summary>
    /// The album type.
    /// </summary>
    public enum AlbumType
    {
        /// <summary>
        /// The album
        /// </summary>
        [EnumMember(Value = "album")]
        Album,

        /// <summary>
        /// The single
        /// </summary>
        [EnumMember(Value = "single")]
        Single,

        /// <summary>
        /// The appears on
        /// </summary>
        [EnumMember(Value = "appears_on")]
        AppearsOn,

        /// <summary>
        /// The compilation
        /// </summary>
        [EnumMember(Value = "compilation")]
        Compilation
    }
}
