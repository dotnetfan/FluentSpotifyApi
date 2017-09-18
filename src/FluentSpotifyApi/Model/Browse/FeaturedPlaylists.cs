using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Browse
{
    /// <summary>
    /// The featured playlists.
    /// </summary>
    public class FeaturedPlaylists
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        [JsonProperty(PropertyName = "playlists")]
        public Page<SimplePlaylist> Playlists { get; set; }
    }
}
