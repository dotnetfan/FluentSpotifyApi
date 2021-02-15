using System;
using FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Handler;
using FluentSpotifyApi.Core.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode
{
    internal class AuthorizationCodeFlowOptionsProvider : IOptionsProvider<AuthorizationCodeFlowOptions>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string spotifyAuthenticationScheme;

        public AuthorizationCodeFlowOptionsProvider(IHttpContextAccessor httpContextAccessor, string spotifyAuthenticationScheme)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.spotifyAuthenticationScheme = spotifyAuthenticationScheme;
        }

        public AuthorizationCodeFlowOptions Get()
        {
            var optionsSnapshot = this.httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IOptionsSnapshot<SpotifyOptions>>();

            var spotifyOptions = string.IsNullOrEmpty(this.spotifyAuthenticationScheme) ? optionsSnapshot.Value : optionsSnapshot.Get(this.spotifyAuthenticationScheme);
            var result = new AuthorizationCodeFlowOptions
            {
                ClientId = spotifyOptions.ClientId,
                ClientSecret = spotifyOptions.ClientSecret,
                TokenEndpoint = new Uri(spotifyOptions.TokenEndpoint)
            };

            return result;
        }
    }
}
