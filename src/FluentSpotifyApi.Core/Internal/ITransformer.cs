using System;

namespace FluentSpotifyApi.Core.Internal
{
    /// <summary>
    /// A weakly typed transformer interface.
    /// </summary>
    public interface ITransformer
    {
        /// <summary>
        /// Gets the type of the source.
        /// </summary>
        /// <value>
        /// The type of the source.
        /// </value>
        Type SourceType { get; }

        /// <summary>
        /// Transforms the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        object Transform(object value);
    }
}
