using System;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.User;
using FluentSpotifyApi.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.UnitTests.Builder
{
    [TestClass]
    public abstract class TestsBase
    {
        protected const string UserId = "TestUser";

        private IServiceProvider serviceProvider;

        protected MockHttpMessageHandler MockHttp { get; private set; }

        protected IFluentSpotifyClient Client { get; private set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ICurrentUserProvider, CurrentUserProviderStub>();

            this.MockHttp = new MockHttpMessageHandler();

            services.AddFluentSpotifyClient().ConfigureHttpClientBuilder(b => b.ConfigurePrimaryHttpMessageHandler(() => this.MockHttp));

            this.serviceProvider = services.BuildServiceProvider();

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

        private class CurrentUserProviderStub : ICurrentUserProvider
        {
            public Task<IUser> GetAsync(CancellationToken cancellationToken) => Task.FromResult<IUser>(new TestUser());
        }

        private class TestUser : IUser
        {
            public string Id => UserId;
        }
    }
}
