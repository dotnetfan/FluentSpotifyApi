using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.Builder
{
    internal class EntityBuilderBase : BuilderBase
    {
        protected readonly string Id;

        protected EntityBuilderBase(BuilderBase parent, string entityName, string id)
            : base(parent, new[] { entityName, id })
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(entityName, nameof(entityName));
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(id, nameof(id));

            this.Id = id;
        }
    }
}
