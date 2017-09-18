using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Handler
{
    /// <summary>
    /// The set of <see cref="AuthenticationBuilder"/> extensions
    /// </summary>
    public static class SpotifyExtensions
    {
        /// <summary>
        /// Adds the Spotify authentication to <see cref="AuthenticationBuilder"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddSpotify(this AuthenticationBuilder builder)
            => builder.AddSpotify(
                SpotifyDefaults.AuthenticationScheme, 
                o => 
                {
                });

        /// <summary>
        /// Adds the Spotify authentication to <see cref="AuthenticationBuilder"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="configureOptions">The configure options.</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddSpotify(this AuthenticationBuilder builder, Action<SpotifyOptions> configureOptions)
            => builder.AddSpotify(SpotifyDefaults.AuthenticationScheme, configureOptions);

        /// <summary>
        /// Adds the Spotify authentication to <see cref="AuthenticationBuilder"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="configureOptions">The configure options.</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddSpotify(this AuthenticationBuilder builder, string authenticationScheme, Action<SpotifyOptions> configureOptions)
            => builder.AddSpotify(authenticationScheme, SpotifyDefaults.DisplayName, configureOptions);

        /// <summary>
        /// Adds the Spotify authentication to <see cref="AuthenticationBuilder"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="configureOptions">The configure options.</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddSpotify(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<SpotifyOptions> configureOptions)
            => builder.AddOAuth<SpotifyOptions, SpotifyHandler>(authenticationScheme, displayName, configureOptions);
    }
}
