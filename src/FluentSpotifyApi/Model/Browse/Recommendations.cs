using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Browse
{
    /// <summary>
    /// The recommendations.
    /// </summary>
    public class Recommendations
    {
        /// <summary>
        /// Gets or sets the seeds.
        /// </summary>
        /// <value>
        /// The seeds.
        /// </value>
        [JsonProperty(PropertyName = "seeds")]
        public RecommendationsSeed[] Seeds { get; set; }

        /// <summary>
        /// Gets or sets the tracks.
        /// </summary>
        /// <value>
        /// The tracks.
        /// </value>
        [JsonProperty(PropertyName = "tracks")]
        public SimpleTrack[] Tracks { get; set; }
    }
}
