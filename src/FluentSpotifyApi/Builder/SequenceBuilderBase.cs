using System.Collections.Generic;
using FluentSpotifyApi.Core.Utils;
using FluentSpotifyApi.Extensions;

namespace FluentSpotifyApi.Builder
{
    internal class SequenceBuilderBase<T> : BuilderBase
    {
        protected readonly IEnumerable<T> Sequence;

        public SequenceBuilderBase(BuilderBase parent, string endpointName, IEnumerable<T> sequence)
            : base(parent, endpointName.Yield())
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(endpointName, nameof(endpointName));
            SpotifyArgumentAssertUtils.ThrowIfNull(sequence, nameof(sequence));

            this.Sequence = sequence;
        }
    }
}
