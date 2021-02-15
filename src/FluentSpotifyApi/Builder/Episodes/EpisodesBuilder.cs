using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Episodes;

namespace FluentSpotifyApi.Builder.Episodes
{
    internal class EpisodesBuilder : EntitiesBuilderBase, IEpisodesBuilder
    {
        public EpisodesBuilder(RootBuilder root, IEnumerable<string> ids)
            : base(root, "episodes", ids)
        {
        }

        public Task<EpisodesResponse> GetAsync(string market, CancellationToken cancellationToken)
        {
            return this.GetListAsync<EpisodesResponse>(cancellationToken, queryParams: new { market });
        }
    }
}
