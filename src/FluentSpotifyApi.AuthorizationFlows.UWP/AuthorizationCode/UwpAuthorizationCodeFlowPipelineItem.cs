using System;
using FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using FluentSpotifyApi.AuthorizationFlows.UWP.Core.Client.Authorization;
using FluentSpotifyApi.Core.Internal.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.AuthorizationFlows.UWP.AuthorizationCode
{
    internal class UwpAuthorizationCodeFlowPipelineItem : AuthorizationCodeFlowPipelineItemBase<UwpAuthorizationCodeFlowOptions>
    {
        public UwpAuthorizationCodeFlowPipelineItem(Action<UwpAuthorizationCodeFlowOptions> configureOptions) : base(configureOptions)
        {
        }

        protected override void Configure(IServiceCollection services)
        {
            base.Configure(services);

            services.RegisterSingleton<IAuthorizationCallbackUriProvider, AuthorizationCallbackUriProvider>();
            services.RegisterSingleton<ICsrfTokenProvider, CsrfTokenProvider>();
            services.RegisterSingleton<IAuthenticationBroker, AuthenticationBroker>();
            services.RegisterSingleton<IAuthorizationInteractionClient<string>, AuthorizationCodeAuthorizationInteractionClient>();
            services.RegisterSingleton<ISecureStorage, SecureStorage>();
        }
    }
}
