using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#pragma warning disable SA1402 // File may only contain a single type

namespace FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode
{
    internal class AuthenticationTicketRepositoryItem
    {
        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_at")]
        public DateTimeOffset? ExpiresAt { get; set; }

        [JsonPropertyName("user_claims")]
        public IDictionary<string, string> UserClaims { get; set; }
    }
}
