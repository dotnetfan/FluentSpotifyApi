using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.Core.Serialization
{
    /// <summary>
    /// Provides conversion between time interval represented as whole milliseconds and <see cref="TimeSpan"/>.
    /// </summary>
    public class SpotifyTimeSpanMillisecondsConverter : JsonConverter<TimeSpan>
    {
        /// <inheritdoc />
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var ms = reader.GetInt32();
            return SpotifyTimeSpanConversionUtils.FromWholeMilliseconds(ms);
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            var ms = SpotifyTimeSpanConversionUtils.ToWholeMilliseconds(value);
            writer.WriteNumberValue(ms);
        }
    }
}
