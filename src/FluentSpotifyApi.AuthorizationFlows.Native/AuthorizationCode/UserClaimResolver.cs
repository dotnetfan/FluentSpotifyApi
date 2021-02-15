using System;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode
{
    /// <summary>
    /// The user claim resolver.
    /// </summary>
    public class UserClaimResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimResolver"/> class.
        /// </summary>
        /// <param name="claimType">The claim type.</param>
        /// <param name="resolver">The function used to resolve claim value from <see cref="PrivateUser"/>.</param>
        public UserClaimResolver(string claimType, Func<PrivateUser, string> resolver)
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(claimType, nameof(claimType));
            SpotifyArgumentAssertUtils.ThrowIfNull(resolver, nameof(resolver));

            this.ClaimType = claimType;
            this.Resolver = resolver;
        }

        /// <summary>
        /// Gets the claim type.
        /// </summary>
        public string ClaimType { get; }

        /// <summary>
        /// Gets the function used to resolve claim value from <see cref="PrivateUser"/>.
        /// </summary>
        public Func<PrivateUser, string> Resolver { get; }
    }
}
