using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Model.Browse;

namespace FluentSpotifyApi.Builder.Browse
{
    internal class BrowseNewReleasesBuilder : BuilderBase, IBrowseNewReleasesBuilder
    {
        public BrowseNewReleasesBuilder(BuilderBase parent)
            : base(parent, "new-releases".Yield())
        {
        }

        public Task<NewReleases> GetAsync(string country, int? limit, int? offset, CancellationToken cancellationToken)
        {
            return this.GetAsync<NewReleases>(cancellationToken, queryParams: new { country, limit, offset });
        }
    }
}
