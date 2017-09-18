using FluentSpotifyApi.Core.Client;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client
{
    /// <summary>
    /// The HTTP client used in authorization flows.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Core.Client.ITypedHttpClient" />
    public interface IAuthorizationFlowsHttpClient : ITypedHttpClient
    {
    }
}
