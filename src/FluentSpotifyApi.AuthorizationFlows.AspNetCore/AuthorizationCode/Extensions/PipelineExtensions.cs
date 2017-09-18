using FluentSpotifyApi.Core.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Extensions
{
    /// <summary>
    /// The set of <see cref="IPipeline"/> extensions.
    /// </summary>
    public static class PipelineExtensions
    {
        /// <summary>
        /// Adds ASP.NET Core authorization code flow to <paramref name="pipeline" />.
        /// Depends on <see cref="ISystemClock" />, <see cref="IHttpContextAccessor" /> and <see cref="IOptionsMonitor{SpotifyOptions}"/>> that are registered by ASP.NET Core middleware.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="spotifyAuthenticationScheme">The Spotify authentication scheme.</param>
        /// <returns></returns>
        public static IPipeline AddAspNetCoreAuthorizationCodeFlow(
            this IPipeline pipeline,            
            string authenticationScheme = null,
            string spotifyAuthenticationScheme = null)
                => pipeline.Add(new AspNetCoreAuthorizationCodeFlowPipelineItem(authenticationScheme, spotifyAuthenticationScheme));
    }
}
