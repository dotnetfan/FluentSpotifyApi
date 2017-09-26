using System.ComponentModel;

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
        [Description("track")]
        Track,

        /// <summary>
        /// Will repeat the current context.
        /// </summary>
        [Description("context")]
        Context,

        /// <summary>
        /// Will turn repeat off.
        /// </summary>
        [Description("off")]
        Off
    }
}
