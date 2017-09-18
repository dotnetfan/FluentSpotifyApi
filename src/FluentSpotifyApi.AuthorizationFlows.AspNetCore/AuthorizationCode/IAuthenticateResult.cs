using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode
{
    internal interface IAuthenticateResult
    {
        ClaimsPrincipal Principal { get; }

        AuthenticationProperties Properties { get; }
    }
}
