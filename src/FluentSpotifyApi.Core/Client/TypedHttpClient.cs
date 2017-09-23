using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Internal;
using FluentSpotifyApi.Core.Internal.Extensions;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Core.Client
{
    /// <summary>
    /// Strongly typed HTTP client.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Core.Client.ITypedHttpClient" />
    public class TypedHttpClient : ITypedHttpClient
    {
        private readonly IHttpClientWrapper httpClientWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypedHttpClient"/> class.
        /// </summary>
        /// <param name="httpClientWrapper">The HTTP client wrapper.</param>
        public TypedHttpClient(IHttpClientWrapper httpClientWrapper)
        {
            this.httpClientWrapper = httpClientWrapper;
        }

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
        public Task<T> SendAsync<T>(
            Uri uri, 
            HttpMethod httpMethod, 
            object queryStringParameters, 
            object requestBodyParameters, 
            IEnumerable<KeyValuePair<string, string>> requestHeaders,
            CancellationToken cancellationToken,
            params object[] routeValues)
        {
            return this.SendInternalAsync<T>(
                uri, 
                httpMethod, 
                queryStringParameters, 
                innerCt => Task.FromResult(CreateHttpFormUrlEncodedContent(requestBodyParameters)), 
                requestHeaders,
                cancellationToken,
                routeValues);
        }

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
        public Task<TResult> SendWithJsonBodyAsync<TResult, TRequestBody>(
            Uri uri, 
            HttpMethod httpMethod, 
            object queryStringParameters,
            TRequestBody requestBody,
            IEnumerable<KeyValuePair<string, string>> requestHeaders,
            CancellationToken cancellationToken,
            params object[] routeValues)
        {
            return this.SendInternalAsync<TResult>(
                uri, 
                httpMethod, 
                queryStringParameters, 
                innerCt => Task.FromResult(CreateJsonHttpStringContent(requestBody)), 
                requestHeaders,
                cancellationToken,
                routeValues);
        }

        /// <summary>
        /// Sends request to the server and deserializes response to an instance of <typeparamref name="T" /> using JSON serializer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="queryStringParameters">The query string parameters.</param>
        /// <param name="streamProvider">The stream provider.</param>
        /// <param name="streamContentType">The stream content type.</param>
        /// <param name="requestHeaders">The request headers.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns></returns>
        public Task<T> SendWithStreamBodyAsync<T>(
            Uri uri,
            HttpMethod httpMethod,
            object queryStringParameters,
            Func<CancellationToken, Task<Stream>> streamProvider,
            string streamContentType,
            IEnumerable<KeyValuePair<string, string>> requestHeaders,
            CancellationToken cancellationToken,
            params object[] routeValues)
        {
            return this.SendInternalAsync<T>(
                uri,
                httpMethod,
                queryStringParameters,
                innerCt => CreateBase64StreamContentAsync(streamProvider, streamContentType, innerCt),
                requestHeaders,
                cancellationToken,
                routeValues);
        }

        private static Uri GetCompleteUri(IList<KeyValuePair<Type, object>> transformersSourceValues, Uri uri, object queryStringParameters, object[] routeValues)
        {
            var relativeUri = string.Join("/", routeValues.EmptyIfNull().Select(item => GetOrTransform(item, transformersSourceValues)).Select(item => item.ToUrlString()));

            var uriBuilder = new UriBuilder(new Uri(uri, relativeUri));

            var queryString = 
                SpotifyObjectHelpers.GetPropertyBag(queryStringParameters)
                .Select(item => new KeyValuePair<string, object>(item.Key, GetOrTransform(item.Value, transformersSourceValues)))
                .GetQueryString();

            if (!string.IsNullOrEmpty(queryString))
            {
                uriBuilder.Query = queryString;
            }

            return uriBuilder.Uri;
        }

        private static object GetOrTransform(object value, IList<KeyValuePair<Type, object>> transformersSourceValues)
        {
            var transformer = value as ITransformer;
            if (transformer == null)
            {
                return value;
            }

            var firstMatchingSourceValue = transformersSourceValues.FirstOrDefault(item => transformer.SourceType == item.Key);
            if (firstMatchingSourceValue.Key == null)
            {
                throw new InvalidOperationException($"No matching source value has been found for the transformer of source type {transformer.SourceType}");
            }

            return transformer.Transform(firstMatchingSourceValue.Value);
        }

        private static HttpContent CreateHttpFormUrlEncodedContent(object parameters)
        {
            return parameters == null 
                ? 
                null 
                : 
                new FormUrlEncodedContent(SpotifyObjectHelpers.GetPropertyBag(parameters).Select(item => new KeyValuePair<string, string>(item.Key, item.Value.ToInvariantString())));
        }

        private static HttpContent CreateJsonHttpStringContent<T>(T value)
        {
            return new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
        }

        private static async Task<HttpContent> CreateBase64StreamContentAsync(
            Func<CancellationToken, Task<Stream>> streamProvider, 
            string streamContentType, 
            CancellationToken cancellationToken)
        {
            var stream = await streamProvider(cancellationToken).ConfigureAwait(false);

            ICryptoTransform base64Transform = null;

#if (NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
            base64Transform = new NCode.CryptoTransforms.ToBase64Transform();
#else
            base64Transform = new ToBase64Transform();
#endif

            var streamContent = new StreamContent(new CryptoStream(stream, base64Transform, CryptoStreamMode.Read));
            streamContent.Headers.Add("Content-Type", streamContentType);

            return streamContent;
        }

        private Task<T> SendInternalAsync<T>(
            Uri uri, 
            HttpMethod httpMethod, 
            object queryStringParameters, 
            Func<CancellationToken, Task<HttpContent>> requestContentProvider, 
            IEnumerable<KeyValuePair<string, string>> requestHeaders,
            CancellationToken cancellationToken,
            object[] routeValues)
        {
            return this.httpClientWrapper.SendAsync(
                new HttpRequest<T>(
                    new TransformableUriBuilder(transformersSourceValues => GetCompleteUri(transformersSourceValues, uri, queryStringParameters, routeValues)),
                    httpMethod,
                    requestHeaders.EmptyIfNull().ToList().AsReadOnly(),
                    requestContentProvider,
                    async (content, innerCt) =>
                    {
                        using (var stream = await content.ReadAsStreamAsync().ConfigureAwait(false))
                        using (var streamReader = new StreamReader(stream))
                        using (var reader = new JsonTextReader(streamReader))
                        {
                            var serializer = new JsonSerializer();
                            return serializer.Deserialize<T>(reader);
                        }
                    }),                                 
                    cancellationToken);
        }

        private class TransformableUriBuilder : IUriFromValuesBuilder
        {
            private readonly Func<IList<KeyValuePair<Type, object>>, Uri> uriBuilder;

            private readonly IList<KeyValuePair<Type, object>> transfromerSourceValues;

            public TransformableUriBuilder(Func<IList<KeyValuePair<Type, object>>, Uri> func) : this(func, new KeyValuePair<Type, object>[0])
            {
            }

            public TransformableUriBuilder(Func<IList<KeyValuePair<Type, object>>, Uri> uriBuilder, IList<KeyValuePair<Type, object>> transfromerSourceValues)
            {
                this.uriBuilder = uriBuilder;
                this.transfromerSourceValues = transfromerSourceValues;
            }

            public IUriFromValuesBuilder AddValue<T>(T value)
            {
                return new TransformableUriBuilder(this.uriBuilder, this.transfromerSourceValues.Concat((new KeyValuePair<Type, object>(typeof(T), value)).Yield()).ToList());
            }

            public Uri Build()
            {
                return this.uriBuilder(this.transfromerSourceValues);
            }
        }
    }
}
