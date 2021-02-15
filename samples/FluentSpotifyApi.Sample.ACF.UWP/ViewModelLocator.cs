using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using FluentSpotifyApi.AuthorizationFlows.UWP.AuthorizationCode.DependencyInjection;
using FluentSpotifyApi.DependencyInjection;
using FluentSpotifyApi.Sample.ACF.UWP.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace FluentSpotifyApi.Sample.ACF.UWP
{
    public class ViewModelLocator
    {
        private static readonly IContainer Container;

        static ViewModelLocator()
        {
            // Uncomment the following line to get the callback URL
            ////var callbackUrl = Windows.Security.Authentication.Web.WebAuthenticationBroker.GetCurrentApplicationCallbackUri();

            // Load secrets from assets
            var secrets = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("secrets");

            // Build retry policy
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(x => (int)x.StatusCode == 429)
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: (retryCount, response, context) =>
                    {
                        var retryAfter = (response?.Result?.Headers?.RetryAfter?.Delta).GetValueOrDefault();
                        var min = TimeSpan.FromSeconds(1);
                        return retryAfter > min ? retryAfter : min;
                    },
                    onRetryAsync: (response, timespan, retryCount, context) => Task.CompletedTask);

            // Create service collection
            var services = new ServiceCollection();

            // Register fluent spotify API client with authorization code flow
            services.AddFluentSpotifyClient()
                .ConfigureHttpClientBuilder(b => b.AddPolicyHandler(retryPolicy))
                .ConfigureHttpClientBuilder(b => b.AddSpotifyAuthorizationCodeFlow(
                    o =>
                    {
                        o.ClientId = secrets.GetString("ClientId");

                        // Uncomment to add custom profile picture claim
                        ////o.UserClaimResolvers.AddOrUpdate("ProfilePicture", u => u.Images?.FirstOrDefault()?.Url);

                        o.Scopes.Add(SpotifyScopes.PlaylistReadPrivate);
                        o.Scopes.Add(SpotifyScopes.PlaylistReadCollaborative);
                    })
                    .ConfigureTokenHttpClientBuilder(t => t.AddPolicyHandler(retryPolicy))
                    .ConfigureUserHttpClientBuilder(u => u.AddPolicyHandler(retryPolicy)));

            // Create Autofac container builder
            var builder = new ContainerBuilder();

            // Populate Autofac container with services from service collection
            builder.Populate(services);

            // Register View Models
            builder.RegisterType<MainViewModel>().SingleInstance();

            // build container
            Container = builder.Build();
        }

        public MainViewModel Main => Container.Resolve<MainViewModel>();
    }
}
