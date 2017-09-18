using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Audio
{
    /// <summary>
    /// The beat.
    /// </summary>
    public class Beat
    {
        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        [JsonProperty(PropertyName = "start")]
        public float Start { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        [JsonProperty(PropertyName = "duration")]
        public float Duration { get; set; }

        /// <summary>
        /// Gets or sets the confidence.
        /// </summary>
        /// <value>
        /// The confidence.
        /// </value>
        [JsonProperty(PropertyName = "confidence")]
        public float Confidence { get; set; }
    }
}
