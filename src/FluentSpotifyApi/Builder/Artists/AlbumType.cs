using System.ComponentModel;

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
        [Description("album")]
        Album,

        /// <summary>
        /// The single
        /// </summary>
        [Description("single")]
        Single,

        /// <summary>
        /// The appears on
        /// </summary>
        [Description("appears_on")]
        AppearsOn,

        /// <summary>
        /// The compilation
        /// </summary>
        [Description("compilation")]
        Compilation
    }
}
