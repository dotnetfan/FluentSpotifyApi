using System.Collections.Generic;

namespace FluentSpotifyApi.Builder.Me.Player
{
    internal static class Factory
    {
        private const string EndpointName = "player";

        public static IDevicesBuilder CreateDevicesBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix)
        {
            return new DevicesBuilder(contextData, routeValuesPrefix, EndpointName);
        }

        public static IActiveDevicePlaybackBuilder CreateActiveDevicePlaybackBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix)
        {
            return new PlaybackBuilder(contextData, routeValuesPrefix, EndpointName);
        }

        public static IDevicePlaybackBuilder CreateDevicePlaybackBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, string deviceId)
        {
            return new PlaybackBuilder(contextData, routeValuesPrefix, EndpointName, deviceId);
        }
    }
}
