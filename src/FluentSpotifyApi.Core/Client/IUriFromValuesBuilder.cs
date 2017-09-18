using System;

namespace FluentSpotifyApi.Core.Client
{
    /// <summary>
    /// The URI from values builder.
    /// </summary>
    public interface IUriFromValuesBuilder
    {
        /// <summary>
        /// Adds the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        IUriFromValuesBuilder AddValue<T>(T value);

        /// <summary>
        /// Builds URI using values provided by <see cref="AddValue{T}(T)"/>.
        /// </summary>
        /// <returns></returns>
        Uri Build();
    }
}
