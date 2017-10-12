using System.ComponentModel;

namespace FluentSpotifyApi.Builder.Search
{
    /// <summary>
    /// The search tag
    /// </summary>
    public enum Tag
    {
        /// <summary>
        /// Represents albums released in last two weeks.
        /// </summary>
        [Description("new")]
        New,

        /// <summary>
        /// Represents albums with the lowest 10% popularity.
        /// </summary>
        [Description("hipster")]
        Hipster
    }
}
