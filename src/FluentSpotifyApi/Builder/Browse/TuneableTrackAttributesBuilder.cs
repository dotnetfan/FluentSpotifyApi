using System;
using System.Collections.Generic;
using System.Linq;
using FluentSpotifyApi.Extensions;

namespace FluentSpotifyApi.Builder.Browse
{
    internal class TuneableTrackAttributesBuilder : ITuneableTrackAttributesBuilder
    {
        private readonly IDictionary<string, IList<KeyValuePair<string, object>>> attributes = new Dictionary<string, IList<KeyValuePair<string, object>>>();

        public ITuneableTrackAttributesBuilder Acousticness(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute)
        {
            return this.Attribute("acousticness", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Danceability(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute)
        {
            return this.Attribute("danceability", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Duration(Action<ITuneableTrackAttributeBuilder<TimeSpan>> buildAttribute)
        {
            return this.Attribute("duration_ms", buildAttribute, v => v.ToWholeMilliseconds());
        }

        public ITuneableTrackAttributesBuilder Energy(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute)
        {
            return this.Attribute("energy", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Instrumentalness(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute)
        {
            return this.Attribute("instrumentalness", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Key(Action<ITuneableTrackAttributeBuilder<int>> buildAttribute)
        {
            return this.Attribute("key", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Liveness(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute)
        {
            return this.Attribute("liveness", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Loudness(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute)
        {
            return this.Attribute("loudness", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Mode(Action<ITuneableTrackAttributeBuilder<int>> buildAttribute)
        {
            return this.Attribute("mode", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Popularity(Action<ITuneableTrackAttributeBuilder<int>> buildAttribute)
        {
            return this.Attribute("popularity", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Speechiness(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute)
        {
            return this.Attribute("speechiness", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Tempo(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute)
        {
            return this.Attribute("tempo", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder TimeSignature(Action<ITuneableTrackAttributeBuilder<int>> buildAttribute)
        {
            return this.Attribute("time_signature", buildAttribute);
        }

        public ITuneableTrackAttributesBuilder Valence(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute)
        {
            return this.Attribute("valence", buildAttribute);
        }

        public IList<KeyValuePair<string, object>> GetAttributes()
        {
            return this.attributes.SelectMany(item => item.Value).ToList();
        }

        private ITuneableTrackAttributesBuilder Attribute<T>(string attributeName, Action<ITuneableTrackAttributeBuilder<T>> buildAttribute)
            where T : struct
            => this.Attribute<T, T>(attributeName, buildAttribute, v => v);

        private ITuneableTrackAttributesBuilder Attribute<T, TResult>(string attributeName, Action<ITuneableTrackAttributeBuilder<T>> buildAttribute, Func<T, TResult> resultSelector)
            where T : struct
        {
            var attributeBuilder = new TuneableTrackAttributeBuilder<T, TResult>(resultSelector);
            buildAttribute?.Invoke(attributeBuilder);

            this.attributes[attributeName] = attributeBuilder.GetValues(attributeName).ToList();

            return this;
        }
    }
}
