using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Messages
{
    /// <summary>
    /// The full artists message.
    /// </summary>
    public class FullArtistsMessage
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [JsonProperty(PropertyName = "artists")]
        public FullArtist[] Items { get; set; }
    }
}
