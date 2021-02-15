using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Audio
{
    /// <summary>
    /// The segment.
    /// </summary>
    public class Segment : JsonObject
    {
        /// <summary>
        /// The start.
        /// </summary>
        [JsonPropertyName("start")]
        public float Start { get; set; }

        /// <summary>
        /// The duration.
        /// </summary>
        [JsonPropertyName("duration")]
        public float Duration { get; set; }

        /// <summary>
        /// The confidence.
        /// </summary>
        [JsonPropertyName("confidence")]
        public float Confidence { get; set; }

        /// <summary>
        /// The loudness start.
        /// </summary>
        [JsonPropertyName("loudness_start")]
        public float LoudnessStart { get; set; }

        /// <summary>
        /// The loudness maximum time.
        /// </summary>
        [JsonPropertyName("loudness_max_time")]
        public float LoudnessMaxTime { get; set; }

        /// <summary>
        /// The loudness maximum.
        /// </summary>
        [JsonPropertyName("loudness_max")]
        public float LoudnessMax { get; set; }

        /// <summary>
        /// The pitches.
        /// </summary>
        [JsonPropertyName("pitches")]
        public float[] Pitches { get; set; }

        /// <summary>
        /// The timbre.
        /// </summary>
        [JsonPropertyName("timbre")]
        public float[] Timbre { get; set; }

        /// <summary>
        /// The loudness end.
        /// </summary>
        [JsonPropertyName("loudness_end")]
        public float? LoudnessEnd { get; set; }
    }
}
