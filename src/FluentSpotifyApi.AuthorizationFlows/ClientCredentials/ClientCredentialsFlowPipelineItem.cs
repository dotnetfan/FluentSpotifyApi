using System;
using FluentSpotifyApi.AuthorizationFlows.Core;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.AuthorizationFlows.Core.Date;
using FluentSpotifyApi.AuthorizationFlows.Core.Pipeline;
using FluentSpotifyApi.Core.Internal.Extensions;
using FluentSpotifyApi.Core.Options;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.AuthorizationFlows.ClientCredentials
{
    internal class ClientCredentialsFlowPipelineItem : AuthorizationPipelineItemWithOptionsBase<ClientCredentialsFlowOptions>
    {
        public ClientCredentialsFlowPipelineItem(Action<ClientCredentialsFlowOptions> configureOptions) : base(configureOptions)
        {
        }

        protected override void Configure(IServiceCollection services)
        {
            base.Configure(services);

            services.RegisterSingleton<IDateTimeOffsetProvider, DateTimeOffsetProvider>();
            services.RegisterSingleton<IOptionsProvider<ITokenClientOptions>, OptionsProvider<ClientCredentialsFlowOptions>>();
            services.RegisterSingleton<ITokenHttpClient, TokenHttpClient>();
            services.RegisterSingleton<IAuthenticationTicketProvider, AuthenticationTicketProvider>();
        }
    }
}
