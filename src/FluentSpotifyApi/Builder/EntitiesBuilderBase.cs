using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Extensions;

namespace FluentSpotifyApi.Builder
{
    internal class EntitiesBuilderBase : SequenceBuilderBase<string>
    {
        public EntitiesBuilderBase(BuilderBase parent, string entityName, IEnumerable<string> ids)
            : base(parent, entityName, ids)
        {
        }

        protected Task<T> GetListAsync<T>(
            CancellationToken cancellationToken,
            IEnumerable<object> additionalRouteValues = null,
            object queryParams = null)
        {
            return this.GetAsync<T>(
                cancellationToken,
                additionalRouteValues: additionalRouteValues,
                queryParams: new { ids = this.Sequence.JoinWithComma(), originalParams = queryParams });
        }
    }
}
