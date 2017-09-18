using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Core.Options;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.User
{
    /// <summary>
    /// The HTTP client for getting information about current user.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.AuthorizationFlows.Core.Client.User.IUserHttpClient" />
    public class UserHttpClient : IUserHttpClient
    {
        private readonly IAuthorizationFlowsHttpClient authorizationFlowsHttpClient;

        private readonly IOptionsProvider<IUserClientOptions> userClientOptionsProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserHttpClient"/> class.
        /// </summary>
        /// <param name="authorizationFlowsHttpClient">The authorization flows HTTP client.</param>
        /// <param name="userClientOptionsProvider">The user client options provider.</param>
        public UserHttpClient(IAuthorizationFlowsHttpClient authorizationFlowsHttpClient, IOptionsProvider<IUserClientOptions> userClientOptionsProvider)
        {
            this.authorizationFlowsHttpClient = authorizationFlowsHttpClient;
            this.userClientOptionsProvider = userClientOptionsProvider;
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<PrivateUser> GetCurrentUserAsync(string accessToken, CancellationToken cancellationToken)
        {
            return this.authorizationFlowsHttpClient.SendAsync<PrivateUser>(                
                this.userClientOptionsProvider.Get().UserInformationEndpoint,
                HttpMethod.Get,
                null,
                null,
                new[] { new KeyValuePair<string, string>("Authorization", $"Bearer {accessToken}") },
                cancellationToken);
        }
    }
}
