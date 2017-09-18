using System.Collections.Generic;

namespace FluentSpotifyApi.Builder
{
    internal class SequenceBuilderBase<T> : BuilderBase
    {
        protected readonly IEnumerable<T> Sequence;

        public SequenceBuilderBase(ContextData contextData, string enpointName, IEnumerable<T> sequence) 
            : this(contextData, null, enpointName, sequence)
        {
        }

        public SequenceBuilderBase(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName, IEnumerable<T> sequence) 
            : base(contextData, routeValuesPrefix, endpointName)
        {
            this.Sequence = sequence;
        }
    }
}
