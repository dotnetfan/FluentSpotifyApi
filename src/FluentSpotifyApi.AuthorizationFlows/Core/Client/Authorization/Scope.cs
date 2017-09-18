using System.ComponentModel;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    /// <summary>
    /// The permission enumeration.
    /// </summary>
    public enum Scope
    {
        /// <summary>
        /// The playlist-read-private permission.
        /// </summary>
        [Description("playlist-read-private")]
        PlaylistReadPrivate,

        /// <summary>
        /// The playlist-read-collaborative permission.
        /// </summary>
        [Description("playlist-read-collaborative")]
        PlaylistReadCollaborative,

        /// <summary>
        /// The playlist-modify-public permission.
        /// </summary>
        [Description("playlist-modify-public")]
        PlaylistModifyPublic,

        /// <summary>
        /// The playlist-modify-private permission.
        /// </summary>
        [Description("playlist-modify-private")]
        PlaylistModifyPrivate,

        /// <summary>
        /// The streaming permission.
        /// </summary>
        [Description("streaming")]
        Streaming,

        /// <summary>
        /// The ugc-image-upload permission.
        /// </summary>
        [Description("ugc-image-upload")]
        UgcImageUpload,

        /// <summary>
        /// The user-follow-modify permission.
        /// </summary>
        [Description("user-follow-modify")]
        UserFollowModify,

        /// <summary>
        /// The user-follow-read permission.
        /// </summary>
        [Description("user-follow-read")]
        UserFollowRead,

        /// <summary>
        /// The user-library-read permission.
        /// </summary>
        [Description("user-library-read")]
        UserLibraryRead,

        /// <summary>
        /// The user-library-modify permission.
        /// </summary>
        [Description("user-library-modify")]
        UserLibraryModify,

        /// <summary>
        /// The user-read-private permission.
        /// </summary>
        [Description("user-read-private")]
        UserReadPrivate,

        /// <summary>
        /// The user-read-birthdate permission.
        /// </summary>
        [Description("user-read-birthdate")]
        UserReadBirthdate,

        /// <summary>
        /// The user-read-email permission.
        /// </summary>
        [Description("user-read-email")]
        UserReadEmail,

        /// <summary>
        /// The user-top-read permission.
        /// </summary>
        [Description("user-top-read")]
        UserTopRead,

        /// <summary>
        /// The user-read-recently-played permission.
        /// </summary>
        [Description("user-read-recently-played")]
        UserReadRecentlyPlayed,        
    }
}
