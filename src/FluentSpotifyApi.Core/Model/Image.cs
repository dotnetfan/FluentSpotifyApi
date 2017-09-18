using Newtonsoft.Json;

namespace FluentSpotifyApi.Core.Model
{
    /// <summary>
    /// The image.
    /// </summary>
    public class Image
    {
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        [JsonProperty(PropertyName = "height")]
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        [JsonProperty(PropertyName = "width")]
        public int? Width { get; set; }
    }
}
