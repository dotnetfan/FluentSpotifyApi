using System.Collections.Generic;

namespace FluentSpotifyApi.Builder.Browse
{
    internal class TuneableTrackAttributeBuilder<T> : ITuneableTrackAttributeBuilder<T> where T : struct
    {
        private T? min;

        private T? max;

        private T? target;

        public ITuneableTrackAttributeBuilder<T> Min(T value)
        {
            this.min = value;
            return this;
        }

        public ITuneableTrackAttributeBuilder<T> Max(T value)
        {
            this.max = value;
            return this;
        }

        public ITuneableTrackAttributeBuilder<T> Target(T value)
        {
            this.target = value;
            return this;
        }

        public IEnumerable<KeyValuePair<string, object>> GetValues(string attributeName)
        {
            yield return new KeyValuePair<string, object>($"min_{attributeName}", this.min);
            yield return new KeyValuePair<string, object>($"max_{attributeName}", this.max);
            yield return new KeyValuePair<string, object>($"target_{attributeName}", this.target);
        }
    }
}
