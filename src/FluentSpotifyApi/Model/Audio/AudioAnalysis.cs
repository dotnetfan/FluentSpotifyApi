using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Audio
{
    /// <summary>
    /// The audio analysis.
    /// </summary>
    public class AudioAnalysis : JsonObject
    {
        /// <summary>
        /// The meta.
        /// </summary>
        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }

        /// <summary>
        /// The audio track.
        /// </summary>
        [JsonPropertyName("track")]
        public AudioTrack Track { get; set; }

        /// <summary>
        /// The bars.
        /// </summary>
        [JsonPropertyName("bars")]
        public TimeInterval[] Bars { get; set; }

        /// <summary>
        /// The beats.
        /// </summary>
        [JsonPropertyName("beats")]
        public TimeInterval[] Beats { get; set; }

        /// <summary>
        /// The tatums.
        /// </summary>
        [JsonPropertyName("tatums")]
        public TimeInterval[] Tatums { get; set; }

        /// <summary>
        /// The sections.
        /// </summary>
        [JsonPropertyName("sections")]
        public Section[] Sections { get; set; }

        /// <summary>
        /// The segments.
        /// </summary>
        [JsonPropertyName("segments")]
        public Segment[] Segments { get; set; }
    }
}
