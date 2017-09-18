using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Core.Client;
using FluentSpotifyApi.Model.Browse;
using FluentSpotifyApi.UnitTesting.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluentSpotifyApi.UnitTesting.UnitTests
{
    [TestClass]
    public class ServiceCollectionExtensionsTests
    {
        [TestMethod]
        public void ShouldRegisterNullHttpClient()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddFluentSpotifyClientForUnitTesting();

            services.BuildServiceProvider().GetRequiredService<HttpClientRegistrationWrapper>().Value.Should().BeNull();
        }

        [TestMethod]
        public async Task ShouldCallHttpClientWrapperMockAsync()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            var mock = new Mock<IHttpClientWrapper>();
            services.AddFluentSpotifyClientForUnitTesting(httpClientWrapperMock: mock);
            await services.BuildServiceProvider().GetRequiredService<IFluentSpotifyClient>().Browse.NewReleases.GetAsync();

            // Assert
            mock
                .Verify(
                x => x.SendAsync<NewReleases>(It.IsAny<HttpRequest<NewReleases>>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
