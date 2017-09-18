using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Albums
{
    internal class AlbumBuilder : EntityBuilderBase, IAlbumBuilder
    {
        public AlbumBuilder(ContextData contextData, string endpointName, string id) : base(contextData, endpointName, id)
        {
        }

        public IAlbumTracksBuilder Tracks => new AlbumTracksBuilder(this.ContextData, this.RouteValuesPrefix);

        public Task<FullAlbum> GetAsync(string market, CancellationToken cancellationToken)
        {
            return this.GetAsync<FullAlbum>(cancellationToken, optionalQueryStringParameters: new { market });
        }
    }
}
