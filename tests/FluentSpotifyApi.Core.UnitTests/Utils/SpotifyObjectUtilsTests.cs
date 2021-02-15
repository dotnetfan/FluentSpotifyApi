using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentSpotifyApi.Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.Core.UnitTests.Utils
{
    [TestClass]
    public class SpotifyObjectUtilsTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetConversionData), DynamicDataSourceType.Method)]
        public void ShouldConvertObjectToCanonicalString(object value, string expected)
        {
            // Arrange + Act + Assert
            SpotifyObjectUtils.ConvertToCanonicalString(value).Should().Be(expected);
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenCallingConvertObjectToCanonicalStringForInvalidType()
        {
            // Arrange + Act + Assert
            Action action = () => SpotifyObjectUtils.ConvertToCanonicalString(new object());
            action.Should().Throw<InvalidOperationException>();
        }

        private static IEnumerable<object[]> GetConversionData()
        {
            yield return new object[] { null, string.Empty };
            yield return new object[] { "test", "test" };
            yield return new object[] { true, "true" };
            yield return new object[] { false, "false" };
            yield return new object[] { new Uri("http://localhost/test"), "http://localhost/test" };
            yield return new object[] { 123, "123" };
            yield return new object[] { 123.77, "123.77" };
        }

        private class TestClass
        {
            public string Id { get; set; }
        }
    }
}
