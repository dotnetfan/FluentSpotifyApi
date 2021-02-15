using System.Text.Json.Serialization;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token
{
    /// <summary>
    /// The access token response with scope.
    /// </summary>
    public class ScopedAccessTokenResponse : IAccessTokenResponse
    {
        /// <inheritdoc />
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// A space-separated list of scopes which have been granted for this <see cref="AccessToken"/>.
        /// </summary>
        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
