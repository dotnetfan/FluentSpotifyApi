using System;
using System.Collections.Generic;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    /// <summary>
    /// The authorization options
    /// </summary>
    public interface IAuthorizationOptions
    {
        /// <summary>
        /// Gets the Client ID.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        string ClientId { get; }

        /// <summary>
        /// Gets the URL for getting authorization from the Spotify Accounts Service.
        /// </summary>
        /// <value>
        /// The URL for getting authorization from the Spotify Accounts Service
        /// </value>
        Uri AuthorizationEndpoint { get; }

        /// <summary>
        /// Gets the list of strongly typed permissions. Can be used together with <see cref="DynamicScopes"/>.
        /// </summary>
        /// <value>
        /// The list of strongly typed permissions.
        /// </value>
        IList<Scope> Scopes { get; }

        /// <summary>
        /// Gets the list of permission strings. Can be used together with <see cref="Scopes"/>.
        /// </summary>
        /// <value>
        /// The list of permission strings.
        /// </value>
        IList<string> DynamicScopes { get;  }

        /// <summary>
        /// Gets a value indicating whether user is forced to re-approve the app during authorization.
        /// </summary>
        /// <value>
        /// If set to <c>true</c> the user is forced to re-approve the app during authorization.
        /// </value>
        bool ShowDialog { get; }
    }
}
