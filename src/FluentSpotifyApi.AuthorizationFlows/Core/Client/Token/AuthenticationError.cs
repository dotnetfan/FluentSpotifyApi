using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token
{
    /// <summary>
    /// The authentication error.
    /// </summary>
    public class AuthenticationError : JsonObject
    {
        /// <summary>
        /// A high level description of the error as specified in RFC 6749 Section 5.2.
        /// </summary>
        [JsonPropertyName("error")]
        public string Error { get; set; }

        /// <summary>
        /// A more detailed description of the error as specified in RFC 6749 Section 4.1.2.1.
        /// </summary>
        [JsonPropertyName("error_description")]
        public string ErrorDescription { get; set; }
    }
}
