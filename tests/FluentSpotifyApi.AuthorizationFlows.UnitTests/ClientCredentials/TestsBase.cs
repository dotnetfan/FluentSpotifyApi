using System;
using System.Net.Http;
using FluentSpotifyApi.AuthorizationFlows.ClientCredentials.DependencyInjection;
using FluentSpotifyApi.AuthorizationFlows.Core.Time;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.AuthorizationFlows.UnitTests.ClientCredentials
{
    [TestClass]
    public class TestsBase
    {
        protected const string ClientId = "TestClientId";

        protected const string ClientSecret = "TestClientSecret";

        private IServiceProvider serviceProvider;

        protected MockHttpMessageHandler MockHttp { get; private set; }

        protected ClockStub Clock { get; private set; }

        protected HttpClient TestClient { get; private set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            var services = new ServiceCollection();

            this.Clock = new ClockStub();
            services.AddSingleton<IClock>(this.Clock);

            this.MockHttp = new MockHttpMessageHandler();

            var builder = services.AddHttpClient("TestClient");
            builder
                .ConfigurePrimaryHttpMessageHandler(() => this.MockHttp);
            builder
                .AddSpotifyClientCredentialsFlow(o =>
                {
                    o.ClientId = ClientId;
                    o.ClientSecret = ClientSecret;
                })
                .ConfigureTokenHttpClientBuilder(b => b.ConfigurePrimaryHttpMessageHandler(() => this.MockHttp));

            this.serviceProvider = services.BuildServiceProvider();

            this.TestClient = this.serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient("TestClient");
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            if (this.serviceProvider != null)
            {
                (this.serviceProvider as IDisposable).Dispose();
            }
        }

        protected class ClockStub : IClock
        {
            public DateTimeOffset Time { get; set; } = new DateTimeOffset(new DateTime(2015, 3, 4, 10, 33, 14, DateTimeKind.Utc));

            DateTimeOffset IClock.GetUtcNow() => this.Time;

            public DateTimeOffset Add(TimeSpan timeSpan) => this.Time = this.Time.Add(timeSpan);
        }
    }
}
