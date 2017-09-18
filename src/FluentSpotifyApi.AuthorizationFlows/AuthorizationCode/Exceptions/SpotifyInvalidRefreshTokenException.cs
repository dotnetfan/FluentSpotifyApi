using System;
using FluentSpotifyApi.Core.Exceptions;

namespace FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the refresh token is invalid.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Core.Exceptions.SpotifyServiceException" />
    public class SpotifyInvalidRefreshTokenException : SpotifyServiceException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyInvalidRefreshTokenException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public SpotifyInvalidRefreshTokenException(Exception innerException) : base("Invalid refresh token exception has occurred.", innerException)
        {
        }
    }
}
