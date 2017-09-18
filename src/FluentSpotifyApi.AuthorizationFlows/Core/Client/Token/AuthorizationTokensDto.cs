using Newtonsoft.Json;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token
{
    /// <summary>
    /// The authorization tokens DTO.
    /// </summary>
    public class AuthorizationTokensDto
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the type of the token.
        /// </summary>
        /// <value>
        /// The type of the token.
        /// </value>
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// Gets or sets the scope.
        /// </summary>
        /// <value>
        /// The scope.
        /// </value>
        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }

        /// <summary>
        /// Gets or sets the expires in.
        /// </summary>
        /// <value>
        /// The expires in.
        /// </value>
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefresToken { get; set; }
    }
}
