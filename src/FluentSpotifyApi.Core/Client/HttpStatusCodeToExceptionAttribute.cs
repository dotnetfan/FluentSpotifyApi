using System;
using System.Net;
using System.Net.Http;

namespace FluentSpotifyApi.Core.Client
{
    /// <summary>
    /// This attribute instructs HTTP response processor to throw the exception of <see cref="ExceptionType"/> type 
    /// when <see cref="HttpResponseMessage.StatusCode"/> matches <see cref="StatusCode"/>. 
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class HttpStatusCodeToExceptionAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the exception.
        /// </summary>
        /// <value>
        /// The type of the exception.
        /// </value>
        public Type ExceptionType { get; set; }
    }
}
