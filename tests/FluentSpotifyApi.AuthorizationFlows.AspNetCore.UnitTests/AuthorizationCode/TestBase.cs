using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode;
using FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Extensions;
using FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Handler;
using FluentSpotifyApi.AuthorizationFlows.Core.Client;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.Core.Client;
using FluentSpotifyApi.Core.Internal;
using FluentSpotifyApi.Core.Internal.Extensions;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Options;
using FluentSpotifyApi.UnitTesting.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Language;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.UnitTests.AuthorizationCode
{
    [TestClass]
    [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1009:ClosingParenthesisMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
    public class TestBase
    {
        private const string ClientId = "Test Client ID";

        private const string ClientSecret = "Test Client Secret";

        private readonly string tokenEndpoint = "http://localhost/token";
       
        private IServiceProvider serviceProvider;

        protected AuthenticationManagerStub AuthenticationManager { get; private set; }

        protected FluentSpotifyClientOptions FluentSpotifyClientOptions { get; set; }

        protected Mock<IHttpClientWrapper> HttpClientWrapperMock { get; private set; }

        protected Mock<IAuthorizationFlowsHttpClient> AuthorizationFlowsHttpClientMock { get; private set; }

        protected Mock<IOptionsMonitor<SpotifyOptions>> SpotifyOptionsMonitorMock { get; private set; }

        protected Mock<ISystemClock> SystemClockMock { get; private set; }

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
                        .AddAspNetCoreAuthorizationCodeFlow());

            this.AuthorizationFlowsHttpClientMock = new Mock<IAuthorizationFlowsHttpClient>(MockBehavior.Strict);
            services.Replace(ServiceDescriptor.Singleton(this.AuthorizationFlowsHttpClientMock.Object));

            this.AuthenticationManager = new AuthenticationManagerStub();
            services.Replace(ServiceDescriptor.Singleton<IAuthenticationManager>(this.AuthenticationManager));

            this.SystemClockMock = new Mock<ISystemClock>(MockBehavior.Strict);
            services.RegisterSingleton(this.SystemClockMock.Object);

            this.SpotifyOptionsMonitorMock = new Mock<IOptionsMonitor<SpotifyOptions>>(MockBehavior.Strict);
            this.SpotifyOptionsMonitorMock.Setup(x => x.CurrentValue).Returns(new SpotifyOptions { ClientId = ClientId, ClientSecret = ClientSecret, TokenEndpoint = this.tokenEndpoint });
            services.RegisterSingleton(this.SpotifyOptionsMonitorMock.Object);

            services.Replace(ServiceDescriptor.Singleton<ISemaphoreProvider>(new SempahoreProviderStub()));

            this.serviceProvider = services.BuildServiceProvider();

            this.FluentSpotifyClientOptions = this.serviceProvider.GetRequiredService<Microsoft.Extensions.Options.IOptionsMonitor<FluentSpotifyClientOptions>>().CurrentValue;

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

        protected ISetupSequentialResult<Task<AccessTokenDto>> MockGetAccessToken(string refereshToken)
        {
            return this.AuthorizationFlowsHttpClientMock
                .SetupSequence(x => x.SendAsync<AccessTokenDto>(
                    It.Is<UriParts>(item => 
                        item.BaseUri == new Uri(this.tokenEndpoint) &&
                        item.QueryStringParameters == null &&
                        item.RouteValues == null),
                    HttpMethod.Post,
                    It.Is<IEnumerable<KeyValuePair<string, string>>>((IEnumerable<KeyValuePair<string, string>> item) => item.Single().Equals(new KeyValuePair<string, string>("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}"))}"))),
                    It.Is<object>(item =>
                        SpotifyObjectHelpers.GetPropertyBag(item).Count() == 2 &&
                        SpotifyObjectHelpers.GetPropertyBag(item).Contains(new KeyValuePair<string, object>("grant_type", "refresh_token")) &&
                        SpotifyObjectHelpers.GetPropertyBag(item).Contains(new KeyValuePair<string, object>("refresh_token", refereshToken))),
                    It.IsAny<CancellationToken>()));
        }

        protected ISetupSequentialResult<Task<FullPlaylist>> MockGetPlaylistHttpClientWrapper(string userId, string playlistId, string accessToken)
        {
            return this.HttpClientWrapperMock
                .SetupSequence(x => x.SendAsync<FullPlaylist>(
                    It.Is<HttpRequest<FullPlaylist>>(item =>
                        item.UriFromValuesBuilder.Build() == new Uri(this.FluentSpotifyClientOptions.WebApiEndpoint, $"users/{userId}/playlists/{playlistId}") &&
                        item.RequestHeaders.Contains(new KeyValuePair<string, string>("Authorization", $"Bearer {accessToken}"))),
                    It.IsAny<CancellationToken>()));
        }

        protected class AuthenticationManagerStub : IAuthenticationManager
        {
            private IAuthenticateResult authenticateResult;

            public void SetAuthenticateResult((string UserId, string RefreshToken, string AccessToken, DateTimeOffset ExpiresAt) authenticateResult)
            {
                var principal = new ClaimsPrincipal();
                principal.AddIdentity(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, authenticateResult.UserId) }));

                var properties = new AuthenticationProperties(new Dictionary<string, string>()
                {
                    { ".Token.refresh_token", authenticateResult.RefreshToken },
                    { ".Token.access_token", authenticateResult.AccessToken },
                    { ".Token.expires_at", authenticateResult.ExpiresAt.ToString("o", CultureInfo.InvariantCulture) },
                });

                this.authenticateResult = new AuthenticateResult { Principal = principal, Properties = properties };
            }

            Task<IAuthenticateResult> IAuthenticationManager.GetAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(this.authenticateResult);
            }

            Task IAuthenticationManager.UpdateAsync(IAuthenticateResult authenticateResult, CancellationToken cancellationToken)
            {
                this.authenticateResult = authenticateResult;

                return Task.FromResult(0);
            }

            private class AuthenticateResult : IAuthenticateResult
            {
                public ClaimsPrincipal Principal { get; set; }

                public AuthenticationProperties Properties { get; set; }
            }
        }

        private class SempahoreProviderStub : ISemaphoreProvider, IDisposable
        {
            private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);

            public SemaphoreSlim Get() => this.semaphore;

            public void Dispose()
            {
                this.semaphore.Dispose();
            }
        }
    }
}
