using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    /// <summary>
    /// The interface for getting callback URI. 
    /// </summary>
    public interface IAuthorizationCallbackUriProvider
    {
        /// <summary>
        /// Gets the callback URI. The URI must be registered in the "Redirect URIs" section of the Spotify App.
        /// </summary>
        /// <returns></returns>
        Uri Get();
    }
}
