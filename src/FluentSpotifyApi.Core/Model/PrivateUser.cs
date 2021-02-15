using System.Text.Json.Serialization;

namespace FluentSpotifyApi.Core.Model
{
    /// <summary>
    /// The private user.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Core.Model.UserBase" />
    public class PrivateUser : UserBase
    {
        /// <summary>
        /// The country of the user, as set in the user’s account profile. An ISO 3166-1 alpha-2 country code.
        /// This field is only available when the current user has granted access to the <c>user-read-private</c> scope.
        /// </summary>
        [JsonPropertyName("country")]
        public string Country { get; set; }

        /// <summary>
        /// The user’s email address, as entered by the user when creating their account.
        /// Important! This email address is unverified; there is no proof that it actually belongs to the user.
        /// This field is only available when the current user has granted access to the <c>user-read-email</c> scope.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }

        /// <summary>
        /// The user’s explicit content settings. This field is only available when the current user has granted access to the <c>user-read-private</c> scope.
        /// </summary>
        [JsonPropertyName("explicit_content")]
        public ExplicitContentSettings ExplicitContent { get; set; }

        /// <summary>
        /// The user’s Spotify subscription level: “premium”, “free”, etc. (The subscription level “open” can be considered the same as “free”.)
        /// This field is only available when the current user has granted access to the <c>user-read-private</c> scope.
        /// </summary>
        [JsonPropertyName("product")]
        public string Product { get; set; }
    }
}
