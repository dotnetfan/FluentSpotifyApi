using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Exceptions;

namespace FluentSpotifyApi.Core.HttpHandlers
{
    /// <summary>
    /// The base <see cref="DelegatingHandler"/> that shields exceptions thrown by <see cref="HttpClient"/> and transforms error responses into exceptions.
    /// </summary>
    /// <typeparam name="TClient">The client type.</typeparam>
    /// <typeparam name="TErrorResponse">The error response type.</typeparam>
    /// <typeparam name="TError">The error type that is selected from <typeparamref name="TError"/>.</typeparam>
    public abstract class SpotifyErrorHandler<TClient, TErrorResponse, TError> : DelegatingHandler
        where TErrorResponse : class
        where TError : class
    {
        private readonly Func<TErrorResponse, TError> errorSelector;
        private readonly Func<Type, HttpStatusCode, string, TError, SpotifyHttpResponseException<TError>> exceptionFromErrorFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyErrorHandler{TClient,TErrorResponse,TError}" /> class.
        /// </summary>
        /// <param name="errorSelector">
        /// A transform function that is applied to instance of <typeparamref name="TErrorResponse"/> in order to get instance of <typeparamref name="TError"/>.
        /// </param>
        /// <param name="exceptionFromErrorFactory">A factory that creates concrete implementation of <see cref="SpotifyHttpResponseException&lt;TError&gt;"/>.</param>
        public SpotifyErrorHandler(Func<TErrorResponse, TError> errorSelector, Func<Type, HttpStatusCode, string, TError, SpotifyHttpResponseException<TError>> exceptionFromErrorFactory)
        {
            this.errorSelector = errorSelector;
            this.exceptionFromErrorFactory = exceptionFromErrorFactory;
        }

        /// <inheritdoc />
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
                await this.EnsureSuccessStatusCodeAsync(response, cancellationToken).ConfigureAwait(false);
                return response;
            }
            catch (HttpRequestException e)
            {
                throw new SpotifyHttpRequestException(typeof(TClient), e);
            }
        }

        private async Task EnsureSuccessStatusCodeAsync(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            TErrorResponse errorResponse = null;
            if (response.Content != null)
            {
                string content = null;
                try
                {
#if (NETSTANDARD2_0)
                    content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#else
                    content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#endif
                }
                catch (Exception e)
                {
                    throw new SpotifyHttpResponseException(
                        "Error response has been returned from the server. An error has occurred while getting error content from the server. See inner exception for details.",
                        typeof(TClient),
                        response.StatusCode,
                        null,
                        e);
                }

                try
                {
                    errorResponse = JsonSerializer.Deserialize<TErrorResponse>(content);
                }
                catch (Exception e)
                {
                    throw new SpotifyHttpResponseException(
                        $"Error response has been returned from the server. An error has occurred while deserializing error content to '{typeof(TErrorResponse).FullName}'. See inner exception for details.",
                        typeof(TClient),
                        response.StatusCode,
                        content,
                        e);
                }

                var error = errorResponse != null ? this.errorSelector(errorResponse) : null;
                if (error != null)
                {
                    throw this.exceptionFromErrorFactory(typeof(TClient), response.StatusCode, content, error);
                }
                else
                {
                    throw new SpotifyHttpResponseException(typeof(TClient), response.StatusCode, content);
                }
            }
        }
    }
}
