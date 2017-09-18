using Newtonsoft.Json;

namespace FluentSpotifyApi.Core.Model
{
    /// <summary>
    /// The private user.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Core.Model.UserBase" />
    public class PrivateUser : UserBase
    {
        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        [JsonProperty(PropertyName = "birthdate")]
        public string BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        [JsonProperty(PropertyName = "product")]
        public string Product { get; set; }
    }
}
