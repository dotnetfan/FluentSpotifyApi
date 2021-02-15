using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token.Exceptions;
using FluentSpotifyApi.Core.HttpHandlers;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token.Handlers
{
    internal class SpotifyAuthenticationErrorHandler : SpotifyErrorHandler<ISpotifyTokenClient, AuthenticationError, AuthenticationError>
    {
        public SpotifyAuthenticationErrorHandler()
            : base(
                  r => r,
                  (t, s, c, e) => new SpotifyAuthenticationErrorException(s, c, e))
        {
        }
    }
}
