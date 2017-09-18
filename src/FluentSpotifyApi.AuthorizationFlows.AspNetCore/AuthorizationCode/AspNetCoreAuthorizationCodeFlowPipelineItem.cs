using System;
using FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Handler;
using FluentSpotifyApi.AuthorizationFlows.Core;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.AuthorizationFlows.Core.Date;
using FluentSpotifyApi.AuthorizationFlows.Core.Pipeline;
using FluentSpotifyApi.Core.Internal.Extensions;
using FluentSpotifyApi.Core.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode
{
    internal class AspNetCoreAuthorizationCodeFlowPipelineItem : AuthorizationPipelineItemBase
    {
        private readonly string authenticationScheme;

        private readonly string spotifyAuthenticationScheme;

        public AspNetCoreAuthorizationCodeFlowPipelineItem(string authenticationScheme, string spotifyAuthenticationScheme)
        {
            this.authenticationScheme = authenticationScheme;
            this.spotifyAuthenticationScheme = spotifyAuthenticationScheme;
        }

        protected override void Configure(IServiceCollection services)
        {
            services.RegisterSingleton<IDateTimeOffsetProvider, SystemClockDateTimeOffsetProvider>();

            services.RegisterSingleton<IOptionsProvider<AspNetCoreAuthorizationCodeFlowOptions>>(serviceProvider => new OptionsProvider(
                serviceProvider.GetRequiredService<IOptionsMonitor<SpotifyOptions>>(),
                this.spotifyAuthenticationScheme));

            services.RegisterSingleton<IOptionsProvider<ITokenClientOptions>>(serviceProvider => 
                serviceProvider.GetRequiredService<IOptionsProvider<AspNetCoreAuthorizationCodeFlowOptions>>());

            services.RegisterSingleton<ITokenHttpClient, TokenHttpClient>();

            services.RegisterSingleton<IAuthenticationManager>(serviceProvider => new AuthenticationManager(
                serviceProvider.GetRequiredService<IHttpContextAccessor>(),
                this.authenticationScheme));

            services.RegisterSingleton<IAuthenticationTicketProvider, AuthenticationTicketProvider>();
        }

        private class OptionsProvider : IOptionsProvider<AspNetCoreAuthorizationCodeFlowOptions>
        {
            private readonly IOptionsMonitor<SpotifyOptions> optionsMonitor;

            private readonly string spotifyAuthenticationScheme;

            public OptionsProvider(IOptionsMonitor<SpotifyOptions> optionsMonitor, string spotifyAuthenticationScheme)
            {
                this.optionsMonitor = optionsMonitor;
                this.spotifyAuthenticationScheme = spotifyAuthenticationScheme;
            }

            public AspNetCoreAuthorizationCodeFlowOptions Get()
            {
                var spotifyOptions = string.IsNullOrEmpty(this.spotifyAuthenticationScheme) ? this.optionsMonitor.CurrentValue : this.optionsMonitor.Get(this.spotifyAuthenticationScheme);
                var options = new AspNetCoreAuthorizationCodeFlowOptions
                {
                    ClientId = spotifyOptions.ClientId,
                    ClientSecret = spotifyOptions.ClientSecret,
                    TokenEndpoint = new Uri(spotifyOptions.TokenEndpoint)
                };

                options.Validate();

                return options;
            }
        }

        private class SystemClockDateTimeOffsetProvider : IDateTimeOffsetProvider
        {
            private readonly ISystemClock systemClock;

            public SystemClockDateTimeOffsetProvider(ISystemClock systemClock)
            {
                this.systemClock = systemClock;
            }

            public DateTimeOffset GetUtcNow()
            {
                return this.systemClock.UtcNow;
            }
        }
    }
}
