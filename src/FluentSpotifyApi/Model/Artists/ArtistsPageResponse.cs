using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Artists
{
    /// <summary>
    /// The artists page response.
    /// </summary>
    public class ArtistsPageResponse : JsonObject
    {
        /// <summary>
        /// The artists page.
        /// </summary>
        [JsonPropertyName("artists")]
        public Page<Artist> Page { get; set; }
    }
}
