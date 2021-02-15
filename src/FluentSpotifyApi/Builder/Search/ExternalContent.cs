using System.Runtime.Serialization;

namespace FluentSpotifyApi.Builder.Search
{
    /// <summary>
    /// The external content.
    /// </summary>
    public enum ExternalContent
    {
        /// <summary>
        /// Audio content.
        /// </summary>
        [EnumMember(Value = "audio")]
        Audio,
    }
}
