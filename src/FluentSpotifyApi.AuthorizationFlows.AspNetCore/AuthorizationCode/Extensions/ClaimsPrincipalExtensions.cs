using System.Security.Claims;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Extensions
{
    /// <summary>
    /// The set of <see cref="ClaimsPrincipal"/> extensions.
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Gets the name identifier.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <returns></returns>
        public static string GetNameIdentifier(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
