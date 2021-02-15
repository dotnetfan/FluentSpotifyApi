using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token
{
    /// <summary>
    /// The access token response.
    /// </summary>
    public class AccessTokenResponse : JsonObject, IAccessTokenResponse
    {
        /// <inheritdoc />
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
