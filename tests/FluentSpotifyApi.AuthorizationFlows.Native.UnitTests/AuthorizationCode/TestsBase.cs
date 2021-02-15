using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using FluentSpotifyApi.AuthorizationFlows.Core.Time;
using FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode;
using FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.AuthorizationFlows.Native.UnitTests.AuthorizationCode
{
    [TestClass]
    public class TestsBase
    {
        protected const string ClientId = "TestClientId";

        private IServiceProvider serviceProvider;

        protected MockHttpMessageHandler MockHttp { get; private set; }

        protected ClockStub Clock { get; private set; }

        protected CodeVerifierProviderStub CodeVerifierProvider { get; private set; }

        protected AuthorizationRedirectUriProviderStub AuthorizationRedirectUriProvider { get; private set; }

        protected AuthorizationInteractionClientStub AuthorizationInteractionClient { get; private set; }

        protected IAuthenticationTicketStorage AuhenticationTicketStorage { get; private set; }

        protected IAuthenticationManager AuthenticationManager { get; private set; }

        protected HttpClient TestClient { get; private set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            var services = new ServiceCollection();

            this.Clock = new ClockStub();
            services.AddSingleton<IClock>(this.Clock);

            this.CodeVerifierProvider = new CodeVerifierProviderStub();
            services.AddSingleton<ICodeVerifierProvider>(this.CodeVerifierProvider);

            this.AuthorizationRedirectUriProvider = new AuthorizationRedirectUriProviderStub();
            services.AddSingleton<IAuthorizationRedirectUriProvider>(this.AuthorizationRedirectUriProvider);

            this.AuthorizationInteractionClient = new AuthorizationInteractionClientStub();
            services.AddSingleton<IAuthorizationInteractionClient>(this.AuthorizationInteractionClient);

            this.AuhenticationTicketStorage = new AuthenticationTicketStorageStub();
            services.AddSingleton<IAuthenticationTicketStorage>(this.AuhenticationTicketStorage);

            this.MockHttp = new MockHttpMessageHandler();

            var builder = services.AddHttpClient("TestClient");
            builder
                .ConfigurePrimaryHttpMessageHandler(() => this.MockHttp);
            builder
                .AddSpotifyAuthorizationCodeFlowCore<SpotifyAuthorizationCodeFlowOptions>(o => { o.ClientId = ClientId; })
                .ConfigureTokenHttpClientBuilder(b => b.ConfigurePrimaryHttpMessageHandler(() => this.MockHttp))
                .ConfigureUserHttpClientBuilder(b => b.ConfigurePrimaryHttpMessageHandler(() => this.MockHttp));

            this.serviceProvider = services.BuildServiceProvider();

            this.AuthenticationManager = this.serviceProvider.GetRequiredService<IAuthenticationManager>();
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

        protected class CodeVerifierProviderStub : ICodeVerifierProvider
        {
            public string CodeVerifier { get; set; }

            string ICodeVerifierProvider.Get() => this.CodeVerifier;
        }

        protected class AuthorizationRedirectUriProviderStub : IAuthorizationRedirectUriProvider
        {
            public Uri Uri { get; set; }

            Uri IAuthorizationRedirectUriProvider.Get() => this.Uri;
        }

        protected class AuthorizationInteractionClientStub : IAuthorizationInteractionClient
        {
            public string AuthorizationCode { get; set; }

            Task<Uri> IAuthorizationInteractionClient.AuthorizeAsync(Uri authorizationUri, Uri redirectUri, CancellationToken cancellationToken)
            {
                return Task.FromResult(new Uri($"{authorizationUri.AbsoluteUri}&code={this.AuthorizationCode}"));
            }
        }

        private class AuthenticationTicketStorageStub : IAuthenticationTicketStorage
        {
            private ValueWrapper valueWrapper;

            public Task<bool> RemoveAsync(CancellationToken cancellationToken)
            {
                if (this.valueWrapper != null)
                {
                    this.valueWrapper = null;
                    return Task.FromResult(true);
                }

                return Task.FromResult(false);
            }

            public Task SaveAsync(string value, CancellationToken cancellationToken)
            {
                this.valueWrapper = new ValueWrapper { Value = value };

                return Task.CompletedTask;
            }

            public Task<(bool IsSuccess, string Value)> TryGetAsync(CancellationToken cancellationToken)
            {
                if (this.valueWrapper != null)
                {
                    return Task.FromResult((true, this.valueWrapper.Value));
                }

                return Task.FromResult((false, null as string));
            }

            private class ValueWrapper
            {
                public string Value { get; set; }
            }
        }
    }
}
