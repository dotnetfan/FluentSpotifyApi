using System.Collections.Generic;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Builder.User.Playlists.Tracks
{
    internal class PlaylistTrackSequenceBuilderBase<T> : SequenceBuilderBase<T>
    {
        public PlaylistTrackSequenceBuilderBase(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName, IEnumerable<T> sequence) 
            : base(contextData, routeValuesPrefix, endpointName, sequence)
        {
        }

        protected static string GetTrackUrl(string url)
        {
            return $"spotify:track:{url}";
        }

        protected class TrackUrls
        {
            [JsonProperty(PropertyName = "uris")]
            public string[] Uris { get; set; }
        }

        protected class TrackUrlWithPositions
        {
            [JsonProperty(PropertyName = "tracks")]
            public TrackUrlWithPosition[] Tracks { get; set; }

            [JsonProperty(PropertyName = "snapshot_id", NullValueHandling = NullValueHandling.Ignore)]
            public string SnapshotId { get; set; }
        }

        protected class TrackUrlWithPosition
        {
            [JsonProperty(PropertyName = "uri")]
            public string Uri { get; set; }

            [JsonProperty(PropertyName = "positions", NullValueHandling = NullValueHandling.Ignore)]
            public int[] Positions { get; set; }
        }
    }
}
