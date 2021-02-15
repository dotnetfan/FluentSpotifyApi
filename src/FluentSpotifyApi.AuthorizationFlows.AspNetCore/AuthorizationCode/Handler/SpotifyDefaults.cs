using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.User;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Handler
{
    /// <summary>
    /// Default values for Spotify authentication.
    /// </summary>
    public static class SpotifyDefaults
    {
        /// <summary>
        /// The authentication scheme.
        /// </summary>
        public const string AuthenticationScheme = "Spotify";

        /// <summary>
        /// The display name.
        /// </summary>
        public const string DisplayName = "Spotify";

        /// <summary>
        /// The URL for getting authorization from the Spotify Accounts Service.
        /// </summary>
        public static readonly string AuthorizationEndpoint = SpotifyAuthorizationClientDefaults.AuthorizationEndpoint.AbsoluteUri;

        /// <summary>
        /// The URL for getting OAuth tokens from Spotify Accounts Service.
        /// </summary>
        public static readonly string TokenEndpoint = SpotifyTokenClientDefaults.TokenEndpoint.AbsoluteUri;

        /// <summary>
        /// The URL for getting info about current user.
        /// </summary>
        public static readonly string UserInformationEndpoint = SpotifyUserClientDefaults.UserInformationEndpoint.AbsoluteUri;
    }
}
