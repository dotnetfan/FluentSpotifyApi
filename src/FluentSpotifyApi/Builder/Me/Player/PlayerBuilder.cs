using FluentSpotifyApi.Extensions;

namespace FluentSpotifyApi.Builder.Me.Player
{
    internal class PlayerBuilder : BuilderBase, IPlayerBuilder
    {
        public PlayerBuilder(BuilderBase parent)
            : base(parent, "player".Yield())
        {
        }

        public IRecentlyPlayedTracksBuilder RecentlyPlayedTracks => new RecentlyPlayedTracksBuilder(this);

        public IDevicesBuilder Devices => new DevicesBuilder(this);

        public IActiveDevicePlaybackBuilder Playback() => new ActiveDevicePlaybackBuilder(this);

        public IDevicePlaybackBuilder Playback(string deviceId) => new DevicePlaybackBuilder(this, deviceId);
    }
}
