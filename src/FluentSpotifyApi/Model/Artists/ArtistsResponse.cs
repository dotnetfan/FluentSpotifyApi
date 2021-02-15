using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Artists
{
    /// <summary>
    /// The artists response.
    /// </summary>
    public class ArtistsResponse : JsonObject
    {
        /// <summary>
        /// The artists.
        /// </summary>
        [JsonPropertyName("artists")]
        public Artist[] Items { get; set; }
    }
}
