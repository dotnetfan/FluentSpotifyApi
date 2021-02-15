namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    /// <summary>
    /// The Spotify scopes.
    /// </summary>
    public static class SpotifyScopes
    {
        /// <summary>
        /// Read access to user's private playlists.
        /// </summary>
        public const string PlaylistReadPrivate = "playlist-read-private";

        /// <summary>
        /// Include collaborative playlists when requesting a user's playlists.
        /// </summary>
        public const string PlaylistReadCollaborative = "playlist-read-collaborative";

        /// <summary>
        /// Write access to a user's public playlists.
        /// </summary>
        public const string PlaylistModifyPublic = "playlist-modify-public";

        /// <summary>
        /// Write access to a user's private playlists.
        /// </summary>
        public const string PlaylistModifyPrivate = "playlist-modify-private";

        /// <summary>
        /// Write access to user-provided images.
        /// </summary>
        public const string UgcImageUpload = "ugc-image-upload";

        /// <summary>
        /// Read access to user’s email address.
        /// </summary>
        public const string UserReadEmail = "user-read-email";

        /// <summary>
        /// Read access to user’s subscription details (type of user account).
        /// </summary>
        public const string UserReadPrivate = "user-read-private";

        /// <summary>
        /// Read access to the list of artists and other users that the user follows.
        /// </summary>
        public const string UserFollowRead = "user-follow-read";

        /// <summary>
        /// Write/delete access to the list of artists and other users that the user follows.
        /// </summary>
        public const string UserFollowModify = "user-follow-modify";

        /// <summary>
        /// Read access to a user's "Your Music" library.
        /// </summary>
        public const string UserLibraryRead = "user-library-read";

        /// <summary>
        /// Write/delete access to a user's "Your Music" library.
        /// </summary>
        public const string UserLibraryModify = "user-library-modify";

        /// <summary>
        /// Read access to a user's top artists and tracks.
        /// </summary>
        public const string UserTopRead = "user-top-read";

        /// <summary>
        /// Read access to a user’s recently played tracks.
        /// </summary>
        public const string UserReadRecentlyPlayed = "user-read-recently-played";

        /// <summary>
        /// Read access to a user’s currently playing content.
        /// </summary>
        public const string UserReadCurrentlyPlaying = "user-read-currently-playing";

        /// <summary>
        /// Read access to a user’s player state.
        /// </summary>
        public const string UserReadPlaybackState = "user-read-playback-state";

        /// <summary>
        /// Read access to a user’s playback position in a content.
        /// </summary>
        public const string UserReadPlaybackPosition = "user-read-playback-position";

        /// <summary>
        /// Write access to a user’s playback state.
        /// </summary>
        public const string UserModifyPlaybackState = "user-modify-playback-state";

        /// <summary>
        /// Control playback of a Spotify track. This scope is currently available to the Web Playback SDK. The user must have a Spotify Premium account.
        /// </summary>
        public const string Streaming = "streaming";

        /// <summary>
        /// Remote control playback of Spotify. This scope is currently available to Spotify iOS and Android SDKs.
        /// </summary>
        public const string AppRemoteControl = "app-remote-control";
    }
}
