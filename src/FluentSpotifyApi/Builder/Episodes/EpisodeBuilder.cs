using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Episodes;

namespace FluentSpotifyApi.Builder.Episodes
{
    internal class EpisodeBuilder : EntityBuilderBase, IEpisodeBuilder
    {
        public EpisodeBuilder(RootBuilder root, string id)
            : base(root, "episodes", id)
        {
        }

        public Task<Episode> GetAsync(string market, CancellationToken cancellationToken)
        {
            return this.GetAsync<Episode>(cancellationToken, queryParams: new { market });
        }
    }
}
