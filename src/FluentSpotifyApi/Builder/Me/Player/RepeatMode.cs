using System.Runtime.Serialization;

namespace FluentSpotifyApi.Builder.Me.Player
{
    /// <summary>
    /// The playback repeat mode.
    /// </summary>
    public enum RepeatMode
    {
        /// <summary>
        /// Will repeat the current track.
        /// </summary>
        [EnumMember(Value = "track")]
        Track,

        /// <summary>
        /// Will repeat the current context.
        /// </summary>
        [EnumMember(Value = "context")]
        Context,

        /// <summary>
        /// Will turn repeat off.
        /// </summary>
        [EnumMember(Value = "off")]
        Off
    }
}
