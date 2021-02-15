using System.Security.Claims;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Utils
{
    /// <summary>
    /// Helper utilities for validating and getting claims required by Spotify Authorization Code Flow.
    /// </summary>
    public static class SpotifyRequiredClaimsUtils
    {
        /// <summary>
        /// Searches and validates required claims.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <returns><c>true</c> if required claims are present and valid, <c>false</c> otherwise.</returns>
        public static bool Validate(ClaimsPrincipal principal)
        {
            return TryGet(principal, out _);
        }

        /// <summary>
        /// Gets required claims from claims principal.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <param name="userId">The user ID.</param>
        /// <returns><c>true</c> if required claims are present and valid, <c>false</c> otherwise.</returns>
        public static bool TryGet(ClaimsPrincipal principal, out string userId)
        {
            var result = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(result))
            {
                userId = null;
                return false;
            }

            userId = result;
            return true;
        }
    }
}
