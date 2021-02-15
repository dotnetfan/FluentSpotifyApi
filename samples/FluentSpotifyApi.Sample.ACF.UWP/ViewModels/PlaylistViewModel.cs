using FluentSpotifyApi.Model.Playlists;

namespace FluentSpotifyApi.Sample.ACF.UWP.ViewModels
{
    public class PlaylistViewModel
    {
        private readonly SimplifiedPlaylist playlist;

        public PlaylistViewModel(SimplifiedPlaylist playlist, bool isOwned)
        {
            this.playlist = playlist;
            this.IsOwned = isOwned;
        }

        public string Id => this.playlist.Id;

        public string Name => this.playlist.Name;

        public string Owner => this.playlist.Owner?.DisplayName ?? this.playlist.Owner?.Id;

        public int? NumberOfTracks => this.playlist.Tracks?.Total;

        public bool? IsPublic => this.playlist.Public;

        public bool? IsCollaborative => this.playlist.Collaborative;

        public bool IsOwned { get; }
    }
}
