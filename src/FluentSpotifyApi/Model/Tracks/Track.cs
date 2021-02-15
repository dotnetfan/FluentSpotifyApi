using System.Collections.Generic;
using System.Text.Json.Serialization;
using FluentSpotifyApi.Model.Albums;

namespace FluentSpotifyApi.Model.Tracks
{
    /// <summary>
    /// The track.
    /// </summary>
    /// <seealso cref="TrackBase" />
    public class Track : TrackBase
    {
        /// <summary>
        /// The album on which the track appears. The album object includes a link in href to full information about the album.
        /// </summary>
        [JsonPropertyName("album")]
        public SimplifiedAlbum Album { get; set; }

        /// <summary>
        /// Known external IDs for the track.
        /// </summary>
        [JsonPropertyName("external_ids")]
        public IDictionary<string, string> ExternalIds { get; set; }

        /// <summary>
        /// The popularity of a track is a value between 0 and 100, with 100 being the most popular.
        /// The popularity is calculated by algorithm and is based, in the most part, on the total number of plays the track has had and how recent those plays are.
        /// Generally speaking, songs that are being played a lot now will have a higher popularity than songs that were played a lot in the past.
        /// Duplicate tracks (e.g.the same track from a single and an album) are rated independently. Artist and album popularity is derived mathematically from track popularity.
        /// Note that the popularity value may lag actual popularity by a few days: the value is not updated in real time.
        /// </summary>
        [JsonPropertyName("popularity")]
        public int Popularity { get; set; }
    }
}
