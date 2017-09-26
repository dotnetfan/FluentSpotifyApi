using System;
using System.Collections.Generic;
using System.Text;

namespace FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Exceptions
{
    /// <summary>
    /// The exception that is thrown when there is no session in storage when <see cref="Native.IAuthenticationManager.RestoreSessionAsync(System.Threading.CancellationToken)"/> is called.
    /// </summary>
    /// <seealso cref="Exception" />
    public class SessionNotFoundException : Exception
    {
    }
}
