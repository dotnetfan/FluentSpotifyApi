using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Messages
{
    /// <summary>
    /// The simple albums page message.
    /// </summary>
    public class SimpleAlbumsPageMessage
    {
        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        [JsonProperty(PropertyName = "albums")]
        public Page<SimpleAlbum> Page { get; set; }
    }
}
