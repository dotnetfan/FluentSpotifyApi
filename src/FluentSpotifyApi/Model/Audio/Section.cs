using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Audio
{
    /// <summary>
    /// The section.
    /// </summary>
    public class Section
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
        /// Gets or sets the loudness.
        /// </summary>
        /// <value>
        /// The loudness.
        /// </value>
        [JsonProperty(PropertyName = "loudness")]
        public float Loudness { get; set; }

        /// <summary>
        /// Gets or sets the tempo.
        /// </summary>
        /// <value>
        /// The tempo.
        /// </value>
        [JsonProperty(PropertyName = "tempo")]
        public float Tempo { get; set; }

        /// <summary>
        /// Gets or sets the tempo confidence.
        /// </summary>
        /// <value>
        /// The tempo confidence.
        /// </value>
        [JsonProperty(PropertyName = "tempo_confidence")]
        public float TempoConfidence { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        [JsonProperty(PropertyName = "key")]
        public int Key { get; set; }

        /// <summary>
        /// Gets or sets the key confidence.
        /// </summary>
        /// <value>
        /// The key confidence.
        /// </value>
        [JsonProperty(PropertyName = "key_confidence")]
        public float KeyConfidence { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>
        /// The mode.
        /// </value>
        [JsonProperty(PropertyName = "mode")]
        public int Mode { get; set; }

        /// <summary>
        /// Gets or sets the mode confidence.
        /// </summary>
        /// <value>
        /// The mode confidence.
        /// </value>
        [JsonProperty(PropertyName = "mode_confidence")]
        public float ModeConfidence { get; set; }

        /// <summary>
        /// Gets or sets the time signature.
        /// </summary>
        /// <value>
        /// The time signature.
        /// </value>
        [JsonProperty(PropertyName = "time_signature")]
        public int TimeSignature { get; set; }

        /// <summary>
        /// Gets or sets the time signature confidence.
        /// </summary>
        /// <value>
        /// The time signature confidence.
        /// </value>
        [JsonProperty(PropertyName = "time_signature_confidence")]
        public float TimeSignatureConfidence { get; set; }
    }
}
