using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace FluentSpotifyApi.Core.Internal.Extensions
{
    /// <summary>
    /// Set of <see cref="Enum"/> extensions.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name != null)
            {
                var field = type.GetRuntimeField(name);
                if (field != null)
                {
                    var attr = field.GetCustomAttribute<DescriptionAttribute>();
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetValues<T>() where T : struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
