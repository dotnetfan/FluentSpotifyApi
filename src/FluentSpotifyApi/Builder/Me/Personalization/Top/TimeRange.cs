using System.ComponentModel;

namespace FluentSpotifyApi.Builder.Me.Personalization.Top
{
    /// <summary>
    /// The affinities time frame.
    /// </summary>
    public enum TimeRange
    {
        /// <summary>
        /// Calculated from several years of data and including all new data as it becomes available.
        /// </summary>
        [Description("long_term")]
        LongTerm,

        /// <summary>
        /// Approximately last 6 months.
        /// </summary>
        [Description("medium_term")]
        MediumTerm,

        /// <summary>
        /// Approximately last 4 weeks.
        /// </summary>
        [Description("short_term")]
        ShortTerm,
    }
}
