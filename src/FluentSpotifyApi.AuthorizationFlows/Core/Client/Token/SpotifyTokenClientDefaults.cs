using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token
{
    /// <summary>
    /// The default values for <see cref="ISpotifyTokenClient"/>.
    /// </summary>
    public static class SpotifyTokenClientDefaults
    {
        /// <summary>
        /// The URI of Spotify Accounts Service endpoint for obtaining OAuth tokens.
        /// </summary>
        public static readonly Uri TokenEndpoint = new Uri("https://accounts.spotify.com/api/token");
    }
}
