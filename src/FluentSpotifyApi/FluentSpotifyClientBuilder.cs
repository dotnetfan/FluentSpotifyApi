using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FluentSpotifyApi.Client;
using FluentSpotifyApi.Core.Client;
using FluentSpotifyApi.Core.Internal.Extensions;
using FluentSpotifyApi.Core.Options;
using FluentSpotifyApi.Options;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi
{
    internal class FluentSpotifyClientBuilder : IFluentSpotifyClientBuilder
    {
        private Action<FluentSpotifyClientOptions> configureOptions;

        private Action<IPipeline> configurePipeline;

        private Action<HttpClient> configureHttpClient;

        private (Func<IServiceProvider, HttpClient> Factory, bool DisposeWithServiceProvider) httpClientFactoryWithOptions;

        public IFluentSpotifyClientBuilder ConfigureOptions(Action<FluentSpotifyClientOptions> configureOptions)
        {
            this.configureOptions = configureOptions;

            return this;
        }

        public IFluentSpotifyClientBuilder ConfigurePipeline(Action<IPipeline> configurePipeline)
        {
            this.configurePipeline = configurePipeline;

            return this;
        }

        public IFluentSpotifyClientBuilder ConfigureHttpClient(Action<HttpClient> configureHttpClient)
        {
            this.configureHttpClient = configureHttpClient;

            return this;
        }

        public IFluentSpotifyClientBuilder UseHttpClientFactory(Func<IServiceProvider, HttpClient> httpClientFactory, bool disposeWithServiceProvider)
        {
            this.httpClientFactoryWithOptions = (httpClientFactory, disposeWithServiceProvider);

            return this;
        }

        public void Configure(IServiceCollection services)
        {
            services.AddOptions();

            if (this.configureOptions != null)
            {
                services.Configure(this.configureOptions);
            }

            services.RegisterSingleton<IOptionsProvider<FluentSpotifyClientOptions>, OptionsProvider<FluentSpotifyClientOptions>>();

            services.RegisterSingleton<ISpotifyHttpClient, SpotifyHttpClient>();
            services.RegisterSingleton<IFluentSpotifyClient, FluentSpotifyClient>();

            services.RegisterSingleton(serviceProvider =>
            {
                HttpClient httpClient;
                bool disposeWithServiceProvider;
                if (this.httpClientFactoryWithOptions.Factory != null)
                {
                    httpClient = this.httpClientFactoryWithOptions.Factory(serviceProvider);

                    disposeWithServiceProvider = this.httpClientFactoryWithOptions.DisposeWithServiceProvider;
                }
                else
                {
                    httpClient = new HttpClient
                    {
                        Timeout = TimeSpan.FromMinutes(1)
                    };

                    disposeWithServiceProvider = true;
                }

                this.configureHttpClient?.Invoke(httpClient);

                return new HttpClientRegistrationWrapper(httpClient, disposeWithServiceProvider);
            });

            Func<IServiceProvider, IHttpClientWrapper> baseFactory = serviceProvider => new HttpClientWrapper(serviceProvider.GetRequiredService<HttpClientRegistrationWrapper>().Value);

            var pipeline = new Pipeline();
            this.configurePipeline?.Invoke(pipeline);

            var decoratorContainer = new DecoratorContainer(services, baseFactory);
            foreach (var item in pipeline.Items.Reverse())
            {
                decoratorContainer.AddDecoratorFactory(item.Configure(services, decoratorContainer.GetCurrentHttpClientWrapperFactory()));
            }
        }

        private class Pipeline : IPipeline
        {
            public Pipeline()
            {
                this.Items = new List<IPipelineItem>();
            }

            public IList<IPipelineItem> Items { get; }

            public IPipeline Add(IPipelineItem item)
            {
                this.Items.Add(item);

                return this;
            }
        }

        private class DecoratorContainer
        {
            private readonly IServiceCollection services;

            private readonly IList<Func<IServiceProvider, IHttpClientWrapper, IHttpClientWrapper>> httpClientWrapperFactories;

            public DecoratorContainer(
                IServiceCollection services,
                Func<IServiceProvider, IHttpClientWrapper> baseFactory)
            {
                this.services = services;

                this.httpClientWrapperFactories = new List<Func<IServiceProvider, IHttpClientWrapper, IHttpClientWrapper>>(new[]
                {
                    new Func<IServiceProvider, IHttpClientWrapper, IHttpClientWrapper>(
                        (serviceProvider, httpClientWrapper) =>
                            baseFactory(serviceProvider))
                });

                this.services.RegisterSingleton(serviceProvider => new DecoratorsWrapper(this.httpClientWrapperFactories));

                this.services.RegisterSingleton(serviceProvider => serviceProvider
                    .GetRequiredService<DecoratorsWrapper>()
                    .GetHttpClientWrapper(serviceProvider, null));
            }

            public Func<IServiceProvider, IHttpClientWrapper> GetCurrentHttpClientWrapperFactory()
            {
                var lastIndex = this.httpClientWrapperFactories.Count - 1;
                return serviceProvider => serviceProvider
                    .GetRequiredService<DecoratorsWrapper>()
                    .GetHttpClientWrapper(serviceProvider, lastIndex);
            }

            public void AddDecoratorFactory(Func<IServiceProvider, IHttpClientWrapper, IHttpClientWrapper> func)
            {
                this.httpClientWrapperFactories.Add(func);
            }

            private class DecoratorsWrapper : IDisposable
            {
                private readonly IList<Func<IServiceProvider, IHttpClientWrapper, IHttpClientWrapper>> httpClientWrapperFactories;

                private readonly IList<IHttpClientWrapper> httpClientWrappers;

                private readonly object locker;

                public DecoratorsWrapper(IList<Func<IServiceProvider, IHttpClientWrapper, IHttpClientWrapper>> httpClientWrapperFactories)
                {
                    this.httpClientWrapperFactories = httpClientWrapperFactories;

                    this.httpClientWrappers = new List<IHttpClientWrapper>();
                    this.locker = new object();
                }

                public IHttpClientWrapper GetHttpClientWrapper(IServiceProvider serviceProvider, int? index)
                {
                    lock (this.locker)
                    {
                        index = index ?? this.httpClientWrapperFactories.Count - 1;

                        if (this.httpClientWrappers.Count - 1 >= index)
                        {
                            return this.httpClientWrappers[index.Value];
                        }
                        else
                        {
                            var httpClientWrapper = this.httpClientWrappers.LastOrDefault();
                            for (var i = this.httpClientWrappers.Count; i <= index; i++)
                            {
                                httpClientWrapper = this.httpClientWrapperFactories[i](serviceProvider, httpClientWrapper);
                                this.httpClientWrappers.Add(httpClientWrapper);
                            }

                            return httpClientWrapper;
                        }
                    }
                }

                public void Dispose()
                {
                    foreach (var item in this.httpClientWrappers.OfType<IDisposable>())
                    {
                        item.Dispose();
                    }
                }
            }
        }
    }
}