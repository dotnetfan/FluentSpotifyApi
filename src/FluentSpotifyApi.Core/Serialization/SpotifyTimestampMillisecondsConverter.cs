using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.Core.Serialization
{
    /// <summary>
    /// Provides conversion between epoch timestamp in milliseconds (i.e. number of milliseconds since 1/1/1970) and <see cref="DateTime"/>.
    /// </summary>
    public class SpotifyTimestampMillisecondsConverter : JsonConverter<DateTime>
    {
        /// <inheritdoc />
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var ms = reader.GetInt64();
            return SpotifyDateTimeConversionUtils.FromTimestampMilliseconds(ms);
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var ms = SpotifyDateTimeConversionUtils.ToTimestampMilliseconds(value);
            writer.WriteNumberValue(ms);
        }
    }
}
