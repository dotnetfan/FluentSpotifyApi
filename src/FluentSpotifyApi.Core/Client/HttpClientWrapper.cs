using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Exceptions;
using FluentSpotifyApi.Core.Internal.Extensions;

namespace FluentSpotifyApi.Core.Client
{
    /// <summary>
    /// Wraps <see cref="System.Net.Http.HttpClient"/> instance.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Core.Client.IHttpClientWrapper" />
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly System.Net.Http.HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientWrapper"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        public HttpClientWrapper(System.Net.Http.HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Sends request to the server.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TResult> SendAsync<TResult>(HttpRequest<TResult> httpRequest, CancellationToken cancellationToken)
        {
            var uri = httpRequest.UriFromValuesBuilder.Build();

            try
            {
                using (var request = new HttpRequestMessage())
                {
                    request.RequestUri = uri;
                    request.Method = httpRequest.HttpMethod;
                    request.Content = httpRequest.RequestContentProvider?.Invoke();

                    foreach (var item in httpRequest.RequestHeaders.EmptyIfNull())
                    {
                        request.Headers.Add(item.Key, item.Value);
                    }

                    using (var response = await this.httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false))
                    {
                        TResult result = default(TResult);

                        await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
                        if (response.Content != null)
                        {
                            result = await httpRequest.ResponseProcessor(response.Content).ConfigureAwait(false);
                        }

                        return result;
                    }
                }
            }
            catch (Exception e) when (!(e is SpotifyHttpResponseWithErrorCodeException))
            {
                if (e is OperationCanceledException operationCanceledException)
                {
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        throw new SpotifyHttpRequestCanceledException(operationCanceledException);
                    }

                    throw;
                }
                else
                {
                    throw new SpotifyHttpRequestException("An error has occurred while sending request to the server.", e);
                }
            }
        }
    }
}
