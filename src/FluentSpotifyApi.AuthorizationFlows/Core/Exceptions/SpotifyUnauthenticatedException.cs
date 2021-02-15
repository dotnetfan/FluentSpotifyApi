using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Exceptions
{
    /// <summary>
    /// The exception that is thrown when one of the Spotify User Authorization Flow is used and Spotify Web API is called without user being authenticated.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class SpotifyUnauthenticatedException : Exception
    {
    }
}
