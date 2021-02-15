using System.Text.Json.Serialization;

namespace FluentSpotifyApi.Core.Model
{
    /// <summary>
    /// The regular error response.
    /// </summary>
    public class RegularErrorResponse : JsonObject
    {
        /// <summary>
        /// The regular error.
        /// </summary>
        [JsonPropertyName("error")]
        public RegularError Error { get; set; }
    }
}
