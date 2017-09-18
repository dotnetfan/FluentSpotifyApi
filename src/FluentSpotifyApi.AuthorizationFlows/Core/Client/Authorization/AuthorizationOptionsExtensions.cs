using System.Collections.Generic;
using System.Linq;
using FluentSpotifyApi.Core.Internal.Extensions;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    /// <summary>
    /// The set of <see cref="IAuthorizationOptions"/> extensions.
    /// </summary>
    public static class AuthorizationOptionsExtensions
    {
        /// <summary>
        /// Gets the scopes list.
        /// </summary>
        /// <param name="authorizationOptions">The authorization options.</param>
        /// <returns></returns>
        public static IList<string> GetScopesList(this IAuthorizationOptions authorizationOptions)
        {
            return authorizationOptions.Scopes.EmptyIfNull().Select(item => item.GetDescription()).Concat(authorizationOptions.DynamicScopes.EmptyIfNull()).Distinct().ToList();
        }
    }
}
