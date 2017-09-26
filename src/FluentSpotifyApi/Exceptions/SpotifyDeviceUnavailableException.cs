using System.Net;
using FluentSpotifyApi.Core.Exceptions;

namespace FluentSpotifyApi.Exceptions
{
    /// <summary>
    /// The exception that is thrown when <see cref="HttpStatusCode.Accepted"/> is returned from Web API Connect requests 
    /// (i.e. methods that are accessible via <see cref="Builder.Me.IMeBuilder.Player"/> property).
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Core.Exceptions.SpotifyServiceException" />
    public class SpotifyDeviceUnavailableException : SpotifyServiceException
    {
    }
}
