using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Audio
{
    /// <summary>
    /// The audio track.
    /// </summary>
    public class AudioTrack : JsonObject
    {
        /// <summary>
        /// The number samples.
        /// </summary>
        [JsonPropertyName("num_samples")]
        public int NumSamples { get; set; }

        /// <summary>
        /// The duration.
        /// </summary>
        [JsonPropertyName("duration")]
        public float Duration { get; set; }

        /// <summary>
        /// The sample MD5.
        /// </summary>
        [JsonPropertyName("sample_md5")]
        public string SampleMd5 { get; set; }

        /// <summary>
        /// The offset seconds.
        /// </summary>
        [JsonPropertyName("offset_seconds")]
        public int OffsetSeconds { get; set; }

        /// <summary>
        /// The window seconds.
        /// </summary>
        [JsonPropertyName("window_seconds")]
        public int WindowSeconds { get; set; }

        /// <summary>
        /// The analysis sample rate.
        /// </summary>
        [JsonPropertyName("analysis_sample_rate")]
        public int AnalysisSampleRate { get; set; }

        /// <summary>
        /// The analysis channels.
        /// </summary>
        [JsonPropertyName("analysis_channels")]
        public int AnalysisChannels { get; set; }

        /// <summary>
        /// The end of fade in.
        /// </summary>
        [JsonPropertyName("end_of_fade_in")]
        public float EndOfFadeIn { get; set; }

        /// <summary>
        /// The start of fade out.
        /// </summary>
        [JsonPropertyName("start_of_fade_out")]
        public float StartOfFadeOut { get; set; }

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
        /// The time signature.
        /// </summary>
        [JsonPropertyName("time_signature")]
        public int TimeSignature { get; set; }

        /// <summary>
        /// The time signature confidence.
        /// </summary>
        [JsonPropertyName("time_signature_confidence")]
        public float TimeSignatureConfidence { get; set; }

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
        /// The code string.
        /// </summary>
        [JsonPropertyName("codestring")]
        public string CodeString { get; set; }

        /// <summary>
        /// The code version.
        /// </summary>
        [JsonPropertyName("code_version")]
        public float CodeVersion { get; set; }

        /// <summary>
        /// The echo print string.
        /// </summary>
        [JsonPropertyName("echoprintstring")]
        public string EchoPrintString { get; set; }

        /// <summary>
        /// The echo print version.
        /// </summary>
        [JsonPropertyName("echoprint_version")]
        public float EchoPrintVersion { get; set; }

        /// <summary>
        /// The synch string.
        /// </summary>
        [JsonPropertyName("synchstring")]
        public string SynchString { get; set; }

        /// <summary>
        /// The synch version.
        /// </summary>
        [JsonPropertyName("synch_version")]
        public float SynchVersion { get; set; }

        /// <summary>
        /// The rhythm string.
        /// </summary>
        [JsonPropertyName("rhythmstring")]
        public string RhythmString { get; set; }

        /// <summary>
        /// The rhythm version.
        /// </summary>
        [JsonPropertyName("rhythm_version")]
        public float RhythmVersion { get; set; }
    }
}
