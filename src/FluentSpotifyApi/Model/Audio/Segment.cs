using System.Collections.Generic;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Audio
{
    /// <summary>
    /// The segment.
    /// </summary>
    public class Segment
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

        /// <summary>
        /// Gets or sets the loudness start.
        /// </summary>
        /// <value>
        /// The loudness start.
        /// </value>
        [JsonProperty(PropertyName = "loudness_start")]
        public float LoudnessStart { get; set; }

        /// <summary>
        /// Gets or sets the loudness maximum time.
        /// </summary>
        /// <value>
        /// The loudness maximum time.
        /// </value>
        [JsonProperty(PropertyName = "loudness_max_time")]
        public float LoudnessMaxTime { get; set; }

        /// <summary>
        /// Gets or sets the loudness maximum.
        /// </summary>
        /// <value>
        /// The loudness maximum.
        /// </value>
        [JsonProperty(PropertyName = "loudness_max")]
        public float LoudnessMax { get; set; }

        /// <summary>
        /// Gets or sets the pitches.
        /// </summary>
        /// <value>
        /// The pitches.
        /// </value>
        [JsonProperty(PropertyName = "pitches")]
        public List<float> Pitches { get; set; }

        /// <summary>
        /// Gets or sets the timbre.
        /// </summary>
        /// <value>
        /// The timbre.
        /// </value>
        [JsonProperty(PropertyName = "timbre")]
        public List<float> Timbre { get; set; }

        /// <summary>
        /// Gets or sets the loudness end.
        /// </summary>
        /// <value>
        /// The loudness end.
        /// </value>
        [JsonProperty(PropertyName = "loudness_end")]
        public float? LoudnessEnd { get; set; }
    }
}
