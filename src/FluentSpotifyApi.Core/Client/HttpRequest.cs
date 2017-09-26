using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Core.Client
{
    /// <summary>
    /// The class that represents an HTTP Request.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public sealed class HttpRequest<TResult>
    {
        internal HttpRequest(
            IUriFromValuesBuilder uriFromValuesBuilder, 
            HttpMethod httpMethod,
            IReadOnlyCollection<KeyValuePair<string, string>> requestHeaders,            
            Func<CancellationToken, Task<HttpContent>> requestContentProvider,
            Func<HttpResponseMessage, CancellationToken, Task<TResult>> responseProcessor)
        {
            this.UriFromValuesBuilder = uriFromValuesBuilder;
            this.HttpMethod = httpMethod;
            this.RequestHeaders = requestHeaders;
            this.RequestContentProvider = requestContentProvider;
            this.ResponseProcessor = responseProcessor;                       
        }

        private HttpRequest(HttpRequest<TResult> httpRequest)
            : this(
                  httpRequest.UriFromValuesBuilder, 
                  httpRequest.HttpMethod, 
                  httpRequest.RequestHeaders, 
                  httpRequest.RequestContentProvider, 
                  httpRequest.ResponseProcessor)
        {
        }

        /// <summary>
        /// Gets the URI from values builder.
        /// </summary>
        /// <value>
        /// The URI from values builder.
        /// </value>
        public IUriFromValuesBuilder UriFromValuesBuilder { get; private set; }

        /// <summary>
        /// Gets the HTTP method.
        /// </summary>
        /// <value>
        /// The HTTP method.
        /// </value>
        public HttpMethod HttpMethod { get; private set; }

        /// <summary>
        /// Gets the request headers.
        /// </summary>
        /// <value>
        /// The request headers.
        /// </value>
        public IReadOnlyCollection<KeyValuePair<string, string>> RequestHeaders { get; private set; }

        /// <summary>
        /// Gets the request content provider.
        /// </summary>
        /// <value>
        /// The request content provider.
        /// </value>
        public Func<CancellationToken, Task<HttpContent>> RequestContentProvider { get; private set; }

        /// <summary>
        /// Gets the response processor.
        /// </summary>
        /// <value>
        /// The response processor.
        /// </value>
        public Func<HttpResponseMessage, CancellationToken, Task<TResult>> ResponseProcessor { get; private set; }

        /// <summary>
        /// Replaces the URI from values builder.
        /// </summary>
        /// <param name="valueProvider">The value provider.</param>
        /// <returns></returns>
        public HttpRequest<TResult> ReplaceUriFromValuesBuilder(Func<IUriFromValuesBuilder, IUriFromValuesBuilder> valueProvider)
        {
            var result = new HttpRequest<TResult>(this);
            result.UriFromValuesBuilder = valueProvider(result.UriFromValuesBuilder);

            return result;
        }

        /// <summary>
        /// Replaces the HTTP method.
        /// </summary>
        /// <param name="valueProvider">The value provider.</param>
        /// <returns></returns>
        public HttpRequest<TResult> ReplaceHttpMethod(Func<HttpMethod, HttpMethod> valueProvider)
        {
            var result = new HttpRequest<TResult>(this);
            result.HttpMethod = valueProvider(result.HttpMethod);

            return result;
        }

        /// <summary>
        /// Replaces the request headers.
        /// </summary>
        /// <param name="valueProvider">The value provider.</param>
        /// <returns></returns>
        public HttpRequest<TResult> ReplaceRequestHeaders(Func<IReadOnlyCollection<KeyValuePair<string, string>>, IReadOnlyCollection<KeyValuePair<string, string>>> valueProvider)
        {
            var result = new HttpRequest<TResult>(this);
            result.RequestHeaders = valueProvider(result.RequestHeaders);

            return result;
        }

        /// <summary>
        /// Replaces the request content provider.
        /// </summary>
        /// <param name="valueProvider">The value provider.</param>
        /// <returns></returns>
        public HttpRequest<TResult> ReplaceRequestContentProvider(Func<Func<CancellationToken, Task<HttpContent>>, Func<CancellationToken, Task<HttpContent>>> valueProvider)
        {
            var result = new HttpRequest<TResult>(this);
            result.RequestContentProvider = valueProvider(result.RequestContentProvider);

            return result;
        }

        /// <summary>
        /// Replaces the response processor.
        /// </summary>
        /// <param name="valueProvider">The value provider.</param>
        /// <returns></returns>
        public HttpRequest<TResult> ReplaceResponseProcessor(Func<Func<HttpResponseMessage, CancellationToken, Task<TResult>>, Func<HttpResponseMessage, CancellationToken, Task<TResult>>> valueProvider)
        {
            var result = new HttpRequest<TResult>(this);
            result.ResponseProcessor = valueProvider(result.ResponseProcessor);

            return result;
        }
    }
}
