using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using FluentSpotifyApi.Core.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.Core.UnitTests.Serialization
{
    [TestClass]
    public class SpotifyTimeSpanMillisecondsConverterTests
    {
        [TestMethod]
        public void ShouldWriteTimeMilliseconds()
        {
            // Arrange
            var value = new Test
            {
                Duration = TimeSpan.FromMinutes(3)
            };

            // Act
            var result = JsonSerializer.Serialize(value);

            // Assert
            using var j = JsonDocument.Parse(result);
            j.RootElement.TryGetProperty("Duration", out var duration).Should().BeTrue();
            duration.GetInt32().Should().Be(180000);
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
            j.RootElement.TryGetProperty("Duration", out var duration).Should().BeTrue();
            duration.ValueKind.Should().Be(JsonValueKind.Null);
        }

        [TestMethod]
        public void ShouldReadTimeMilliseconds()
        {
            // Arrange
            var value = "{\"Duration\":180000}";

            // Act
            var result = JsonSerializer.Deserialize<Test>(value);

            // Assert
            result.Should().BeEquivalentTo(new Test { Duration = TimeSpan.FromMinutes(3) });
        }

        [TestMethod]
        public void ShouldReadNull()
        {
            // Arrange
            var value = "{\"Duration\":null}";

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
            [JsonConverter(typeof(SpotifyTimeSpanMillisecondsConverter))]
            public TimeSpan? Duration { get; set; }
        }
    }
}
