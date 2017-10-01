using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native;
using FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Server.Extensions;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.AuthorizationFlows.UWP.AuthorizationCode.Extensions;
using FluentSpotifyApi.Core.Exceptions;
using FluentSpotifyApi.Core.Extensions;
using FluentSpotifyApi.Core.Options;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Sample.ACF.UWP.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Polly;

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

            // Configure retrying
            var serviceUnavailablePolicy = Policy
                .Handle<SpotifyHttpResponseWithErrorCodeException>(x => x.IsRecoverable())
                .Or<SpotifyHttpRequestException>(x => x.InnerException is HttpRequestException)
                .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(5));

            // Create service collection
            var services = new ServiceCollection();

            // Register naive token proxy client
            services.Configure<TokenClientOptions>(o =>
            {
                o.ClientId = secrets.GetString("ClientId");
                o.ClientSecret = secrets.GetString("ClientSecret");
            });

            services.AddSingleton<IOptionsProvider<ITokenClientOptions>, OptionsProvider<TokenClientOptions>>();
            services.AddSingleton<ITokenHttpClient, TokenHttpClient>();
            services.AddSingleton<ITokenProxyClient, NaiveTokenProxyClient>();

            // Register authorization code flow
            services
                .AddFluentSpotifyClient(clientBuilder => clientBuilder
                    .ConfigurePipeline(pipeline => pipeline
                        .AddDelegate((next, cancellationToken) => serviceUnavailablePolicy.ExecuteAsync(next, cancellationToken))
                        .AddUwpAuthorizationCodeFlow(
                            o =>
                            {
                                o.ClientId = secrets.GetString("ClientId");
                                o.Scopes = new[] { Scope.PlaylistReadPrivate, Scope.PlaylistReadCollaborative };
                                o.ShowDialog = true;
                            })));

            // Create Autofac container builder
            var builder = new ContainerBuilder();

            // Populate Autofac container with services from service collection
            builder.Populate(services);

            // Register View Models
            builder.RegisterType<MainViewModel>().SingleInstance();

            // build container
            Container = builder.Build();
        }

        public MainViewModel Main
        {
            get
            {
                return Container.Resolve<MainViewModel>();
            }
        }

        /// <summary>
        /// This is a naive implementation of <see cref="ITokenProxyClient"/> and shouldn't be used for publicly available apps.
        /// It uses Spotify Accounts Service directly and thus requires Client Secret to be embedded in the application package.
        /// </summary>
        /// <seealso cref="FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native.ITokenProxyClient" />
        private class NaiveTokenProxyClient : ITokenProxyClient
        {
            private readonly ITokenHttpClient tokenHttpClient;

            private readonly IAuthorizationCallbackUriProvider authorizationCallbackUriProvider;

            public NaiveTokenProxyClient(ITokenHttpClient tokenHttpClient, IAuthorizationCallbackUriProvider authorizationCallbackUriProvider)
            {
                this.tokenHttpClient = tokenHttpClient;
                this.authorizationCallbackUriProvider = authorizationCallbackUriProvider;
            }

            public async Task<ProxyAuthorizationTokens> GetAuthorizationTokensAsync(string authorizationCode, CancellationToken cancellationToken)
            {
                var result = await this.tokenHttpClient.GetAuthorizationTokensAsync(authorizationCode, this.authorizationCallbackUriProvider.Get(), cancellationToken).ConfigureAwait(false);

                return new ProxyAuthorizationTokens
                {
                    AuthorizationKey = result.RefresToken,
                    AccessToken = new ProxyAccessToken
                    {
                        Token = result.AccessToken,
                        ExpiresIn = result.ExpiresIn
                    },
                };
            }

            public async Task<ProxyAccessToken> GetAccessTokenAsync(string authorizationKey, CancellationToken cancellationToken)
            {
                var result = await this.tokenHttpClient.GetAccessTokenOrThrowInvalidRefreshTokenExceptionAsync(authorizationKey, cancellationToken).ConfigureAwait(false);

                return new ProxyAccessToken
                {
                    Token = result.Token,
                    ExpiresIn = result.ExpiresIn
                };
            }
        }

        private class TokenClientOptions : ITokenClientOptions
        {
            public TokenClientOptions()
            {
                this.TokenEndpoint = AuthorizationFlows.Core.Client.Token.Defaults.TokenEndpoint;
            }

            public string ClientId { get; set; }

            public string ClientSecret { get; set; }

            public Uri TokenEndpoint { get; set; }
        }
    }
}
