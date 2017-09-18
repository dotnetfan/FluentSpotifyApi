using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Browse;

namespace FluentSpotifyApi.Builder.Browse
{
    internal class BrowseNewReleasesBuilder : BuilderBase, IBrowseNewReleasesBuilder
    {
        public BrowseNewReleasesBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix) : base(contextData, routeValuesPrefix, "new-releases")
        {
        }

        public Task<NewReleases> GetAsync(string country, int limit, int offset, CancellationToken cancellationToken)
        {
            return this.GetAsync<NewReleases>(cancellationToken, queryStringParameters: new { limit, offset }, optionalQueryStringParameters: new { country });
        }
    }
}
