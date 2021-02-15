using System;
using System.Net.Http;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.UnitTests
{
    public static class MockHttpMessageHandlerExtensions
    {
        public static MockedRequest ExpectSpotifyRequest(this MockHttpMessageHandler handler, HttpMethod method, string relativeUri)
            => handler.Expect(method, new Uri(FluentSpotifyClientDefaults.WebApiEndpoint, relativeUri).AbsoluteUri);
    }
}
