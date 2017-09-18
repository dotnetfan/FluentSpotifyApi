using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentSpotifyApi.Core.Internal.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.Core.UnitTests
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        [TestMethod]
        public void ShouldReturnEmptySequenceIfProvidedIsNull()
        {
            // Arrange + Act + Assert
            ((IEnumerable<object>)null).EmptyIfNull().Should().BeEmpty();
        }

        [TestMethod]
        public void ShouldReturnSameSequenceIfProvidedIsNotNull()
        {
            // Arrange
            var sequence = Enumerable.Empty<object>();

            // Act + Assert
            sequence.EmptyIfNull().Should().BeSameAs(sequence);
        }

        [TestMethod]
        public void ShouldGetQueryString()
        {
            // Arrange
            var query = new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("key1", "value1"), new KeyValuePair<string, object>("ke 1", new Uri("http://localhost/te%20st?ke%201=test%20value&key2=value2")) };

            // Act
            var result = query.GetQueryString();

            // Assert
            result.Should().Be("key1=value1&ke%201=http%3A%2F%2Flocalhost%2Fte%2520st%3Fke%25201%3Dtest%2520value%26key2%3Dvalue2");
        }
    }
}