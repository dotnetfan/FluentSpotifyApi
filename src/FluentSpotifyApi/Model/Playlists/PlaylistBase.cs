using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Playlists
{
    /// <summary>
    /// The base class for playlists.
    /// </summary>
    /// <seealso cref="EntityBase"/>
    public abstract class PlaylistBase : EntityBase
    {
        /// <summary>
        /// <c>true</c> if the owner allows other users to modify the playlist.
        /// </summary>
        [JsonPropertyName("collaborative")]
        public bool Collaborative { get; set; }

        /// <summary>
        /// The playlist description. Only returned for modified, verified playlists, otherwise <c>null</c>.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Images for the playlist. The array may be empty or contain up to three images. The images are returned by size in descending order.
        /// Note: If returned, the source URL for the image (<see cref="Image.Url"/>) is temporary and will expire in less than a day.
        /// </summary>
        [JsonPropertyName("images")]
        public Image[] Images { get; set; }

        /// <summary>
        /// The name of the playlist.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The user who owns the playlist.
        /// </summary>
        [JsonPropertyName("owner")]
        public PublicUser Owner { get; set; }

        /// <summary>
        /// The playlist’s public/private status: <c>true</c> the playlist is public, <c>false</c> the playlist is private, <c>null</c> the playlist status is not relevant.
        /// </summary>
        [JsonPropertyName("public")]
        public bool? Public { get; set; }

        /// <summary>
        /// The version identifier for the current playlist. Can be supplied in other requests to target a specific playlist version.
        /// </summary>
        [JsonPropertyName("snapshot_id")]
        public string SnapshotId { get; set; }
    }
}
