using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Messages;

namespace FluentSpotifyApi.Builder.Browse
{
    internal class BrowseCategoryPlaylistsBuilder : BuilderBase, IBrowseCategoryPlaylistsBuilder
    {
        public BrowseCategoryPlaylistsBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix) : base(contextData, routeValuesPrefix, "playlists")
        {
        }

        public Task<SimplePlaylistsPageMessage> GetAsync(string country, int limit, int offset, CancellationToken cancellationToken)
        {
            return this.GetAsync<SimplePlaylistsPageMessage>(cancellationToken, queryStringParameters: new { limit, offset }, optionalQueryStringParameters: new { country });
        }
    }
}
