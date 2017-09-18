using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The tracks link.
    /// </summary>
    public class TracksLink
    {
        /// <summary>
        /// Gets or sets the href.
        /// </summary>
        /// <value>
        /// The href.
        /// </value>
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }
    }
}
