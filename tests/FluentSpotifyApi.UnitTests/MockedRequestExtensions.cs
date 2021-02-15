using System;
using System.Text.Json;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.UnitTests
{
    public static class MockedRequestExtensions
    {
        public static MockedRequest WithJsonContent(this MockedRequest mockedRequest, Func<JsonElement, bool> matcher)
        {
            mockedRequest.With(r =>
            {
                if (r.Content == null)
                {
                    return false;
                }

                if (r.Content.Headers.ContentType.MediaType != "application/json")
                {
                    return false;
                }

                var content = r.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                try
                {
                    using var document = JsonDocument.Parse(content);
                    return matcher(document.RootElement);
                }
                catch (JsonException)
                {
                    return false;
                }
            });

            return mockedRequest;
        }

        public static MockedRequest WithNullContent(this MockedRequest mockedRequest) => mockedRequest.With(r => r.Content == null);
    }
}