using FluentSpotifyApi.Core.Client;
using FluentSpotifyApi.Exceptions;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The base player class.
    /// </summary>
    [HttpStatusCodeToException(StatusCode = System.Net.HttpStatusCode.Accepted, ExceptionType = typeof(SpotifyDeviceUnavailableException))]
    public class PlayerBase
    {
    }
}
