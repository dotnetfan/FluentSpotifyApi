using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Me.Personalization.RecentlyPlayed
{
    internal class RecentlyPlayedTracksBuilder : BuilderBase, IRecentlyPlayedTracksBuilder
    {
        public RecentlyPlayedTracksBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix) : base(contextData, routeValuesPrefix, "player")
        {
        }

        public Task<CursorBasedPage<PlayHistory>> GetAsync(int limit, long? after, long? before, CancellationToken cancellationToken)
        {
            return this.GetAsync<CursorBasedPage<PlayHistory>>(
                cancellationToken, 
                queryStringParameters: new { limit },
                optionalQueryStringParameters: new { after, before }, 
                additionalRouteValues: new[] { "recently-played" });
        }
    }
}
