using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using FluentSpotifyApi.Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.Core.UnitTests.Utils
{
    [TestClass]
    public class SpotifyUriUtilsTests
    {
        [TestMethod]
        public void ShouldGetRelativeUri()
        {
            // Arrange
            var routeValues = new object[] { "value1", 123, "value?2" };

            var queryParams = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("key1", "value1"),
                new KeyValuePair<string, object>("ke 1", new Uri("http://localhost/te%20st?ke%201=test%20value&key2=value2"))
            };

            // Act
            var result = SpotifyUriUtils.GetRelativeUri(routeValues, queryParams);

            // Assert
            result.Should().Be("value1/123/value%3F2?key1=value1&ke%201=http%3A%2F%2Flocalhost%2Fte%2520st%3Fke%25201%3Dtest%2520value%26key2%3Dvalue2");
        }

        [TestMethod]
        public void ShouldConvertToBase64UriString()
        {
            // Arrange + Act + Assert
            SpotifyUriUtils.ConvertToBase64UriString(Encoding.UTF8.GetBytes("Test")).Should().Be("VGVzdA");
        }
    }
}