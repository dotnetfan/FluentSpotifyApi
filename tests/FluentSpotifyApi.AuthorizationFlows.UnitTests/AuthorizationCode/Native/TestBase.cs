using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native;
using FluentSpotifyApi.AuthorizationFlows.Core;
using FluentSpotifyApi.AuthorizationFlows.Core.Client;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.User;
using FluentSpotifyApi.AuthorizationFlows.Core.Date;
using FluentSpotifyApi.Core.Client;
using FluentSpotifyApi.Core.Internal.Extensions;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Core.Options;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Options;
using FluentSpotifyApi.UnitTesting.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Language;
using Newtonsoft.Json;

namespace FluentSpotifyApi.AuthorizationFlows.UnitTests.AuthorizationCode.Native
{
    [TestClass]
    [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1009:ClosingParenthesisMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
    public class TestBase
    {
        private IServiceProvider serviceProvider;

        protected SecureStorageStub SecureStorage { get; private set; }

        protected FluentSpotifyClientOptions FluentSpotifyClientOptions { get; set; }

        protected Mock<IHttpClientWrapper> HttpClientWrapperMock { get; private set; }

        protected Mock<IAuthorizationFlowsHttpClient> AuthorizationFlowsHttpClientMock { get; private set; }

        protected Mock<IAuthorizationCallbackUriProvider> AuthorizationCallbackUriProviderMock { get; private set; }

        protected Mock<IAuthorizationInteractionClient<string>> AuthorizationInteractionServiceMock { get; private set; }

        protected IAuthorizationOptions AuthorizationOptions { get; private set; }

        protected IUserClientOptions UserClientOptions { get; private set; }

        protected Mock<IDateTimeOffsetProvider> DateTimeOffsetProviderMock { get; private set; }

        protected IFluentSpotifyClient Client { get; private set; }

        protected IAuthenticationManager AuthenticationManager { get; private set; }

        protected IAuthenticationTicketProvider AuthenticationTicketProvider { get; private set; }

        protected Mock<ICsrfTokenProvider> CsfrTokenProviderMock { get; private set; }

        protected Mock<ITokenProxyClient> TokenProxyClientMock { get; private set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            this.HttpClientWrapperMock = new Mock<IHttpClientWrapper>(MockBehavior.Strict);

            var services = new ServiceCollection();
            services
                .AddFluentSpotifyClientForUnitTesting(
                this.HttpClientWrapperMock, 
                pipeline => pipeline.Add(new AuthorizationCodeFlowPipelineItemStub(
                this, 
                o =>
                {
                    o.ClientId = "TestClientId";
                    o.Scopes = new[] { Scope.PlaylistModifyPrivate, Scope.PlaylistReadPrivate };
                    o.DynamicScopes = new[] { "user-follow-read", "user-library-modify" };
                    o.AuthorizationEndpoint = new Uri("http://localhost/authorize");
                    o.UserInformationEndpoint = new Uri("http://localhost/user");
                })));

            this.AuthorizationFlowsHttpClientMock = new Mock<IAuthorizationFlowsHttpClient>(MockBehavior.Strict);
            services.Replace(ServiceDescriptor.Singleton(this.AuthorizationFlowsHttpClientMock.Object));

            this.DateTimeOffsetProviderMock = new Mock<IDateTimeOffsetProvider>(MockBehavior.Strict);
            services.Replace(ServiceDescriptor.Singleton(this.DateTimeOffsetProviderMock.Object));

            this.serviceProvider = services.BuildServiceProvider();

            this.FluentSpotifyClientOptions = this.serviceProvider.GetRequiredService<Microsoft.Extensions.Options.IOptionsMonitor<FluentSpotifyClientOptions>>().CurrentValue;

            this.AuthorizationOptions = this.serviceProvider.GetRequiredService<IOptionsProvider<IAuthorizationOptions>>().Get();

            this.UserClientOptions = this.serviceProvider.GetRequiredService<IOptionsProvider<IUserClientOptions>>().Get();

            this.Client = this.serviceProvider.GetRequiredService<IFluentSpotifyClient>();

            this.AuthenticationManager = this.serviceProvider.GetRequiredService<IAuthenticationManager>();

            this.AuthenticationTicketProvider = this.serviceProvider.GetRequiredService<IAuthenticationTicketProvider>();
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            if (this.serviceProvider != null)
            {
                (this.serviceProvider as IDisposable).Dispose();
            }
        }

        protected PrivateUser SetupAuthorizationFlow(Uri callBackUrl = null, string authorizationKey = "test authorization key", string accessToken = "test access token")
        {
            var callbackUrl = callBackUrl == null ? new Uri("http://localhost/callback") : callBackUrl;
            const string csfrToken = "csfr";
            const string authorizationCode = "test authorization code";
            const string userId = "testuser";
            var user = new PrivateUser { Id = userId };

            this.MockAuthorizationCallbackUrl(callbackUrl);
            this.MockCsfrTokenProvider(csfrToken);
            this.MockAuthorizationInteractionService(callbackUrl).Returns(Task.FromResult(new AuthorizationResponse<string>(authorizationCode, csfrToken)));
            this.MockGetAuthorizationTokens(authorizationCode).Returns(Task.FromResult(new ProxyAuthorizationTokens { AuthorizationKey = authorizationKey, AccessToken = new ProxyAccessToken { Token = accessToken, ExpiresIn = 2345 } }));
            this.MockGetUserInformation(accessToken).Returns(Task.FromResult(user));

            return user;
        }

        protected void MockAuthorizationCallbackUrl(Uri url)
        {
            this.AuthorizationCallbackUriProviderMock
                .Setup(x => x.Get())
                .Returns(url);
        }

        protected void MockCsfrTokenProvider(string csfrToken)
        {
            this.CsfrTokenProviderMock
                .Setup(x => x.Get())
                .Returns(csfrToken);
        }

        protected ISetupSequentialResult<Task<AuthorizationResponse<string>>> MockAuthorizationInteractionService(Uri callbackUrl)
        {
            var url = new Uri($"{this.AuthorizationOptions.AuthorizationEndpoint}?client_id=TestClientId&response_type=code&scope=playlist-modify-private%20playlist-read-private%20user-follow-read%20user-library-modify&show_dialog=false&state=csfr&redirect_uri={Uri.EscapeDataString(callbackUrl.AbsoluteUri)}");

            return this.AuthorizationInteractionServiceMock.SetupSequence(x => x.AuthorizeAsync(
                url,
                callbackUrl,
                It.IsAny<CancellationToken>()));
        }

        protected ISetupSequentialResult<Task<ProxyAuthorizationTokens>> MockGetAuthorizationTokens(string authorizationCode)
        {
            return this.TokenProxyClientMock
                .SetupSequence(x => x.GetAuthorizationTokensAsync(
                    authorizationCode,
                    It.IsAny<CancellationToken>()));
        }

        protected ISetupSequentialResult<Task<ProxyAccessToken>> MockGetAccessToken(string authorizationKey)
        {
            return this.TokenProxyClientMock
                .SetupSequence(x => x.GetAccessTokenAsync(
                    authorizationKey,
                    It.IsAny<CancellationToken>()));
        }

        protected ISetupSequentialResult<Task<PrivateUser>> MockGetUserInformation(string accessToken)
        {
            return this.AuthorizationFlowsHttpClientMock
                .SetupSequence(x => x.SendAsync<PrivateUser>(
                    It.Is<UriParts>(item => item.BaseUri == this.UserClientOptions.UserInformationEndpoint && item.QueryStringParameters == null && item.RouteValues == null),
                    HttpMethod.Get,
                    It.Is<IEnumerable<KeyValuePair<string, string>>>((IEnumerable<KeyValuePair<string, string>> item) => item.Single().Equals(new KeyValuePair<string, string>("Authorization", $"Bearer {accessToken}"))),
                    null,
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

        protected class AuthorizationCodeFlowPipelineItemStub : AuthorizationCodeFlowPipelineItemBase<FlowOptionsStub>
        {
            private readonly TestBase testBase;

            public AuthorizationCodeFlowPipelineItemStub(TestBase testBase, Action<FlowOptionsStub> configureAction) : base(configureAction)
            {
                this.testBase = testBase;
            }

            protected override void Configure(IServiceCollection services)
            {
                base.Configure(services);

                var authorizationCallbackUriProviderMock = new Mock<IAuthorizationCallbackUriProvider>(MockBehavior.Strict);
                services.RegisterSingleton(authorizationCallbackUriProviderMock.Object);

                var csrfTokenProviderMock = new Mock<ICsrfTokenProvider>(MockBehavior.Strict);
                services.RegisterSingleton(csrfTokenProviderMock.Object);

                var authorizationInteractionServiceMock = new Mock<IAuthorizationInteractionClient<string>>(MockBehavior.Strict);
                services.RegisterSingleton(authorizationInteractionServiceMock.Object);

                var tokenProxyClientMock = new Mock<ITokenProxyClient>(MockBehavior.Strict);
                services.RegisterSingleton(tokenProxyClientMock.Object);

                var secureStorageStub = new SecureStorageStub();
                services.RegisterSingleton<ISecureStorage>(secureStorageStub);

                this.testBase.AuthorizationCallbackUriProviderMock = authorizationCallbackUriProviderMock;
                this.testBase.AuthorizationInteractionServiceMock = authorizationInteractionServiceMock;
                this.testBase.CsfrTokenProviderMock = csrfTokenProviderMock;
                this.testBase.TokenProxyClientMock = tokenProxyClientMock;
                this.testBase.SecureStorage = secureStorageStub;
            }
        }

        protected class SecureStorageStub : ISecureStorage
        {
            private AuthenticationTicketStorageItem item;

            public (string AuthorizationKey, PrivateUser User) GetItem()
            {
                if (this.item == null)
                {
                    return (null, null);
                }
                else
                {
                    return (this.item.AuthorizationKey, this.item.User);
                }
            }

            public void SetItem((string AuthorizationKey, PrivateUser User) item)
            {
                this.item = new AuthenticationTicketStorageItem
                {
                    Version = 1,
                    AuthorizationKey = item.AuthorizationKey,
                    User = item.User
                };
            }

            Task ISecureStorage.RemoveAsync(CancellationToken cancellationToken)
            {
                this.item = null;

                return Task.FromResult(0);
            }

            Task ISecureStorage.SaveAsync(string value, CancellationToken cancellationToken)
            {
                this.item = JsonConvert.DeserializeObject<AuthenticationTicketStorageItem>(value);

                return Task.FromResult(0);
            }

            Task<(bool IsSuccess, string Value)> ISecureStorage.TryGetAsync(CancellationToken cancellationToken)
            {
                if (this.item != null)
                {
                    return Task.FromResult((true, JsonConvert.SerializeObject(this.item)));
                }
                else
                {
                    return Task.FromResult<(bool, string)>((false, null));
                }
            }
        }

        protected class FlowOptionsStub : IAuthorizationOptions, IUserClientOptions
        {
            public string ClientId { get; set; }

            public Uri AuthorizationEndpoint { get; set; }

            public IList<Scope> Scopes { get; set; }

            public IList<string> DynamicScopes { get; set; }

            public bool ShowDialog { get; set; }

            public Uri UserInformationEndpoint { get; set; }
        }
    }
}
