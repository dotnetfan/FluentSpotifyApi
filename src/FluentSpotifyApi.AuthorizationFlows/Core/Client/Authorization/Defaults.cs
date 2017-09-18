using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    /// <summary>
    /// Default values for authorization.
    /// </summary>
    public static class Defaults
    {
        /// <summary>
        /// The URL for getting authorization from the Spotify Accounts Service.
        /// </summary>
        public static readonly Uri AuthorizationEndpoint = new Uri("https://accounts.spotify.com/authorize");
    }
}
