using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentSpotifyApi.Builder.Browse
{
    internal class TuneableTrackAttributesBuilder : ITuneableTrackAttributesBuilder
    {
        private IDictionary<string, IList<KeyValuePair<string, object>>> attributes = new Dictionary<string, IList<KeyValuePair<string, object>>>();

        public ITuneableTrackAttributesBuilder Acousticness(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute)
        {
            return this.DynamicAttribute("acousticness", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Danceability(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute)
        {
            return this.DynamicAttribute("danceability", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder DurationMs(Action<ITuneableTrackAttributeBuilder<int>> buildAttribute)
        {
            return this.DynamicAttribute("duration_ms", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Energy(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute)
        {
            return this.DynamicAttribute("energy", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Instrumentalness(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute)
        {
            return this.DynamicAttribute("instrumentalness", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Key(Action<ITuneableTrackAttributeBuilder<int>> buildAttribute)
        {
            return this.DynamicAttribute("key", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Liveness(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute)
        {
            return this.DynamicAttribute("liveness", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Loudness(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute)
        {
            return this.DynamicAttribute("loudness", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Mode(Action<ITuneableTrackAttributeBuilder<int>> buildAttribute)
        {
            return this.DynamicAttribute("mode", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Popularity(Action<ITuneableTrackAttributeBuilder<int>> buildAttribute)
        {
            return this.DynamicAttribute("popularity", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Speechiness(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute)
        {
            return this.DynamicAttribute("speechiness", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Tempo(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute)
        {
            return this.DynamicAttribute("tempo", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder TimeSignature(Action<ITuneableTrackAttributeBuilder<int>> buildAttribute)
        {
            return this.DynamicAttribute("time_signature", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Valence(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute)
        {
            return this.DynamicAttribute("valence", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder DynamicAttribute<T>(string attributeName, Action<ITuneableTrackAttributeBuilder<T>> buildAttribute) where T : struct
        {
            var attributeBuilder = new TuneableTrackAttributeBuilder<T>();
            buildAttribute?.Invoke(attributeBuilder);

            this.attributes[attributeName] = attributeBuilder.GetValues(attributeName).ToList();

            return this;
        }

        public IList<KeyValuePair<string, object>> GetAttributes()
        {
            return this.attributes.SelectMany(item => item.Value).ToList();
        }
    }
}
