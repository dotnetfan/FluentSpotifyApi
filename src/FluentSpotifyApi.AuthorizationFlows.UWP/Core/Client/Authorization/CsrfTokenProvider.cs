using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;

namespace FluentSpotifyApi.AuthorizationFlows.UWP.Core.Client.Authorization
{
    internal class CsrfTokenProvider : ICsrfTokenProvider
    {
        public string Get()
        {
            var buffer = CryptographicBuffer.GenerateRandom(32);
            return GerUrlSafeBase64String(buffer);
        }

        private static string GerUrlSafeBase64String(IBuffer buffer)
        {
            return CryptographicBuffer.EncodeToBase64String(buffer).Replace("+", "-").Replace("/", "_").Replace("=", string.Empty);
        }
    }
}
