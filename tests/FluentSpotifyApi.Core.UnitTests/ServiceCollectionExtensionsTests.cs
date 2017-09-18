using System;
using FluentAssertions;
using FluentSpotifyApi.Core.Internal.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.Core.UnitTests
{
    [TestClass]
    public class ServiceCollectionExtensionsTests
    {
        [TestMethod]
        public void ShoudlRegisterServiceInServiceCollection()
        {
            // Arrange
            var serviceDescriptor = new ServiceDescriptor(typeof(object), new object());
            var services = new ServiceCollection();

            // Act
            services.Register(serviceDescriptor);

            // Assert
            services.Should().Contain(serviceDescriptor);
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenTryingToRegisterAlreadyRegisteredService()
        {
            // Arrange
            var serviceDescriptor = new ServiceDescriptor(typeof(object), new object());
            var newServiceDescriptor = new ServiceDescriptor(typeof(object), new object());
            var services = new ServiceCollection();

            // Act
            services.Register(serviceDescriptor);

            // Assert
            ((Action)(() => services.Register(newServiceDescriptor))).ShouldThrow<InvalidOperationException>().WithMessage($"Service of type {typeof(object)} has already been registered.");
        }
    }
}
