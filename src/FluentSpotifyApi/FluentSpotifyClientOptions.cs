using System;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi
{
    /// <summary>
    /// The options for <see cref="IFluentSpotifyClient"/>.
    /// </summary>
    public sealed class FluentSpotifyClientOptions
    {
        /// <summary>
        /// Gets or sets the URI of Spotify Web API. Defaults to https://api.spotify.com/v1/.
        /// </summary>
        public Uri WebApiEndpoint { get; set; } = FluentSpotifyClientDefaults.WebApiEndpoint;

        /// <summary>
        /// Performs validation.
        /// </summary>
        public void Validate()
        {
            SpotifyArgumentAssertUtils.ThrowIfNull(this.WebApiEndpoint, nameof(this.WebApiEndpoint));
        }
    }
}
