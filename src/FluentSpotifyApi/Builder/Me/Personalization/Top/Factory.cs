using System.Collections.Generic;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Me.Personalization.Top
{
    internal static class Factory
    {
        public static ITopBuilder<FullArtist> CreateTopArtistsBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix)
        {
            return new TopBuilder<FullArtist>(contextData, routeValuesPrefix, "artists");
        }

        public static ITopBuilder<FullTrack> CreateTopTracksBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix)
        {
            return new TopBuilder<FullTrack>(contextData, routeValuesPrefix, "tracks");
        }
    }
}
