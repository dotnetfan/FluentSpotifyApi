using System;
using FluentSpotifyApi.AuthorizationFlows.ClientCredentials.Extensions;
using FluentSpotifyApi.AuthorizationFlows.Core.Client;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.AuthorizationFlows.Core.Date;
using FluentSpotifyApi.Core.Client;
using FluentSpotifyApi.Core.Options;
using FluentSpotifyApi.UnitTesting.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluentSpotifyApi.AuthorizationFlows.UnitTests.ClientCredentials
{
    [TestClass]
    public class TestBase
    {
        private IServiceProvider serviceProvider;

        protected Mock<IHttpClientWrapper> HttpClientWrapperMock { get; private set; }

        protected Mock<IAuthorizationFlowsHttpClient> AuthorizationFlowsHttpClientMock { get; private set; }

        protected Mock<IDateTimeOffsetProvider> DateTimeOffsetProviderMock { get; private set; }

        protected ITokenClientOptions Options { get; private set; }

        protected IFluentSpotifyClient Client { get; private set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            this.HttpClientWrapperMock = new Mock<IHttpClientWrapper>(MockBehavior.Strict);

            var services = new ServiceCollection();
            services
                .AddFluentSpotifyClientForUnitTesting(
                    this.HttpClientWrapperMock, 
                    pipeline => pipeline
                        .AddClientCredentialsFlow(o =>
                        {
                            o.ClientId = "TestClientId";
                            o.ClientSecret = "TestClientSecret";
                        }));

            this.AuthorizationFlowsHttpClientMock = new Mock<IAuthorizationFlowsHttpClient>(MockBehavior.Strict);
            services.Replace(ServiceDescriptor.Singleton(this.AuthorizationFlowsHttpClientMock.Object));

            this.DateTimeOffsetProviderMock = new Mock<IDateTimeOffsetProvider>(MockBehavior.Strict);
            services.Replace(ServiceDescriptor.Singleton(this.DateTimeOffsetProviderMock.Object));

            this.serviceProvider = services.BuildServiceProvider();

            this.Options = this.serviceProvider.GetRequiredService<IOptionsProvider<ITokenClientOptions>>().Get();

            this.Client = this.serviceProvider.GetRequiredService<IFluentSpotifyClient>();
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            if (this.serviceProvider != null)
            {
                (this.serviceProvider as IDisposable).Dispose();
            }
        }
    }
}
