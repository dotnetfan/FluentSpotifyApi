using System;
using System.Collections.Generic;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    /// <summary>
    /// The options interface for <see cref="ISpotifyAuthorizationClient"/>.
    /// </summary>
    public interface ISpotifyAuthorizationClientOptions
    {
        /// <summary>
        /// Gets the Client ID.
        /// </summary>
        string ClientId { get; }

        /// <summary>
        /// Gets the URI of Spotify Accounts Service endpoint for performing user authorization.
        /// </summary>
        Uri AuthorizationEndpoint { get; }

        /// <summary>
        /// Gets the list of permissions requested by app. Use <see cref="SpotifyScopes"/> for well-known permissions.
        /// </summary>
        ICollection<string> Scopes { get; }

        /// <summary>
        /// Gets a value indicating whether user is forced to re-approve the app during user authorization.
        /// </summary>
        bool ShowDialog { get; }
    }
}
