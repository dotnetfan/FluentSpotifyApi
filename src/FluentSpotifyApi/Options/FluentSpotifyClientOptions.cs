using System;
using FluentSpotifyApi.Core.Options;

namespace FluentSpotifyApi.Options
{
    /// <summary>
    /// The client options.
    /// </summary>
    public sealed class FluentSpotifyClientOptions : IValidatable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FluentSpotifyClientOptions"/> class.
        /// </summary>
        public FluentSpotifyClientOptions()
        {
            this.WebApiEndpoint = new Uri("https://api.spotify.com/v1/");
        }

        /// <summary>
        /// Gets or sets the Web API endpoint. Set to https://api.spotify.com/v1/ by default.
        /// </summary>
        public Uri WebApiEndpoint { get; set; }

        /// <summary>
        /// Performs validation.
        /// </summary>
        public void Validate()
        {
            if (this.WebApiEndpoint == null)
            {
                throw new ArgumentNullException(nameof(this.WebApiEndpoint));
            }
        }
    }
}
