using System.Net.Http;
using FluentSpotifyApi.Core.Internal;

namespace FluentSpotifyApi
{
    internal class HttpClientRegistrationWrapper : Wrapper<HttpClient>
    {
        public HttpClientRegistrationWrapper(HttpClient value, bool isOwned) : base(value, isOwned)
        {
        }
    }
}
