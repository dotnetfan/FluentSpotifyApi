namespace FluentSpotifyApi.Builder.Me.Player
{
    internal class DevicePlaybackBuilder : PlaybackBuilder, IDevicePlaybackBuilder
    {
        public DevicePlaybackBuilder(BuilderBase parent, string deviceId)
            : base(parent, deviceId)
        {
        }
    }
}
