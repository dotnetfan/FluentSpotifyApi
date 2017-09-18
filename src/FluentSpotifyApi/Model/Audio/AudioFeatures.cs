using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Audio
{
    /// <summary>
    /// The audio features.
    /// </summary>
    public class AudioFeatures
    {
        /// <summary>
        /// Gets or sets the acousticness.
        /// </summary>
        /// <value>
        /// The acousticness.
        /// </value>
        [JsonProperty(PropertyName = "acousticness")]
        public float Acousticness { get; set; }

        /// <summary>
        /// Gets or sets the analysis URL.
        /// </summary>
        /// <value>
        /// The analysis URL.
        /// </value>
        [JsonProperty(PropertyName = "analysis_url")]
        public string AnalysisUrl { get; set; }

        /// <summary>
        /// Gets or sets the danceability.
        /// </summary>
        /// <value>
        /// The danceability.
        /// </value>
        [JsonProperty(PropertyName = "danceability")]
        public float Danceability { get; set; }

        /// <summary>
        /// Gets or sets the duration ms.
        /// </summary>
        /// <value>
        /// The duration ms.
        /// </value>
        [JsonProperty(PropertyName = "duration_ms")]
        public int DurationMs { get; set; }

        /// <summary>
        /// Gets or sets the energy.
        /// </summary>
        /// <value>
        /// The energy.
        /// </value>
        [JsonProperty(PropertyName = "energy")]
        public float Energy { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the instrumentalness.
        /// </summary>
        /// <value>
        /// The instrumentalness.
        /// </value>
        [JsonProperty(PropertyName = "instrumentalness")]
        public float Instrumentalness { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        [JsonProperty(PropertyName = "key")]
        public int Key { get; set; }

        /// <summary>
        /// Gets or sets the liveness.
        /// </summary>
        /// <value>
        /// The liveness.
        /// </value>
        [JsonProperty(PropertyName = "liveness")]
        public float Liveness { get; set; }

        /// <summary>
        /// Gets or sets the loudness.
        /// </summary>
        /// <value>
        /// The loudness.
        /// </value>
        [JsonProperty(PropertyName = "loudness")]
        public float Loudness { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>
        /// The mode.
        /// </value>
        [JsonProperty(PropertyName = "mode")]
        public int Mode { get; set; }

        /// <summary>
        /// Gets or sets the speechiness.
        /// </summary>
        /// <value>
        /// The speechiness.
        /// </value>
        [JsonProperty(PropertyName = "speechiness")]
        public float Speechiness { get; set; }

        /// <summary>
        /// Gets or sets the tempo.
        /// </summary>
        /// <value>
        /// The tempo.
        /// </value>
        [JsonProperty(PropertyName = "tempo")]
        public float Tempo { get; set; }

        /// <summary>
        /// Gets or sets the time signature.
        /// </summary>
        /// <value>
        /// The time signature.
        /// </value>
        [JsonProperty(PropertyName = "time_signature")]
        public int TimeSignature { get; set; }

        /// <summary>
        /// Gets or sets the track href.
        /// </summary>
        /// <value>
        /// The track href.
        /// </value>
        [JsonProperty(PropertyName = "track_href")]
        public string TrackHref { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets the valence.
        /// </summary>
        /// <value>
        /// The valence.
        /// </value>
        [JsonProperty(PropertyName = "valence")]
        public float Valence { get; set; }
    }
}
