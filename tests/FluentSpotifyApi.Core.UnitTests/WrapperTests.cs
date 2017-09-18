using System;
using FluentAssertions;
using FluentSpotifyApi.Core.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluentSpotifyApi.Core.UnitTests
{
    [TestClass]
    public class WrapperTests
    {
        [TestMethod]
        public void ShouldWrapValue()
        {
            // Arrange
            var value = new object();

            // Act
            var wrapper = new Wrapper<object>(value, true);

            // Assert
            wrapper.Value.Should().BeSameAs(value);
        }

        [TestMethod]
        public void ShouldDisposeWrappedValueWhenOwned()
        {
            // Arrange
            var mock = new Mock<IDisposable>();
            var wrapper = new Wrapper<IDisposable>(mock.Object, true);

            // Act
            wrapper.Dispose();

            // Assert
            mock.Verify(x => x.Dispose(), Times.Once);
        }

        [TestMethod]
        public void ShouldNotDisposeWrappedValueWhenNotOwned()
        {
            // Arrange
            var mock = new Mock<IDisposable>();
            var wrapper = new Wrapper<IDisposable>(mock.Object, false);

            // Act
            wrapper.Dispose();

            // Assert
            mock.Verify(x => x.Dispose(), Times.Never);
        }
    }
}
