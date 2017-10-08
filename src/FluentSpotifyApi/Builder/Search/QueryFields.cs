using System.ComponentModel;

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
        [Description("album")]
        public string Album { get; }

        /// <summary>
        /// The artist field.
        /// </summary>
        [Description("artist")]
        public string Artist { get; }

        /// <summary>
        /// The track field.
        /// </summary>
        [Description("track")]
        public string Track { get; }

        /// <summary>
        /// The year field.
        /// </summary>
        [Description("year")]
        public int Year { get; }

        /// <summary>
        /// The tag field.
        /// </summary>
        [Description("tag")]
        public Tag Tag { get; }

        /// <summary>
        /// The genre field.
        /// </summary>
        [Description("genre")]
        public string Genre { get; }

        /// <summary>
        /// The UPC field.
        /// </summary>
        [Description("upc")]
        public string Upc { get; }

        /// <summary>
        /// The ISRC field.
        /// </summary>
        [Description("isrc")]
        public string Isrc { get; }
    }
}
