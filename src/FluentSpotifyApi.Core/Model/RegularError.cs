using System.Text.Json.Serialization;

namespace FluentSpotifyApi.Core.Model
{
    /// <summary>
    /// The regular error.
    /// </summary>
    public class RegularError : JsonObject
    {
        /// <summary>
        /// The HTTP status code that is also returned in the response header.
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }

        /// <summary>
        /// A short description of the cause of the error.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// The reason code for the error. Currently available only from Player API.
        /// </summary>
        [JsonPropertyName("reason")]
        public string Reason { get; set; }
    }
}
