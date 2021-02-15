using System;
using System.Collections.Generic;

namespace FluentSpotifyApi.Builder.Browse
{
    internal class TuneableTrackAttributeBuilder<TSource, TResult> : ITuneableTrackAttributeBuilder<TSource>
        where TSource : struct
    {
        private readonly Func<TSource, TResult> resultSelector;

        private TSource? min;

        private TSource? max;

        private TSource? target;

        public TuneableTrackAttributeBuilder(Func<TSource, TResult> resultSelector)
        {
            this.resultSelector = resultSelector;
        }

        public ITuneableTrackAttributeBuilder<TSource> Min(TSource value)
        {
            this.min = value;
            return this;
        }

        public ITuneableTrackAttributeBuilder<TSource> Max(TSource value)
        {
            this.max = value;
            return this;
        }

        public ITuneableTrackAttributeBuilder<TSource> Target(TSource value)
        {
            this.target = value;
            return this;
        }

        public IEnumerable<KeyValuePair<string, object>> GetValues(string attributeName)
        {
            if (this.min != null)
            {
                yield return new KeyValuePair<string, object>($"min_{attributeName}", this.resultSelector(this.min.Value));
            }

            if (this.max != null)
            {
                yield return new KeyValuePair<string, object>($"max_{attributeName}", this.resultSelector(this.max.Value));
            }

            if (this.target != null)
            {
                yield return new KeyValuePair<string, object>($"target_{attributeName}", this.resultSelector(this.target.Value));
            }
        }
    }
}
