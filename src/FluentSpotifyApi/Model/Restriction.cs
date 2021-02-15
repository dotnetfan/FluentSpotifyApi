using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The restriction.
    /// </summary>
    public class Restriction : JsonObject
    {
        /// <summary>
        /// The reason for the restriction.
        /// </summary>
        [JsonPropertyName("reason")]
        public string Reason { get; set; }
    }
}
