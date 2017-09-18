using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Messages
{
    /// <summary>
    /// The full albums message.
    /// </summary>
    public class FullAlbumsMessage
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [JsonProperty(PropertyName = "albums")]
        public FullAlbum[] Items { get; set; }
    }
}
