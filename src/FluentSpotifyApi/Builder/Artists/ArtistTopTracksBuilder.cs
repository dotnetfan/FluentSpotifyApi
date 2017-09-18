using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Messages;

namespace FluentSpotifyApi.Builder.Artists
{
    internal class ArtistTopTracksBuilder : BuilderBase, IArtistTopTracksBuilder
    {
        public ArtistTopTracksBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix) : base(contextData, routeValuesPrefix, "top-tracks")
        {
        }

        public Task<FullTracksMessage> GetAsync(string country, CancellationToken cancellationToken)
        {
            return this.GetAsync<FullTracksMessage>(cancellationToken, queryStringParameters: new { country });
        }
    }
}
