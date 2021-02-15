using System;

namespace FluentSpotifyApi
{
    /// <summary>
    ///  The default values for <see cref="IFluentSpotifyClient"/>.
    /// </summary>
    public static class FluentSpotifyClientDefaults
    {
        /// <summary>
        /// The URI of Spotify Web API endpoint for obtaining user information.
        /// </summary>
        public static readonly Uri WebApiEndpoint = new Uri("https://api.spotify.com/v1/");
    }
}
