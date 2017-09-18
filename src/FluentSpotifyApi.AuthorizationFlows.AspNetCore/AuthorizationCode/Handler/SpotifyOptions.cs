using System.Security.Claims;
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
            this.Scope.Add(SpotifyDefaults.UserReadEmailScope);

            ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
            ClaimActions.MapCustomJson(ClaimTypes.Name, SpotifyHelper.GetName);
            ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
            ClaimActions.MapCustomJson(SpotifyClaimTypes.ProfilePicture, SpotifyHelper.GetProfilePicture);
        }

        /// <summary>
        /// Gets or sets a value indicating whether user is forced to re-approve the app during authentication. Set to <c>false</c> by default.
        /// </summary>
        /// <value>
        /// If set to <c>true</c> the user is forced to re-approve the app during authentication.
        /// </value>
        public bool ShowDialog { get; set; }        
    }
}
