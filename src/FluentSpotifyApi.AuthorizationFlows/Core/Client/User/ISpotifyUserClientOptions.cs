using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.User
{
    /// <summary>
    /// The options interface for <see cref="ISpotifyUserClient"/>.
    /// </summary>
    public interface ISpotifyUserClientOptions
    {
        /// <summary>
        /// Gets the URI of Spotify Web API endpoint for obtaining user information.
        /// </summary>
        Uri UserInformationEndpoint { get; }
    }
}
