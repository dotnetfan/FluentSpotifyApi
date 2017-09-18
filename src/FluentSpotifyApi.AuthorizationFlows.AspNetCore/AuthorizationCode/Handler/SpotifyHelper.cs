using System;
using Newtonsoft.Json.Linq;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Handler
{
    /// <summary>
    /// Contains static methods that allow to extract user's information from a <see cref="JObject"/>
    /// instance retrieved from Spotify after a successful authentication process.
    /// </summary>
    public static class SpotifyHelper
    {
        /// <summary>
        /// Gets the user's name.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static string GetName(JObject user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return string.IsNullOrEmpty(user.Value<string>("display_name")) ? user.Value<string>("id") : user.Value<string>("display_name");
        }

        /// <summary>
        /// Gets the user's profile picture.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static string GetProfilePicture(JObject user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return TryGetFirstValue(user, "images", "url");
        }

        private static string TryGetFirstValue(JObject user, string propertyName, string subProperty)
        {
            if (user.TryGetValue(propertyName, out JToken value))
            {
                var array = JArray.Parse(value.ToString());
                if (array != null && array.Count > 0)
                {
                    var subObject = JObject.Parse(array.First.ToString());
                    if (subObject != null)
                    {
                        if (subObject.TryGetValue(subProperty, out value))
                        {
                            return value.ToString();
                        }
                    }
                }
            }

            return null;
        }
    }
}
