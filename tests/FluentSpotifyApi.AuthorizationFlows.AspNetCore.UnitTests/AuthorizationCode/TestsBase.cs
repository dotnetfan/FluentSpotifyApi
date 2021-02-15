using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode;
using FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.DependencyInjection;
using FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Handler;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.UnitTests.AuthorizationCode
{
    [TestClass]
    public class TestsBase
    {
        protected const string ClientId = "TestClientId";

        protected const string ClientSecret = "TestClientSecret";

        private IServiceProvider serviceProvider;

        protected MockHttpMessageHandler MockHttp { get; private set; }

        protected ClockStub Clock { get; private set; }

        protected AuthenticationManagerStub AuthenticationManager { get; private set; }

        protected HttpClient TestClient { get; private set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            var services = new ServiceCollection();

            this.Clock = new ClockStub();
            services.AddSingleton<ISystemClock>(this.Clock);

            this.AuthenticationManager = new AuthenticationManagerStub();
            services.AddSingleton<IAuthenticationManager>(this.AuthenticationManager);

            var httpContextAccessor = new HttpContextAccessorStub();
            services.AddSingleton<IHttpContextAccessor>(httpContextAccessor);

            var options = new SpotifyOptions
            {
                ClientId = ClientId,
                ClientSecret = ClientSecret,
            };

            var optionsSnapshotMock = new Mock<IOptionsSnapshot<SpotifyOptions>>();
            optionsSnapshotMock.Setup(x => x.Value).Returns(options);
            services.AddSingleton(optionsSnapshotMock.Object);

            this.MockHttp = new MockHttpMessageHandler();

            var builder = services.AddHttpClient("TestClient");
            builder
                .ConfigurePrimaryHttpMessageHandler(() => this.MockHttp);
            builder
                .AddSpotifyAuthorizationCodeFlow()
                .ConfigureTokenHttpClientBuilder(b => b.ConfigurePrimaryHttpMessageHandler(() => this.MockHttp));

            this.serviceProvider = services.BuildServiceProvider();
            httpContextAccessor.ServiceProvider = this.serviceProvider;

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

        protected class ClockStub : ISystemClock
        {
            DateTimeOffset ISystemClock.UtcNow => this.Time;

            public DateTimeOffset Time { get; set; } = new DateTime(2015, 3, 4, 10, 33, 14, DateTimeKind.Utc);

            public DateTimeOffset Add(TimeSpan timeSpan) => this.Time = this.Time.Add(timeSpan);
        }

        protected class AuthenticationManagerStub : IAuthenticationManager
        {
            private IAuthenticateResult authenticateResult;

            public void SetAuthenticateResult(string userId, string refreshToken, string accessToken, DateTimeOffset expiresAt)
            {
                var principal = new ClaimsPrincipal();
                principal.AddIdentity(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId) }));

                var properties = new AuthenticationProperties(new Dictionary<string, string>()
                {
                    [".Token.refresh_token"] = refreshToken,
                    [".Token.access_token"] = accessToken,
                    [".Token.expires_at"] = expiresAt.ToString("o", CultureInfo.InvariantCulture)
                });

                this.authenticateResult = new AuthenticateResult { Principal = principal, Properties = properties };
            }

            Task<IAuthenticateResult> IAuthenticationManager.GetAsync(CancellationToken cancellationToken) => Task.FromResult(this.authenticateResult);

            Task IAuthenticationManager.UpdateAsync(IAuthenticateResult authenticateResult, CancellationToken cancellationToken)
            {
                this.authenticateResult = authenticateResult;

                return Task.CompletedTask;
            }

            private class AuthenticateResult : IAuthenticateResult
            {
                public ClaimsPrincipal Principal { get; set; }

                public AuthenticationProperties Properties { get; set; }
            }
        }

        private class HttpContextAccessorStub : IHttpContextAccessor
        {
            public IServiceProvider ServiceProvider { private get; set; }

            HttpContext IHttpContextAccessor.HttpContext
            {
                get => new DefaultHttpContext { RequestServices = this.ServiceProvider };
                set => throw new NotImplementedException();
            }
        }
    }
}
