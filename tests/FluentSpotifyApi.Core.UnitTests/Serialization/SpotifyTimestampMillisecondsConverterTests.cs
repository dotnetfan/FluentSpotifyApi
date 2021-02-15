using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using FluentSpotifyApi.Core.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.Core.UnitTests.Serialization
{
    [TestClass]
    public class SpotifyTimestampMillisecondsConverterTests
    {
        [TestMethod]
        public void ShouldWriteTimestampMilliseconds()
        {
            // Arrange
            var value = new Test
            {
                Timestamp = new DateTime(1993, 3, 1, 11, 12, 30, DateTimeKind.Utc)
            };

            // Act
            var result = JsonSerializer.Serialize(value);

            // Assert
            using var j = JsonDocument.Parse(result);
            j.RootElement.TryGetProperty("Timestamp", out var timestamp).Should().BeTrue();
            timestamp.GetInt64().Should().Be(730984350000);
        }

        [TestMethod]
        public void ShouldWriteNull()
        {
            // Arrange
            var value = new Test();

            // Act
            var result = JsonSerializer.Serialize(value);

            // Assert
            using var j = JsonDocument.Parse(result);
            j.RootElement.TryGetProperty("Timestamp", out var timestamp).Should().BeTrue();
            timestamp.ValueKind.Should().Be(JsonValueKind.Null);
        }

        [TestMethod]
        public void ShouldReadTimeMilliseconds()
        {
            // Arrange
            var value = "{\"Timestamp\":730984350000}";

            // Act
            var result = JsonSerializer.Deserialize<Test>(value);

            // Assert
            result.Should().BeEquivalentTo(new Test { Timestamp = new DateTime(1993, 3, 1, 11, 12, 30, DateTimeKind.Utc) });
        }

        [TestMethod]
        public void ShouldReadNull()
        {
            // Arrange
            var value = "{\"Timestamp\":null}";

            // Act
            var result = JsonSerializer.Deserialize<Test>(value);

            // Assert
            result.Should().BeEquivalentTo(new Test());
        }

        [TestMethod]
        public void ShouldReadOmitted()
        {
            // Arrange
            var value = "{}";

            // Act
            var result = JsonSerializer.Deserialize<Test>(value);

            // Assert
            result.Should().BeEquivalentTo(new Test());
        }

        private class Test
        {
            [JsonConverter(typeof(SpotifyTimestampMillisecondsConverter))]
            public DateTime? Timestamp { get; set; }
        }
    }
}
