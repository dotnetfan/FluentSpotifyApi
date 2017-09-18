using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluentSpotifyApi.Core.Internal
{
    /// <summary>
    /// The set of object helpers intended for internal use only.
    /// </summary>
    public static class SpotifyObjectHelpers
    {
        /// <summary>
        /// Gets the property bag for the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> GetPropertyBag(object value)
        {
            return GetPropertyBagRecursively(value);
        }

        private static IEnumerable<KeyValuePair<string, object>> GetPropertyBagRecursively(object value)
        {
            if (value == null)
            {
                yield break;
            }

            var type = value.GetType();

            foreach (var property in type.GetRuntimeProperties())
            {
                var propertyValue = property.GetValue(value);
                var typeInfo = property.PropertyType.GetTypeInfo();

                if (typeof(IEnumerable<KeyValuePair<string, object>>).GetTypeInfo().IsAssignableFrom(typeInfo))
                {
                    foreach (var item in (IEnumerable<KeyValuePair<string, object>>)propertyValue)
                    {
                        yield return item;
                    }
                }
                else if (typeof(KeyValuePair<string, object>).GetTypeInfo().IsAssignableFrom(typeInfo))
                {
                    yield return (KeyValuePair<string, object>)propertyValue;
                }
                else if (property.PropertyType == typeof(string) ||
                    property.PropertyType == typeof(Uri) ||
                    typeInfo.IsValueType ||
                    typeof(ITransformer).GetTypeInfo().IsAssignableFrom(typeInfo))
                {
                    yield return new KeyValuePair<string, object>(property.Name, propertyValue);
                }
                else
                {
                    foreach (var item in GetPropertyBagRecursively(propertyValue))
                    {
                        yield return item;
                    }
                }
            }
        }
    }
}
