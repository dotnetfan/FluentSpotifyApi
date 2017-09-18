using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.User
{
    /// <summary>
    /// The user client options
    /// </summary>
    public interface IUserClientOptions
    {
        /// <summary>
        /// Gets the URL for getting info about current user.
        /// </summary>
        /// <value>
        /// The URL for getting info about current user.
        /// </value>
        Uri UserInformationEndpoint { get; }
    }
}
