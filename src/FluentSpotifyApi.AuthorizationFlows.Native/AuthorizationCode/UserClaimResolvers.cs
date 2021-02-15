using System;
using System.Collections;
using System.Collections.Generic;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode
{
    /// <summary>
    /// The collection of <see cref="UserClaimResolver"/>.
    /// </summary>
    public class UserClaimResolvers : IEnumerable<UserClaimResolver>
    {
        private IDictionary<string, UserClaimResolver> Resolvers { get; } = new Dictionary<string, UserClaimResolver>();

        /// <summary>
        /// Removes all claim resolvers.
        /// </summary>
        public void Clear() => this.Resolvers.Clear();

        /// <summary>
        /// Removes claim resolver for the given <paramref name="claimType"/>.
        /// </summary>
        /// <param name="claimType">The claim type.</param>
        public void Remove(string claimType)
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(claimType, nameof(claimType));

            this.Resolvers.Remove(claimType);
        }

        /// <summary>
        /// Adds new claim resolver or updates existing for the given <paramref name="claimType"/>.
        /// </summary>
        /// <param name="claimType">The claim type.</param>
        /// <param name="resolver">The resolver.</param>
        public void AddOrUpdate(string claimType, Func<PrivateUser, string> resolver) => this.AddOrUpdate(new UserClaimResolver(claimType, resolver));

        /// <summary>
        /// Adds new claim resolver or updates existing for the given <paramref name="resolver"/> ClaimType.
        /// </summary>
        /// <param name="resolver">The user claim resolver.</param>
        public void AddOrUpdate(UserClaimResolver resolver) => this.Resolvers[resolver.ClaimType] = resolver;

        /// <summary>
        /// Returns an enumerator that iterates through claim resolvers.
        /// </summary>
        public IEnumerator<UserClaimResolver> GetEnumerator() => this.Resolvers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.Resolvers.GetEnumerator();
    }
}
