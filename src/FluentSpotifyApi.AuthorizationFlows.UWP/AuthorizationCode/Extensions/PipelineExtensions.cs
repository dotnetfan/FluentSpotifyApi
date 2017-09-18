using System;
using FluentSpotifyApi.Core.Client;

namespace FluentSpotifyApi.AuthorizationFlows.UWP.AuthorizationCode.Extensions
{
    /// <summary>
    /// The set of <see cref="IPipeline" /> extensions.
    /// </summary>
    public static class PipelineExtensions
    {
        /// <summary>
        /// Adds UWP authorization code flow to <paramref name="pipeline" />.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="configureOptions">The configure options action.</param>
        /// <returns></returns>
        public static IPipeline AddUwpAuthorizationCodeFlow(this IPipeline pipeline, Action<UwpAuthorizationCodeFlowOptions> configureOptions)
            => pipeline.Add(new UwpAuthorizationCodeFlowPipelineItem(configureOptions));
    }
}
