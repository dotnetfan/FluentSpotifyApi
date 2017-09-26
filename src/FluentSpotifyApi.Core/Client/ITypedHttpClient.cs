using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Core.Client
{
    /// <summary>
    /// Strongly typed HTTP client.
    /// </summary>
    public interface ITypedHttpClient
    {
        /// <summary>
        /// Sends request to the server and deserializes response to an instance of <typeparamref name="TResult" /> using JSON serializer.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="uriParts">The URI parts.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="requestHeaders">The request headers.</param>
        /// <param name="requestBodyParameters">The request body parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<TResult> SendAsync<TResult>(
            UriParts uriParts, 
            HttpMethod httpMethod,              
            IEnumerable<KeyValuePair<string, string>> requestHeaders,
            object requestBodyParameters,
            CancellationToken cancellationToken);

        /// <summary>
        /// Sends request to the server and deserializes response to an instance of <typeparamref name="TResult" /> using JSON serializer.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TRequestBody">The type of the JSON serializable request body.</typeparam>
        /// <param name="uriParts">The URI parts.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="requestHeaders">The request headers.</param>
        /// <param name="requestBody">The JSON serializable request body.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<TResult> SendWithJsonBodyAsync<TResult, TRequestBody>(
            UriParts uriParts,
            HttpMethod httpMethod,             
            IEnumerable<KeyValuePair<string, string>> requestHeaders,
            TRequestBody requestBody,
            CancellationToken cancellationToken);

        /// <summary>
        /// Sends request to the server and deserializes response to an instance of <typeparamref name="TResult" /> using JSON serializer.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="uriParts">The URI parts.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="requestHeaders">The request headers.</param>
        /// <param name="streamProvider">The stream provider.</param>
        /// <param name="streamContentType">The stream content type.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<TResult> SendWithStreamBodyAsync<TResult>(
            UriParts uriParts,
            HttpMethod httpMethod,
            IEnumerable<KeyValuePair<string, string>> requestHeaders,
            Func<CancellationToken, Task<Stream>> streamProvider,
            string streamContentType,
            CancellationToken cancellationToken);
    }
}
