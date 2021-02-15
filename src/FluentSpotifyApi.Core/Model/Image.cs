using System.Text.Json.Serialization;

namespace FluentSpotifyApi.Core.Model
{
    /// <summary>
    /// The image.
    /// </summary>
    public class Image : JsonObject
    {
        /// <summary>
        /// The image height in pixels. If unknown: null or not returned.
        /// </summary>
        [JsonPropertyName("height")]
        public int? Height { get; set; }

        /// <summary>
        /// The source URL of the image.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// The image width in pixels. If unknown: null or not returned.
        /// </summary>
        [JsonPropertyName("width")]
        public int? Width { get; set; }
    }
}
