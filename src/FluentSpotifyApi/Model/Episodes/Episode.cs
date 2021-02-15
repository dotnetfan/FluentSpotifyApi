using System.Text.Json.Serialization;
using FluentSpotifyApi.Model.Shows;

namespace FluentSpotifyApi.Model.Episodes
{
    /// <summary>
    /// The episode.
    /// </summary>
    /// <seealso cref="EpisodeBase"/>
    public class Episode : EpisodeBase
    {
        /// <summary>
        /// The show on which the episode belongs.
        /// </summary>
        [JsonPropertyName("show")]
        public SimplifiedShow Show { get; set; }
    }
}
