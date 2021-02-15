using System;
using System.Net;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Core.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a response with error <see cref="HttpStatusCode"/> and Spotify error content of type <see cref="RegularError"/>
    /// is returned from the Spotify service.
    /// </summary>
    public class SpotifyRegularErrorException : SpotifyHttpResponseException<RegularError>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyRegularErrorException" /> class.
        /// </summary>
        /// <param name="clientType">The client type.</param>
        /// <param name="errorCode">The error status code.</param>
        /// <param name="content">The content.</param>
        /// <param name="error">The error.</param>
        public SpotifyRegularErrorException(Type clientType, HttpStatusCode errorCode, string content, RegularError error)
            : base(clientType, errorCode, content, error)
        {
        }
    }
}
