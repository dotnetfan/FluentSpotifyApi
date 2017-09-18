using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Core.Client;
using FluentSpotifyApi.Core.Extensions;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.UnitTesting.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluentSpotifyApi.UnitTests.Configuration
{
    [TestClass]
    public class ClientBuilderTests
    {
        [TestMethod]
        public async Task ShouldExecutePipelineInOrderAsync()
        {
            // Arrange
            var orders = new List<int>();
            var services = new ServiceCollection();

            // Act
            services
                .AddFluentSpotifyClientForUnitTesting(
                    new Mock<IHttpClientWrapper>(), 
                    pipeline => pipeline
                        .AddDelegate((next, cancellationToken) => { orders.Add(1); return next(cancellationToken); })
                        .AddDelegate((next, cancellationToken) => { orders.Add(2); return next(cancellationToken); })
                        .AddDelegate((next, httpRequest, resultType, cancellationToken) => { resultType.Should().Be(typeof(FullAlbum)); orders.Add(3); return next(httpRequest, cancellationToken); })
                        .AddDelegate((next, cancellationToken) => { orders.Add(4); return next(cancellationToken); }));

            await services.BuildServiceProvider().GetRequiredService<IFluentSpotifyClient>().Album("3232").GetAsync();

            // Assert
            orders.Should().Equal(new[] { 1, 2, 3, 4 });
        }

        [TestMethod]
        public void DefaultHttpClientShouldHaveTimeOutOneMinute()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddFluentSpotifyClient();

            // Act
            var httpClient = services.BuildServiceProvider().GetRequiredService<HttpClientRegistrationWrapper>().Value;
                
            // Assert
            httpClient.Timeout.Should().Be(TimeSpan.FromMinutes(1));
        }

        [TestMethod]
        public void ShouldConfigureHttpClient()
        {
            // Arrange
            var services = new ServiceCollection();
            services
                .AddFluentSpotifyClient(clientBuilder => clientBuilder
                    .ConfigureHttpClient(x => { x.Timeout = TimeSpan.FromMinutes(2); }));

            // Act
            var httpClient = services.BuildServiceProvider().GetRequiredService<HttpClientRegistrationWrapper>().Value;
            
            // Assert
            httpClient.Timeout.Should().Be(TimeSpan.FromMinutes(2));
        }

        [TestMethod]
        public void ShouldUseCustomHttpClient()
        {
            // Arrange
            var httpClient = new HttpClient();

            var services = new ServiceCollection();
            services
                .AddFluentSpotifyClient(clientBuilder => clientBuilder
                    .UseHttpClientFactory(serviceProvider => httpClient));

            // Act
            var registeredHttpClient = services.BuildServiceProvider().GetRequiredService<HttpClientRegistrationWrapper>().Value;
            
            // Assert                
            registeredHttpClient.Should().BeSameAs(httpClient);
        }

        [TestMethod]
        public void ShouldUseCustomHttpClientAndConfigureIt()
        {
            // Arrange
            var httpClient = new HttpClient();

            var services = new ServiceCollection();
            services
                .AddFluentSpotifyClient(clientBuilder => clientBuilder
                    .UseHttpClientFactory(serviceProvider => httpClient)
                    .ConfigureHttpClient(x => { x.Timeout = TimeSpan.FromSeconds(10); }));

            // Act
            var registeredHttpClient = services.BuildServiceProvider().GetRequiredService<HttpClientRegistrationWrapper>().Value;

            // Assert
            registeredHttpClient.Should().BeSameAs(httpClient);
            registeredHttpClient.Timeout.Should().Be(TimeSpan.FromSeconds(10));
        }

        [TestMethod]
        public void TheDefaultHttpClientShouldBeDisposedWithServiceProvider()
        {
            // Arrange
            var httpClient = new HttpClient();

            var services = new ServiceCollection();
            services
                .AddFluentSpotifyClient();

            // Act
            var registration = services.BuildServiceProvider().GetRequiredService<HttpClientRegistrationWrapper>();

            // Assert
            registration.IsOwned.Should().Be(true);
        }

        [TestMethod]
        public void TheHttpClientFromFactoryShouldBeDisposedWithServiceProviderWhenDisposeWithServiceProviderIsTrue()
        {
            // Arrange
            var httpClient = new HttpClient();

            var services = new ServiceCollection();
            services
                .AddFluentSpotifyClient(clientBuilder => clientBuilder.UseHttpClientFactory(serviceProvider => new HttpClient(), disposeWithServiceProvider: true));

            // Act
            var registration = services.BuildServiceProvider().GetRequiredService<HttpClientRegistrationWrapper>();

            // Assert
            registration.IsOwned.Should().Be(true);
        }

        [TestMethod]
        public void TheHttpClientFromFactoryShouldNotBeDisposedWithServiceProviderWhenDisposeWithServiceProviderIsFalse()
        {
            // Arrange
            var httpClient = new HttpClient();

            var services = new ServiceCollection();
            services
                .AddFluentSpotifyClient(clientBuilder => clientBuilder.UseHttpClientFactory(serviceProvider => new HttpClient(), disposeWithServiceProvider: false));

            // Act
            var registration = services.BuildServiceProvider().GetRequiredService<HttpClientRegistrationWrapper>();

            // Assert
            registration.IsOwned.Should().Be(false);
        }
    }
}