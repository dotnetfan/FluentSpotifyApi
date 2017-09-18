using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Audio
{
    /// <summary>
    /// The audio track.
    /// </summary>
    public class Track
    {
        /// <summary>
        /// Gets or sets the number samples.
        /// </summary>
        /// <value>
        /// The number samples.
        /// </value>
        [JsonProperty(PropertyName = "num_samples")]
        public int NumSamples { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        [JsonProperty(PropertyName = "duration")]
        public float Duration { get; set; }

        /// <summary>
        /// Gets or sets the sample MD5.
        /// </summary>
        /// <value>
        /// The sample MD5.
        /// </value>
        [JsonProperty(PropertyName = "sample_md5")]
        public string SampleMd5 { get; set; }

        /// <summary>
        /// Gets or sets the offset seconds.
        /// </summary>
        /// <value>
        /// The offset seconds.
        /// </value>
        [JsonProperty(PropertyName = "offset_seconds")]
        public int OffsetSeconds { get; set; }

        /// <summary>
        /// Gets or sets the window seconds.
        /// </summary>
        /// <value>
        /// The window seconds.
        /// </value>
        [JsonProperty(PropertyName = "window_seconds")]
        public int WindowSeconds { get; set; }

        /// <summary>
        /// Gets or sets the snalysis sample rate.
        /// </summary>
        /// <value>
        /// The snalysis sample rate.
        /// </value>
        [JsonProperty(PropertyName = "analysis_sample_rate")]
        public int SnalysisSampleRate { get; set; }

        /// <summary>
        /// Gets or sets the analysis channels.
        /// </summary>
        /// <value>
        /// The analysis channels.
        /// </value>
        [JsonProperty(PropertyName = "analysis_channels")]
        public int AnalysisChannels { get; set; }

        /// <summary>
        /// Gets or sets the end of fade in.
        /// </summary>
        /// <value>
        /// The end of fade in.
        /// </value>
        [JsonProperty(PropertyName = "end_of_fade_in")]
        public float EndOfFadeIn { get; set; }

        /// <summary>
        /// Gets or sets the start of fade out.
        /// </summary>
        /// <value>
        /// The start of fade out.
        /// </value>
        [JsonProperty(PropertyName = "start_of_fade_out")]
        public float StartOfFadeOut { get; set; }

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
        /// Gets or sets the code string.
        /// </summary>
        /// <value>
        /// The code string.
        /// </value>
        [JsonProperty(PropertyName = "codestring")]
        public string CodeString { get; set; }

        /// <summary>
        /// Gets or sets the code version.
        /// </summary>
        /// <value>
        /// The code version.
        /// </value>
        [JsonProperty(PropertyName = "code_version")]
        public float CodeVersion { get; set; }

        /// <summary>
        /// Gets or sets the echo print string.
        /// </summary>
        /// <value>
        /// The echo print string.
        /// </value>
        [JsonProperty(PropertyName = "echoprintstring")]
        public string EchoPrintString { get; set; }

        /// <summary>
        /// Gets or sets the echo print version.
        /// </summary>
        /// <value>
        /// The echo print version.
        /// </value>
        [JsonProperty(PropertyName = "echoprint_version")]
        public float EchoPrintVersion { get; set; }

        /// <summary>
        /// Gets or sets the synch string.
        /// </summary>
        /// <value>
        /// The synch string.
        /// </value>
        [JsonProperty(PropertyName = "synchstring")]
        public string SynchString { get; set; }

        /// <summary>
        /// Gets or sets the synch version.
        /// </summary>
        /// <value>
        /// The synch version.
        /// </value>
        [JsonProperty(PropertyName = "synch_version")]
        public float SynchVersion { get; set; }

        /// <summary>
        /// Gets or sets the rhythm string.
        /// </summary>
        /// <value>
        /// The rhythm string.
        /// </value>
        [JsonProperty(PropertyName = "rhythmstring")]
        public string RhythmString { get; set; }

        /// <summary>
        /// Gets or sets the rhythm version.
        /// </summary>
        /// <value>
        /// The rhythm version.
        /// </value>
        [JsonProperty(PropertyName = "rhythm_version")]
        public float RhythmVersion { get; set; }
    }
}
