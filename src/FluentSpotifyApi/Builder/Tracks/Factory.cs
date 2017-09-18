using System.Collections.Generic;

namespace FluentSpotifyApi.Builder.Tracks
{
    internal static class Factory
    {
        private const string EndpointName = "tracks";

        public static ITrackBuilder CreateTrackBuilder(ContextData contextData, string id)
        {
            return new TrackBuilder(contextData, EndpointName, id);
        }

        public static ITracksBuilder CreateTracksBuilder(ContextData contextData, IEnumerable<string> ids)
        {
            return new TracksBuilder(contextData, EndpointName, ids);
        }
    }
}
