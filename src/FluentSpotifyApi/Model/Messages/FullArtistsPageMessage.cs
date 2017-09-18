using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Messages
{
    /// <summary>
    /// The full artists page message.
    /// </summary>
    public class FullArtistsPageMessage
    {
        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        [JsonProperty(PropertyName = "artists")]
        public Page<FullArtist> Page { get; set; }
    }
}
