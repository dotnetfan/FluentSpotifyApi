using System;

namespace FluentSpotifyApi.Core.Internal
{
    /// <summary>
    /// A strongly typed transformer implementation.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <seealso cref="FluentSpotifyApi.Core.Internal.ITransformer" />
    public class Transformer<TSource> : ITransformer
    {
        private readonly Func<TSource, object> transformer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Transformer{TSource}"/> class.
        /// </summary>
        /// <param name="transformer">The transformer.</param>
        public Transformer(Func<TSource, object> transformer)
        {
            this.transformer = transformer;
        }

        /// <summary>
        /// Gets the type of the source.
        /// </summary>
        /// <value>
        /// The type of the source.
        /// </value>
        public Type SourceType => typeof(TSource);

        /// <summary>
        /// Transforms the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public object Transform(object value)
        {
            return this.transformer((TSource)value);
        }
    }
}
