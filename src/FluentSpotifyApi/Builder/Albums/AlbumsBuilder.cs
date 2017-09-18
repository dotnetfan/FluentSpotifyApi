using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Messages;

namespace FluentSpotifyApi.Builder.Albums
{
    internal class AlbumsBuilder : EntitiesBuilderBase, IAlbumsBuilder
    {
        public AlbumsBuilder(ContextData contextData, string endpointName, IEnumerable<string> ids) : base(contextData, endpointName, ids)
        {
        }

        public Task<FullAlbumsMessage> GetAsync(string market, CancellationToken cancellationToken)
        {
            return this.GetListAsync<FullAlbumsMessage>(cancellationToken, optionalQueryStringParameters: new { market });
        }
    }
}
