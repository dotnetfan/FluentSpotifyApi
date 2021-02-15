using System;
using System.Globalization;
using Microsoft.AspNetCore.Authentication;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Utils
{
    /// <summary>
    /// Helper utilities for validating and getting tokens required by Spotify Authorization Code Flow.
    /// </summary>
    public static class SpotifyRequiredTokensUtils
    {
        /// <summary>
        /// Searches and validates required tokens.
        /// </summary>
        /// <param name="properties">The authentication properties.</param>
        /// <returns><c>true</c> if required tokens are present and valid, <c>false</c> otherwise.</returns>
        public static bool Validate(AuthenticationProperties properties)
        {
            return TryGet(properties, out _);
        }

        /// <summary>
        /// Gets required tokens from authentication properties.
        /// </summary>
        /// <param name="properties">The authentication properties.</param>
        /// <param name="tokens">The tokens.</param>
        /// <returns><c>true</c> if required tokens are present and valid, <c>false</c> otherwise.</returns>
        public static bool TryGet(AuthenticationProperties properties, out (string RefreshToken, string AccessToken, DateTimeOffset ExpiresAt)? tokens)
        {
            var refreshToken = properties.GetTokenValue(TokenNames.RefreshToken);
            var accessToken = properties.GetTokenValue(TokenNames.AccessToken);
            var expiresAt = properties.GetTokenValue(TokenNames.ExpiresAt);

            if (
                string.IsNullOrEmpty(refreshToken) ||
                string.IsNullOrEmpty(accessToken) ||
                !DateTimeOffset.TryParseExact(expiresAt, "o", CultureInfo.InvariantCulture, DateTimeStyles.None, out var expiresAtParsed))
            {
                tokens = null;
                return false;
            }

            tokens = (refreshToken, accessToken, expiresAtParsed);
            return true;
        }
    }
}
