using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Model.Tracks;

namespace FluentSpotifyApi.Model.Browse
{
    /// <summary>
    /// The recommendations.
    /// </summary>
    public class Recommendations : JsonObject
    {
        /// <summary>
        /// An array of recommendation seed objects.
        /// </summary>
        [JsonPropertyName("seeds")]
        public RecommendationsSeed[] Seeds { get; set; }

        /// <summary>
        /// An array of simplified track objects ordered according to the parameters supplied.
        /// </summary>
        [JsonPropertyName("tracks")]
        public SimplifiedTrack[] Tracks { get; set; }
    }
}
