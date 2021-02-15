using FluentSpotifyApi.Expressions.Query;

namespace FluentSpotifyApi.Builder.Search
{
    /// <summary>
    /// The search query fields.
    /// </summary>
    public class QueryFields
    {
        /// <summary>
        /// Represents any field.
        /// </summary>
        public string Any { get; }

        /// <summary>
        /// The album field.
        /// </summary>
        [QueryField("album")]
        public string Album { get; }

        /// <summary>
        /// The artist field.
        /// </summary>
        [QueryField("artist")]
        public string Artist { get; }

        /// <summary>
        /// The track field.
        /// </summary>
        [QueryField("track")]
        public string Track { get; }

        /// <summary>
        /// The year field.
        /// </summary>
        [QueryField("year")]
        public int Year { get; }

        /// <summary>
        /// The tag field.
        /// </summary>
        [QueryField("tag")]
        public Tag Tag { get; }

        /// <summary>
        /// The genre field.
        /// </summary>
        [QueryField("genre")]
        public string Genre { get; }

        /// <summary>
        /// The UPC field.
        /// </summary>
        [QueryField("upc")]
        public string Upc { get; }

        /// <summary>
        /// The ISRC field.
        /// </summary>
        [QueryField("isrc")]
        public string Isrc { get; }
    }
}
