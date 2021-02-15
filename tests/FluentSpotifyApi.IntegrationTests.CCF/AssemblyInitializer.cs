using System;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.ClientCredentials;
using FluentSpotifyApi.AuthorizationFlows.ClientCredentials.DependencyInjection;
using FluentSpotifyApi.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Polly;
using Polly.Extensions.Http;

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
            var config = new ConfigurationBuilder()
                .AddJsonFile("secrets.json", optional: true, reloadOnChange: true)
                .Build();

            // Build retry policy
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(x => x.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: (retryCount, response, context) =>
                    {
                        var retryAfter = (response?.Result?.Headers?.RetryAfter?.Delta).GetValueOrDefault();
                        var min = TimeSpan.FromSeconds(1);
                        return retryAfter > min ? retryAfter : min;
                    },
                    onRetryAsync: (response, timespan, retryCount, context) => Task.CompletedTask);

            // Build services provider
            var services = new ServiceCollection();

            services
                .Configure<SpotifyClientCredentialsFlowOptions>(config.GetSection("SpotifyClientCredentialsFlow"));

            services
                .AddFluentSpotifyClient()
                .ConfigureHttpClientBuilder(b => b.AddPolicyHandler(retryPolicy))
                .ConfigureHttpClientBuilder(b => b
                    .AddSpotifyClientCredentialsFlow()
                    .ConfigureTokenHttpClientBuilder(b => b.AddPolicyHandler(retryPolicy)));

            serviceProvider = services.BuildServiceProvider();

            Client = serviceProvider.GetRequiredService<IFluentSpotifyClient>();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            ((IDisposable)serviceProvider)?.Dispose();
        }
    }
}
