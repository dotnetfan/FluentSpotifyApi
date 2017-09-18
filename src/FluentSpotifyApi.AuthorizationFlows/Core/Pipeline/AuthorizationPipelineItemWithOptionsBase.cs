using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Pipeline
{
    /// <summary>
    /// The base class for authorization pipeline item with options.
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    public abstract class AuthorizationPipelineItemWithOptionsBase<TOptions> : AuthorizationPipelineItemBase where TOptions : class
    {
        private readonly Action<TOptions> configureOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationPipelineItemWithOptionsBase{TOptions}" /> class.
        /// </summary>
        /// <param name="configureOptions">The options configuration action.</param>
        public AuthorizationPipelineItemWithOptionsBase(Action<TOptions> configureOptions)
        {
            this.configureOptions = configureOptions;
        }

        /// <summary>
        /// Configures the specified services.
        /// </summary>
        /// <param name="services">The services.</param>
        protected override void Configure(IServiceCollection services)
        {
            services.AddOptions();

            if (this.configureOptions != null)
            {
                services.Configure(this.configureOptions);
            }
        }
    }
}
