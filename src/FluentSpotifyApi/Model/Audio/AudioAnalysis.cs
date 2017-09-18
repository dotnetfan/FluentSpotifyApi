using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Audio
{
    /// <summary>
    /// The audio analysis.
    /// </summary>
    public class AudioAnalysis
    {
        /// <summary>
        /// Gets or sets the meta.
        /// </summary>
        /// <value>
        /// The meta.
        /// </value>
        [JsonProperty(PropertyName = "meta")]
        public Meta Meta { get; set; }

        /// <summary>
        /// Gets or sets the track.
        /// </summary>
        /// <value>
        /// The track.
        /// </value>
        [JsonProperty(PropertyName = "track")]
        public Track Track { get; set; }

        /// <summary>
        /// Gets or sets the bars.
        /// </summary>
        /// <value>
        /// The bars.
        /// </value>
        [JsonProperty(PropertyName = "bars")]
        public Bar[] Bars { get; set; }

        /// <summary>
        /// Gets or sets the beats.
        /// </summary>
        /// <value>
        /// The beats.
        /// </value>
        [JsonProperty(PropertyName = "beats")]
        public Beat[] Beats { get; set; }

        /// <summary>
        /// Gets or sets the tatums.
        /// </summary>
        /// <value>
        /// The tatums.
        /// </value>
        [JsonProperty(PropertyName = "tatums")]
        public Tatum[] Tatums { get; set; }

        /// <summary>
        /// Gets or sets the sections.
        /// </summary>
        /// <value>
        /// The sections.
        /// </value>
        [JsonProperty(PropertyName = "sections")]
        public Section[] Sections { get; set; }

        /// <summary>
        /// Gets or sets the segments.
        /// </summary>
        /// <value>
        /// The segments.
        /// </value>
        [JsonProperty(PropertyName = "segments")]
        public Segment[] Segments { get; set; }
    }
}
