using System;

namespace FluentSpotifyApi.Builder.Browse
{
    /// <summary>
    /// The tunable track attributes builder.
    /// </summary>
    public interface ITuneableTrackAttributesBuilder
    {
        /// <summary>
        /// Sets acousticnesses.
        /// </summary>
        /// <param name="buildAttribute">The build attribute action.</param>
        /// <returns></returns>
        ITuneableTrackAttributesBuilder Acousticness(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute);

        /// <summary>
        /// Sets danceability.
        /// </summary>
        /// <param name="buildAttribute">The build attribute action.</param>
        /// <returns></returns>
        ITuneableTrackAttributesBuilder Danceability(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute);

        /// <summary>
        /// Sets duration in milliseconds.
        /// </summary>
        /// <param name="buildAttribute">The build attribute action.</param>
        /// <returns></returns>
        ITuneableTrackAttributesBuilder Duration(Action<ITuneableTrackAttributeBuilder<TimeSpan>> buildAttribute);

        /// <summary>
        /// Sets energy.
        /// </summary>
        /// <param name="buildAttribute">The build attribute action.</param>
        /// <returns></returns>
        ITuneableTrackAttributesBuilder Energy(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute);

        /// <summary>
        /// Sets instrumentalness.
        /// </summary>
        /// <param name="buildAttribute">The build attribute action.</param>
        /// <returns></returns>
        ITuneableTrackAttributesBuilder Instrumentalness(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute);

        /// <summary>
        /// Sets key.
        /// </summary>
        /// <param name="buildAttribute">The build attribute action.</param>
        /// <returns></returns>
        ITuneableTrackAttributesBuilder Key(Action<ITuneableTrackAttributeBuilder<int>> buildAttribute);

        /// <summary>
        /// Sets liveness.
        /// </summary>
        /// <param name="buildAttribute">The build attribute action.</param>
        /// <returns></returns>
        ITuneableTrackAttributesBuilder Liveness(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute);

        /// <summary>
        /// Sets loudness.
        /// </summary>
        /// <param name="buildAttribute">The build attribute action.</param>
        /// <returns></returns>
        ITuneableTrackAttributesBuilder Loudness(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute);

        /// <summary>
        /// Sets mode.
        /// </summary>
        /// <param name="buildAttribute">The build attribute action.</param>
        /// <returns></returns>
        ITuneableTrackAttributesBuilder Mode(Action<ITuneableTrackAttributeBuilder<int>> buildAttribute);

        /// <summary>
        /// Sets popularity.
        /// </summary>
        /// <param name="buildAttribute">The build attribute action.</param>
        /// <returns></returns>
        ITuneableTrackAttributesBuilder Popularity(Action<ITuneableTrackAttributeBuilder<int>> buildAttribute);

        /// <summary>
        /// Sets speechiness.
        /// </summary>
        /// <param name="buildAttribute">The build attribute action.</param>
        /// <returns></returns>
        ITuneableTrackAttributesBuilder Speechiness(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute);

        /// <summary>
        /// Sets tempo.
        /// </summary>
        /// <param name="buildAttribute">The build attribute action.</param>
        /// <returns></returns>
        ITuneableTrackAttributesBuilder Tempo(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute);

        /// <summary>
        /// Sets time signature.
        /// </summary>
        /// <param name="buildAttribute">The build attribute action.</param>
        /// <returns></returns>
        ITuneableTrackAttributesBuilder TimeSignature(Action<ITuneableTrackAttributeBuilder<int>> buildAttribute);

        /// <summary>
        /// Sets valence.
        /// </summary>
        /// <param name="buildAttribute">The build attribute action.</param>
        /// <returns></returns>
        ITuneableTrackAttributesBuilder Valence(Action<ITuneableTrackAttributeBuilder<float>> buildAttribute);
    }
}
