using System;
using FluentSpotifyApi.AuthorizationFlows.Core;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.User;
using FluentSpotifyApi.AuthorizationFlows.Core.Date;
using FluentSpotifyApi.AuthorizationFlows.Core.Pipeline;
using FluentSpotifyApi.Core.Internal.Extensions;
using FluentSpotifyApi.Core.Options;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native
{
    /// <summary>
    /// The base class for authorization code flow pipeline item for native apps.
    /// </summary>
    /// <typeparam name="TOptions">The type of the options.</typeparam>
    /// <seealso cref="FluentSpotifyApi.AuthorizationFlows.Core.Pipeline.AuthorizationPipelineItemWithOptionsBase{TOptions}" />
    public class AuthorizationCodeFlowPipelineItemBase<TOptions> : AuthorizationPipelineItemWithOptionsBase<TOptions> 
        where TOptions : class, IAuthorizationOptions, IUserClientOptions, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationCodeFlowPipelineItemBase{TOptions}"/> class.
        /// </summary>
        /// <param name="configureOptions">The options configuration action.</param>
        public AuthorizationCodeFlowPipelineItemBase(Action<TOptions> configureOptions) : base(configureOptions)
        {
        }

        /// <summary>
        /// Configures the specified services.
        /// </summary>
        /// <param name="services">The services.</param>
        protected override void Configure(IServiceCollection services)
        {
            base.Configure(services);

            services.RegisterSingleton<IDateTimeOffsetProvider, DateTimeOffsetProvider>();

            services.RegisterSingleton<IOptionsProvider<IAuthorizationOptions>, OptionsProvider<TOptions>>();
            services.RegisterSingleton<IOptionsProvider<IUserClientOptions>, OptionsProvider<TOptions>>();

            services.RegisterSingleton<IUserHttpClient, UserHttpClient>();

            services.RegisterSingleton<IAuthorizationProvider<string>>(serviceProvider =>
                new AuthorizationProvider<string>(
                    serviceProvider.GetRequiredService<IAuthorizationInteractionClient<string>>(),
                    serviceProvider.GetRequiredService<ICsrfTokenProvider>(),
                    serviceProvider.GetRequiredService<IAuthorizationCallbackUriProvider>(),
                    serviceProvider.GetRequiredService<IOptionsProvider<IAuthorizationOptions>>(),
                    "code"));

            services.RegisterSingleton<IAuthenticationTicketStorage, AuthenticationTicketStorage>();

            services.RegisterSingleton<AuthenticationTicketProvider>();

            services.RegisterSingleton<IAuthenticationManager>(serviceProvider =>
                serviceProvider.GetRequiredService<AuthenticationTicketProvider>());

            services.RegisterSingleton<IAuthenticationTicketProvider>(serviceProvider =>
                serviceProvider.GetRequiredService<AuthenticationTicketProvider>());
        }
    }
}
