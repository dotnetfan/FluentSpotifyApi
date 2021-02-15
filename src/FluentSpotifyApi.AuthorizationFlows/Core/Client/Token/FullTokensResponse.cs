using System.Text.Json.Serialization;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token
{
    /// <summary>
    /// The full tokens response.
    /// </summary>
    public class FullTokensResponse : IAccessTokenResponse
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

        /// <summary>
        /// A token that can be sent to the Spotify Accounts service in place of an authorization code.
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// A space-separated list of scopes which have been granted for this <see cref="AccessToken"/>.
        /// </summary>
        [JsonPropertyName("scope")]
        public string Scope { get; set; }
    }
}
