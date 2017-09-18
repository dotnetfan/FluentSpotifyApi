using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Audio
{
    /// <summary>
    /// The meta.
    /// </summary>
    public class Meta
    {
        /// <summary>
        /// Gets or sets the analyzer version.
        /// </summary>
        /// <value>
        /// The analyzer version.
        /// </value>
        [JsonProperty(PropertyName = "analyzer_version")]
        public string AnalyzerVersion { get; set; }

        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        /// <value>
        /// The platform.
        /// </value>
        [JsonProperty(PropertyName = "platform")]
        public string Platform { get; set; }

        /// <summary>
        /// Gets or sets the detailed status.
        /// </summary>
        /// <value>
        /// The detailed status.
        /// </value>
        [JsonProperty(PropertyName = "detailed_status")]
        public string DetailedStatus { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        [JsonProperty(PropertyName = "status_code")]
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        [JsonProperty(PropertyName = "timestamp")]
        public int Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the analysis time.
        /// </summary>
        /// <value>
        /// The analysis time.
        /// </value>
        [JsonProperty(PropertyName = "analysis_time")]
        public float AnalysisTime { get; set; }

        /// <summary>
        /// Gets or sets the input process.
        /// </summary>
        /// <value>
        /// The input process.
        /// </value>
        [JsonProperty(PropertyName = "input_process")]
        public string InputProcess { get; set; }
    }
}
