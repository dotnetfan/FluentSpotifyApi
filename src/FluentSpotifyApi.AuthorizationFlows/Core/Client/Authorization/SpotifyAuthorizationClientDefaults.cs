using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    /// <summary>
    ///  The default values for <see cref="ISpotifyAuthorizationClient"/>.
    /// </summary>
    public static class SpotifyAuthorizationClientDefaults
    {
        /// <summary>
        /// The URI of Spotify Accounts Service endpoint for performing user authorization.
        /// </summary>
        public static readonly Uri AuthorizationEndpoint = new Uri("https://accounts.spotify.com/authorize");
    }
}
