using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Shows;

namespace FluentSpotifyApi.Builder.Shows
{
    internal class ShowBuilder : EntityBuilderBase, IShowBuilder
    {
        public ShowBuilder(RootBuilder root, string id)
            : base(root, "shows", id)
        {
        }

        public IShowEpisodesBuilder Episodes => new ShowEpisodesBuilder(this);

        public Task<Show> GetAsync(string market, CancellationToken cancellationToken)
        {
            return this.GetAsync<Show>(cancellationToken, queryParams: new { market });
        }
    }
}
