using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Handler
{
    /// <summary>
    /// Configuration options for Spotify authentication handler.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authentication.OAuth.OAuthOptions" />
    public class SpotifyOptions : OAuthOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyOptions"/> class.
        /// </summary>
        public SpotifyOptions()
        {
            this.CallbackPath = new PathString("/signin-spotify");
            this.AuthorizationEndpoint = SpotifyDefaults.AuthorizationEndpoint;
            this.TokenEndpoint = SpotifyDefaults.TokenEndpoint;
            this.UserInformationEndpoint = SpotifyDefaults.UserInformationEndpoint;

            this.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
            this.ClaimActions.MapCustomJson(ClaimTypes.Name, GetName);
        }

        /// <summary>
        /// Gets or sets a value indicating whether user is forced to re-approve the app during authentication. Defaults to <c>false</c>.
        /// </summary>
        public bool ShowDialog { get; set; }

        private static string GetName(JsonElement user)
        {
            return user.TryGetProperty("display_name", out var displayNameProp) && displayNameProp.GetString() is var displayName && !string.IsNullOrEmpty(displayName)
                ? displayName
                : user.GetProperty("id").GetString();
        }
    }
}
