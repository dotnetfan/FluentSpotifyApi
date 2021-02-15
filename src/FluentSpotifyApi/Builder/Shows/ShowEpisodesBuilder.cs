using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Episodes;

namespace FluentSpotifyApi.Builder.Shows
{
    internal class ShowEpisodesBuilder : BuilderBase, IShowEpisodesBuilder
    {
        public ShowEpisodesBuilder(BuilderBase parent)
            : base(parent, "episodes".Yield())
        {
        }

        public Task<Page<SimplifiedEpisode>> GetAsync(string market, int? limit, int? offset, CancellationToken cancellationToken)
        {
            return this.GetAsync<Page<SimplifiedEpisode>>(cancellationToken, queryParams: new { market, limit, offset });
        }
    }
}
