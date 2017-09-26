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

        protected class TrackUris
        {
            [JsonProperty(PropertyName = "uris")]
            public string[] Uris { get; set; }
        }

        protected class TrackUriWithPositions
        {
            [JsonProperty(PropertyName = "tracks")]
            public TrackUriWithPosition[] Tracks { get; set; }

            [JsonProperty(PropertyName = "snapshot_id", NullValueHandling = NullValueHandling.Ignore)]
            public string SnapshotId { get; set; }
        }

        protected class TrackUriWithPosition
        {
            [JsonProperty(PropertyName = "uri")]
            public string Uri { get; set; }

            [JsonProperty(PropertyName = "positions", NullValueHandling = NullValueHandling.Ignore)]
            public int[] Positions { get; set; }
        }
    }
}
