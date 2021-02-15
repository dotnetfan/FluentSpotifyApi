using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Shows;

namespace FluentSpotifyApi.Builder.Shows
{
    internal class ShowsBuilder : EntitiesBuilderBase, IShowsBuilder
    {
        public ShowsBuilder(RootBuilder root, IEnumerable<string> ids)
            : base(root, "shows", ids)
        {
        }

        public Task<SimplifiedShowsResponse> GetAsync(string market, CancellationToken cancellationToken)
        {
            return this.GetListAsync<SimplifiedShowsResponse>(cancellationToken, queryParams: new { market });
        }
    }
}
