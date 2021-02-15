using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Audio
{
    /// <summary>
    /// The time interval.
    /// </summary>
    public class TimeInterval : JsonObject
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
    }
}
