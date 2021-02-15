using System.Security.Cryptography;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    internal class CsrfTokenProvider : ICsrfTokenProvider
    {
        public string Get()
        {
            var bytes = new byte[32];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return SpotifyUriUtils.ConvertToBase64UriString(bytes);
        }
    }
}
