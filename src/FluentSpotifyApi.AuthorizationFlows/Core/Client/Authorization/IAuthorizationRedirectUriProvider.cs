using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    /// <summary>
    /// The interface for getting redirect URI.
    /// </summary>
    public interface IAuthorizationRedirectUriProvider
    {
        /// <summary>
        /// Gets the redirect URI. The URI must be registered in the "Redirect URIs" section of the Spotify App.
        /// </summary>
        /// <returns></returns>
        Uri Get();
    }
}
