using FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Extensions
{
    /// <summary>
    /// The set of <see cref="IApplicationBuilder"/> extensions.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Handles the spotify invalid refresh token exception.
        /// </summary>
        /// <param name="applicationBuilder">The application builder.</param>
        /// <param name="loginPath">The login path.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSpotifyInvalidRefreshTokenExceptionHandler(this IApplicationBuilder applicationBuilder, string loginPath, string authenticationScheme = null)
        {
            return applicationBuilder.Use(async (context, next) =>
            {
                try
                {
                    await next.Invoke();
                }
                catch (SpotifyInvalidRefreshTokenException)
                {
                    await context.SignOutAsync(authenticationScheme).ConfigureAwait(false);
                    context.Response.Redirect(loginPath);
                }
            });
        }
    }
}
