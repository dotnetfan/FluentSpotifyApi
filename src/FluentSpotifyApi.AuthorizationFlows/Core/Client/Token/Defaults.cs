using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token
{
    /// <summary>
    /// Default values for tokens.
    /// </summary>
    public static class Defaults
    {
        /// <summary>
        /// The URL for getting OAuth tokens from Spotify Accounts Service.
        /// </summary>
        public static readonly Uri TokenEndpoint = new Uri("https://accounts.spotify.com/api/token");
    }
}
