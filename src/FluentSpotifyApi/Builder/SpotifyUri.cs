namespace FluentSpotifyApi.Builder
{
    internal static class SpotifyUri
    {
        public static string ForArtist(string artistId)
        {
            return $"spotify:artist:{artistId}";
        }

        public static string ForAlbum(string albumId)
        {
            return $"spotify:album:{albumId}";
        }

        public static string ForTrack(string trackId)
        {
            return $"spotify:track:{trackId}";
        }

        public static string ForPlaylist(string ownerId, string playlistId)
        {
            return $"spotify:user:{ownerId}:playlist:{playlistId}";
        }
    }
}
