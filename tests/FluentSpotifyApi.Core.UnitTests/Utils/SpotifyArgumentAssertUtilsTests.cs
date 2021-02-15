using System;
using FluentAssertions;
using FluentSpotifyApi.Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.Core.UnitTests.Utils
{
    [TestClass]
    public class SpotifyArgumentAssertUtilsTests
    {
        [TestMethod]
        public void ShouldThrowIfNullThrowArgumentNullException()
        {
            ((Action)(() => SpotifyArgumentAssertUtils.ThrowIfNull<object>(null, "test"))).Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ShouldNotThrowIfNullThrowException()
        {
            ((Action)(() => SpotifyArgumentAssertUtils.ThrowIfNull("test", "test"))).Should().NotThrow();
        }

        [TestMethod]
        public void ShouldThrowIfNullOrEmptyThrowArgumentNullExceptionForNullString()
        {
            ((Action)(() => SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(null, "test"))).Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ShouldThrowIfNullOrEmptyThrowArgumentExceptionForEmptyString()
        {
            ((Action)(() => SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(string.Empty, "test"))).Should().Throw<ArgumentException>().WithMessage("*Value cannot be empty.*");
        }

        [TestMethod]
        public void ShouldNotThrowIfNullOrEmptyThrowException()
        {
            ((Action)(() => SpotifyArgumentAssertUtils.ThrowIfNull("test", "test"))).Should().NotThrow();
        }
    }
}
