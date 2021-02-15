using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode
{
    /// <summary>
    /// The collection of <see cref="UserClaim"/>.
    /// </summary>
    public class UserClaims : IReadOnlyCollection<UserClaim>
    {
        private IDictionary<string, UserClaim> claims;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaims"/> class.
        /// </summary>
        /// <param name="sequence">The sequence of claims.</param>
        public UserClaims(IEnumerable<UserClaim> sequence)
        {
            this.claims = new Dictionary<string, UserClaim>();

            foreach (var item in sequence ?? Enumerable.Empty<UserClaim>())
            {
                this.claims[item.Type] = item;
            }
        }

        /// <summary>
        /// Gets the number of claims.
        /// </summary>
        public int Count => this.claims.Count;

        /// <summary>
        /// Gets claim for give <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The claim type.</param>
        /// <returns></returns>
        public UserClaim this[string type] => this.claims[type];

        /// <summary>
        /// Checks whether claim of given <paramref name="type"/> exists.
        /// </summary>
        /// <param name="type">The claim type.</param>
        /// <returns></returns>
        public bool HasClaim(string type) => this.claims.ContainsKey(type);

        /// <summary>
        /// Gets claim for the given <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The claim type.</param>
        /// <param name="claim">Found claim.</param>
        /// <returns><c>true</c> if user claim was found, <c>false</c> otherwise.</returns>
        public bool TryGetClaim(string type, out UserClaim claim) => this.claims.TryGetValue(type, out claim);

        /// <summary>
        /// Gets claim for the given <paramref name="type"/> or <c>null</c> if claim was not found.
        /// </summary>
        /// <param name="type">The claim type.</param>
        public UserClaim GetClaimOrDefault(string type) => this.TryGetClaim(type, out var claim) ? claim : null;

        /// <summary>
        /// Returns an enumerator that iterates through claims.
        /// </summary>
        public IEnumerator<UserClaim> GetEnumerator() => this.claims.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.claims.Values.GetEnumerator();
    }
}
