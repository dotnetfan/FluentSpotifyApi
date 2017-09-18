using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.User
{
    /// <summary>
    /// The default values for user.
    /// </summary>
    public static class Defaults
    {
        /// <summary>
        /// The URL for getting info about current user.
        /// </summary>
        public static readonly Uri UserInformationEndpoint = new Uri("https://api.spotify.com/v1/me");
    }
}
