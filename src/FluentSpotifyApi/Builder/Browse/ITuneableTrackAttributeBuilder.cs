namespace FluentSpotifyApi.Builder.Browse
{
    /// <summary>
    /// The tunable track attribute builder.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITuneableTrackAttributeBuilder<T>
    {
        /// <summary>
        /// Sets min value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        ITuneableTrackAttributeBuilder<T> Min(T value);

        /// <summary>
        /// Sets max value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        ITuneableTrackAttributeBuilder<T> Max(T value);

        /// <summary>
        /// Sets target value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        ITuneableTrackAttributeBuilder<T> Target(T value);
    }
}
