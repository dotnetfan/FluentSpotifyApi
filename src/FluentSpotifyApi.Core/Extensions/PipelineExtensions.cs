using System;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Client;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.Core.Extensions
{
    /// <summary>
    /// The set of <see cref="IPipeline"/> extensions.
    /// </summary>
    public static class PipelineExtensions
    {
        /// <summary>
        /// Adds a delegate to the client execution chain. 
        /// The parameter of HttpRequest type can be used to inspect and modify HTTP request. 
        /// The <see cref="Type"/> parameter contains the type of the HTTP request result.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="func">The delegate that will be executed during client call.</param>
        /// <returns></returns>
        public static IPipeline AddDelegate(
            this IPipeline pipeline,
            Func<Func<HttpRequest<object>, CancellationToken, Task<object>>, HttpRequest<object>, Type, CancellationToken, Task<object>> func)
        {
            return pipeline.Add(new DelegatedPipelineItem(func));
        }

        /// <summary>
        /// Adds a delegate to the client execution chain.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="func">The delegate that will be executed during client call.</param>
        /// <returns></returns>
        public static IPipeline AddDelegate(
            this IPipeline pipeline,
            Func<Func<CancellationToken, Task<object>>, CancellationToken, Task<object>> func)
        {
            return pipeline.Add(new DelegatedPipelineItem((next, httpRequest, resultType, cancellationToken) => func(innerCt => next(httpRequest, innerCt), cancellationToken)));
        }

        private class DelegatedPipelineItem : IPipelineItem
        {
            private readonly Func<Func<HttpRequest<object>, CancellationToken, Task<object>>, HttpRequest<object>, Type, CancellationToken, Task<object>> func;

            public DelegatedPipelineItem(Func<Func<HttpRequest<object>, CancellationToken, Task<object>>, HttpRequest<object>, Type, CancellationToken, Task<object>> func)
            {
                this.func = func;
            }

            public Func<IServiceProvider, IHttpClientWrapper, IHttpClientWrapper> Configure(IServiceCollection services, Func<IServiceProvider, IHttpClientWrapper> currentHttpClientWrapperFactory)
            {
                return (serviceProvider, httpClientWrapper) => new DelegatedHttpClientWrapper(httpClientWrapper, this.func);
            }
        }

        private class DelegatedHttpClientWrapper : IHttpClientWrapper
        {
            private readonly IHttpClientWrapper httpClientWrapper;

            private readonly Func<Func<HttpRequest<object>, CancellationToken, Task<object>>, HttpRequest<object>, Type, CancellationToken, Task<object>> func;

            public DelegatedHttpClientWrapper(IHttpClientWrapper httpClientWrapper, Func<Func<HttpRequest<object>, CancellationToken, Task<object>>, HttpRequest<object>, Type, CancellationToken, Task<object>> func)
            {
                this.httpClientWrapper = httpClientWrapper;
                this.func = func;
            }

            public async Task<TResult> SendAsync<TResult>(HttpRequest<TResult> httpRequest, CancellationToken cancellationToken)
            {
                return (TResult)(await this.func(
                    async (innerHttpRequest, innerCt) => 
                    {
                        var result = await httpClientWrapper.SendAsync(ConvertHttpRequest<object, TResult>(innerHttpRequest), innerCt).ConfigureAwait(false);

                        return result;
                    },
                    ConvertHttpRequest<TResult, object>(httpRequest),
                    typeof(TResult),
                    cancellationToken).ConfigureAwait(false));
            }

            private static HttpRequest<TDestination> ConvertHttpRequest<TSource, TDestination>(HttpRequest<TSource> httpRequest)
            {
                return new HttpRequest<TDestination>(
                    httpRequest.UriFromValuesBuilder,
                    httpRequest.HttpMethod,
                    httpRequest.RequestHeaders,
                    httpRequest.RequestContentProvider,
                    async (response, cancellationToken) =>
                    {
                        var result = await httpRequest.ResponseProcessor(response, cancellationToken).ConfigureAwait(false);
                        return (TDestination)(object)result;
                    });
            }
        }
    }
}