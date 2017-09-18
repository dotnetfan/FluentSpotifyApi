using Newtonsoft.Json;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token
{
    /// <summary>
    /// The access token DTO.
    /// </summary>
    public class AccessTokenDto
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        [JsonProperty(PropertyName = "access_token")]
        public string Token { get; set; }

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
    }
}
