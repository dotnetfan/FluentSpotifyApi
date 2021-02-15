using System.Security.Cryptography;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    internal class CodeVerifierProvider : ICodeVerifierProvider
    {
        private static readonly char[] Characters =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '_'
        };

        public string Get() => this.GetInner(128);

        private string GetInner(int length)
        {
            char[] code = new char[length];
            byte[] secureBytes = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(secureBytes);
            }

            for (var i = 0; i < code.Length; i++)
            {
                int charIndex = secureBytes[i] % Characters.Length;
                code[i] = Characters[charIndex];
            }

            return new string(code);
        }
    }
}
