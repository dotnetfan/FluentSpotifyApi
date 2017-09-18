using FluentSpotifyApi.Core.Client;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client
{
    /// <summary>
    /// The HTTP client used in authorization flows.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Core.Client.TypedHttpClient" />
    /// <seealso cref="FluentSpotifyApi.AuthorizationFlows.Core.Client.IAuthorizationFlowsHttpClient" />
    public class AuthorizationFlowsHttpClient : TypedHttpClient, IAuthorizationFlowsHttpClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationFlowsHttpClient"/> class.
        /// </summary>
        /// <param name="httpClientWrapper">The HTTP client wrapper.</param>
        public AuthorizationFlowsHttpClient(IHttpClientWrapper httpClientWrapper) : base(httpClientWrapper)
        {
        }
    }
}
