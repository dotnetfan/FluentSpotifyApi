using System.Text.Json.Serialization;

namespace FluentSpotifyApi.Model.Albums
{
    /// <summary>
    /// The simplified album.
    /// </summary>
    /// <seealso cref="AlbumBase" />
    public class SimplifiedAlbum : AlbumBase
    {
        /// <summary>
        /// The field is present when getting an artist’s albums. Possible values are “album”, “single”, “compilation”, “appears_on”.
        /// Compare to album_type this field represents relationship between the artist and the album.
        /// </summary>
        [JsonPropertyName("album_group")]
        public string AlbumGroup { get; set; }
    }
}
