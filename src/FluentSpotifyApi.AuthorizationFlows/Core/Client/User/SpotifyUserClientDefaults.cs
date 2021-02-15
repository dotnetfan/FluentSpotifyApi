using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.User
{
    /// <summary>
    /// The default values for <see cref="ISpotifyUserClient"/>.
    /// </summary>
    public static class SpotifyUserClientDefaults
    {
        /// <summary>
        /// The URI of Spotify Web API endpoint for obtaining user information.
        /// </summary>
        public static readonly Uri UserInformationEndpoint = new Uri("https://api.spotify.com/v1/me");
    }
}
