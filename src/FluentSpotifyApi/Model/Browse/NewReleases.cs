using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Browse
{
    /// <summary>
    /// The new releases.
    /// </summary>
    public class NewReleases
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
        /// Gets or sets the albums.
        /// </summary>
        /// <value>
        /// The albums.
        /// </value>
        [JsonProperty(PropertyName = "albums")]
        public Page<SimpleAlbum> Albums { get; set; }
    }
}
