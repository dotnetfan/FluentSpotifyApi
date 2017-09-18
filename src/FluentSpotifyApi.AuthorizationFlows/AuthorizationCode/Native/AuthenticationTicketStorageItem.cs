using FluentSpotifyApi.Core.Model;
using Newtonsoft.Json;

namespace FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native
{
    internal class AuthenticationTicketStorageItem
    {
        [JsonProperty(PropertyName = "version")]
        public int Version { get; set; }

        [JsonProperty(PropertyName = "authorization_key")]
        public string AuthorizationKey { get; set; }

        [JsonProperty(PropertyName = "user")]
        public PrivateUser User { get; set; }
    }
}
