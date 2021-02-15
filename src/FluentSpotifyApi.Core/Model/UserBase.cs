using System.Text.Json.Serialization;

namespace FluentSpotifyApi.Core.Model
{
    /// <summary>
    /// The base class for users.
    /// </summary>
    /// <seealso cref="EntityBase" />
    public abstract class UserBase : EntityBase
    {
        /// <summary>
        /// The name displayed on the user’s profile. null if not available.
        /// </summary>
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Information about the followers of the user.
        /// </summary>
        [JsonPropertyName("followers")]
        public Followers Followers { get; set; }

        /// <summary>
        /// The user’s profile image.
        /// </summary>
        [JsonPropertyName("images")]
        public Image[] Images { get; set; }
    }
}
