using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Audio
{
    /// <summary>
    /// The section.
    /// </summary>
    public class Section : JsonObject
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
        /// The loudness.
        /// </summary>
        [JsonPropertyName("loudness")]
        public float Loudness { get; set; }

        /// <summary>
        /// The tempo.
        /// </summary>
        [JsonPropertyName("tempo")]
        public float Tempo { get; set; }

        /// <summary>
        /// The tempo confidence.
        /// </summary>
        [JsonPropertyName("tempo_confidence")]
        public float TempoConfidence { get; set; }

        /// <summary>
        /// The key.
        /// </summary>
        [JsonPropertyName("key")]
        public int Key { get; set; }

        /// <summary>
        /// The key confidence.
        /// </summary>
        [JsonPropertyName("key_confidence")]
        public float KeyConfidence { get; set; }

        /// <summary>
        /// The mode.
        /// </summary>
        [JsonPropertyName("mode")]
        public int Mode { get; set; }

        /// <summary>
        /// The mode confidence.
        /// </summary>
        [JsonPropertyName("mode_confidence")]
        public float ModeConfidence { get; set; }

        /// <summary>
        /// The time signature.
        /// </summary>
        [JsonPropertyName("time_signature")]
        public int TimeSignature { get; set; }

        /// <summary>
        /// The time signature confidence.
        /// </summary>
        [JsonPropertyName("time_signature_confidence")]
        public float TimeSignatureConfidence { get; set; }
    }
}
