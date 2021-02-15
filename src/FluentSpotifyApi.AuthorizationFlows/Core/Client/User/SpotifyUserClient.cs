using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.User
{
    internal class SpotifyUserClient : ISpotifyUserClient
    {
        private readonly ISpotifyUserHttpClientFactory httpClientFactory;

        public SpotifyUserClient(ISpotifyUserHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<PrivateUser> GetCurrentUserAsync(string accessToken, CancellationToken cancellationToken)
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Get;
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                return await SpotifyHttpUtils.HandleTimeoutAsync<ISpotifyUserClient, PrivateUser>(
                    async innerCt =>
                    {
                        using (var response = await this.httpClientFactory.CreateClient().SendAsync(request, innerCt).ConfigureAwait(false))
                        {
                            return await response.Content.ReadFromJsonAsync<PrivateUser>(cancellationToken: innerCt).ConfigureAwait(false);
                        }
                    },
                    cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
