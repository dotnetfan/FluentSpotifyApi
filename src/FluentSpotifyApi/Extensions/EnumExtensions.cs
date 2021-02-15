using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace FluentSpotifyApi.Extensions
{
    internal static class EnumExtensions
    {
        public static string GetEnumMemberValue(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            var field = type.GetField(name);
            var attr = field.GetCustomAttribute<EnumMemberAttribute>();
            if (attr != null)
            {
                return attr.Value;
            }

            return null;
        }
    }
}
