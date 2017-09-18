using System;
using System.Collections.Generic;
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
        /// Sends request to the server and deserializes response to an instance of <typeparamref name="T" /> using JSON serializer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="queryStringParameters">The query string parameters.</param>
        /// <param name="requestBodyParameters">The request body parameters.</param>
        /// <param name="requestHeaders">The request headers.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns></returns>
        Task<T> SendAsync<T>(
            Uri uri, 
            HttpMethod httpMethod, 
            object queryStringParameters, 
            object requestBodyParameters, 
            IEnumerable<KeyValuePair<string, string>> requestHeaders,
            CancellationToken cancellationToken,
            params object[] routeValues);

        /// <summary>
        /// Sends request to the server and deserializes response to an instance of <typeparamref name="TResult" /> using JSON serializer.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TRequestBody">The type of the JSON serializable request body.</typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="queryStringParameters">The query string parameters.</param>
        /// <param name="requestBody">The JSON serializable request body.</param>
        /// <param name="requestHeaders">The request headers.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns></returns>
        Task<TResult> SendWithJsonBodyAsync<TResult, TRequestBody>(
            Uri uri, 
            HttpMethod httpMethod, 
            object queryStringParameters,
            TRequestBody requestBody,
            IEnumerable<KeyValuePair<string, string>> requestHeaders,
            CancellationToken cancellationToken,
            params object[] routeValues);
    }
}
