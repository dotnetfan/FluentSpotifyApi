using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Episodes
{
    /// <summary>
    /// The simplified episodes page response.
    /// </summary>
    public class SimplifiedEpisodesPageResponse : JsonObject
    {
        /// <summary>
        /// The simplified episodes page.
        /// </summary>
        [JsonPropertyName("episodes")]
        public Page<SimplifiedEpisode> Page { get; set; }
    }
}
