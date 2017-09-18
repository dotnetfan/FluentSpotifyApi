using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Messages
{
    /// <summary>
    /// The full tracks page message.
    /// </summary>
    public class FullTracksPageMessage
    {
        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        [JsonProperty(PropertyName = "tracks")]
        public Page<FullTrack> Page { get; set; }
    }
}
