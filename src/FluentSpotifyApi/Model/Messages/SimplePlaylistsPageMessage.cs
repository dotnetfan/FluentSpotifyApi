using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Messages
{
    /// <summary>
    /// The simple playlist page message.
    /// </summary>
    public class SimplePlaylistsPageMessage
    {
        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        [JsonProperty(PropertyName = "playlists")]
        public Page<SimplePlaylist> Page { get; set; }
    }
}
