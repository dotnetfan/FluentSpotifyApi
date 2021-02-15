using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Episodes
{
    /// <summary>
    /// The episodes response.
    /// </summary>
    public class EpisodesResponse : JsonObject
    {
        /// <summary>
        /// The episodes.
        /// </summary>
        [JsonPropertyName("episodes")]
        public Episode[] Items { get; set; }
    }
}