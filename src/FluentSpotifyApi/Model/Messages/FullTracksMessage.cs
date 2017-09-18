using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Messages
{
    /// <summary>
    /// The full tracks message.
    /// </summary>
    public class FullTracksMessage
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [JsonProperty(PropertyName = "tracks")]
        public FullTrack[] Items { get; set; }
    }
}
