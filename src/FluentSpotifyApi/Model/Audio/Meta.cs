using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Audio
{
    /// <summary>
    /// The meta.
    /// </summary>
    public class Meta : JsonObject
    {
        /// <summary>
        /// The analyzer version.
        /// </summary>
        [JsonPropertyName("analyzer_version")]
        public string AnalyzerVersion { get; set; }

        /// <summary>
        /// The platform.
        /// </summary>
        [JsonPropertyName("platform")]
        public string Platform { get; set; }

        /// <summary>
        /// The detailed status.
        /// </summary>
        [JsonPropertyName("detailed_status")]
        public string DetailedStatus { get; set; }

        /// <summary>
        /// The status code.
        /// </summary>
        [JsonPropertyName("status_code")]
        public int StatusCode { get; set; }

        /// <summary>
        /// The timestamp.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public int Timestamp { get; set; }

        /// <summary>
        /// The analysis time.
        /// </summary>
        [JsonPropertyName("analysis_time")]
        public float AnalysisTime { get; set; }

        /// <summary>
        /// The input process.
        /// </summary>
        [JsonPropertyName("input_process")]
        public string InputProcess { get; set; }
    }
}
