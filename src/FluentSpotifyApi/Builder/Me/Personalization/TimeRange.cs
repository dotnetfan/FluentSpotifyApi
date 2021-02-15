using System.Runtime.Serialization;

namespace FluentSpotifyApi.Builder.Me.Personalization
{
    /// <summary>
    /// The affinities time frame.
    /// </summary>
    public enum TimeRange
    {
        /// <summary>
        /// Calculated from several years of data and including all new data as it becomes available.
        /// </summary>
        [EnumMember(Value = "long_term")]
        LongTerm,

        /// <summary>
        /// Approximately last 6 months.
        /// </summary>
        [EnumMember(Value = "medium_term")]
        MediumTerm,

        /// <summary>
        /// Approximately last 4 weeks.
        /// </summary>
        [EnumMember(Value = "short_term")]
        ShortTerm,
    }
}
