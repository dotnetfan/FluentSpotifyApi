using System;
using FluentSpotifyApi.Core.Client;

namespace FluentSpotifyApi.AuthorizationFlows.ClientCredentials.Extensions
{
    /// <summary>
    /// The set of <see cref="IPipeline"/> extensions.
    /// </summary>
    public static class PipelineExtensions
    {
        /// <summary>
        /// Adds client credentials flow to <paramref name="pipeline" />.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="configureOptions">The options configuration action.</param>
        /// <returns></returns>
        public static IPipeline AddClientCredentialsFlow(this IPipeline pipeline, Action<ClientCredentialsFlowOptions> configureOptions = null) 
            => pipeline.Add(new ClientCredentialsFlowPipelineItem(configureOptions));
    }
}
