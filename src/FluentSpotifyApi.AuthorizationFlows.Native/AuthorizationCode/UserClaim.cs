using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode
{
    /// <summary>
    /// The user claim.
    /// </summary>
    public class UserClaim
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaim"/> class.
        /// </summary>
        /// <param name="type">The claim type.</param>
        /// <param name="value">The claim value.</param>
        public UserClaim(string type, string value)
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(type, nameof(type));
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(value, nameof(value));

            this.Type = type;
            this.Value = value;
        }

        /// <summary>
        /// Gets the claim type.
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Gets the claim value.
        /// </summary>
        public string Value { get; }
    }
}
