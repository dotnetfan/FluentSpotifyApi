using System;
using System.Net.Http;
using FluentSpotifyApi.AuthorizationFlows.ClientCredentials;
using FluentSpotifyApi.AuthorizationFlows.ClientCredentials.Extensions;
using FluentSpotifyApi.Core.Exceptions;
using FluentSpotifyApi.Core.Extensions;
using FluentSpotifyApi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Polly;

namespace FluentSpotifyApi.IntegrationTests.CCF
{
    [TestClass]
    public sealed class AssemblyInitializer
    {
        private static IServiceProvider serviceProvider;

        public static IFluentSpotifyClient Client { get; private set; }

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            // Build configuration
            var builder = new ConfigurationBuilder()
                .AddJsonFile("secrets.json", optional: true, reloadOnChange: true);

            var config = builder.Build();

            // Setup policies
            var serviceUnavailablePolicy = Policy
                .Handle<SpotifyHttpResponseWithErrorCodeException>(x => x.IsRecoverable())
                .Or<SpotifyHttpRequestException>(x => x.InnerException is HttpRequestException)
                .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(5));

            var tooManyRequestsPolicy = Policy.Handle<SpotifyHttpResponseWithErrorCodeException>(e => (int)e.ErrorCode == 429 && e.Headers.RetryAfter.GetValueOrDefault() > TimeSpan.Zero)
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: (retryAttempt, retryContext) =>
                    {
                        if (retryContext == null || !retryContext.TryGetValue("RetryAfter", out object retryAfter))
                        {
                            retryAfter = TimeSpan.FromSeconds(1);
                        }

                        return (TimeSpan)retryAfter;
                    },
                    onRetry: (exception, timespan, retryAttempt, retryContext) =>
                    {
                        retryContext["RetryAfter"] = (exception as SpotifyHttpResponseWithErrorCodeException).Headers.RetryAfter;
                    });

            var wrapPolicy = Policy.WrapAsync(serviceUnavailablePolicy, tooManyRequestsPolicy);

            // Build services provider
            var services = new ServiceCollection();

            services.Configure<ClientCredentialsFlowOptions>(config.GetSection("ClientCredentialsFlowOptions"));

            services
                .AddFluentSpotifyClient(clientBuilder => clientBuilder
                    .ConfigurePipeline(pipeline => pipeline
                        .AddDelegate((next, cancellationToken) => wrapPolicy.ExecuteAsync(next, cancellationToken))
                        .AddClientCredentialsFlow()));

            serviceProvider = services.BuildServiceProvider();

            Client = serviceProvider.GetRequiredService<IFluentSpotifyClient>();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            if (serviceProvider != null)
            {
                ((IDisposable)serviceProvider).Dispose();
            }
        }
    }
}
