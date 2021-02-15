using System;

namespace FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode.Exceptions
{
    /// <summary>
    /// The exception that is thrown when there is no session in storage when <see cref="IAuthenticationManager.RestoreSessionAsync(System.Threading.CancellationToken)"/> is called.
    /// </summary>
    /// <seealso cref="Exception" />
    public class SpotifySessionNotFoundException : Exception
    {
    }
}
