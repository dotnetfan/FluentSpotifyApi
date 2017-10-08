using System;
using System.Linq.Expressions;

namespace FluentSpotifyApi.Expressions.Fields
{
    /// <summary>
    /// The fields builder.
    /// </summary>
    /// <typeparam name="TInput">The type of the input.</typeparam>
    public interface IFieldsBuilder<TInput>
    {
        /// <summary>
        /// Includes the specified expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expression">
        /// The expression to be included.
        /// The indexer operator can be used to include array properties (e.g. <c>f => f.Items[0].Name</c>).
        /// </param>
        /// <returns></returns>
        IFieldsBuilder<TInput> Include<TResult>(Expression<Func<TInput, TResult>> expression);

        /// <summary>
        /// Excludes the specified expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expression">
        /// The expression to be excluded.
        /// The indexer operator can be used to exclude array properties (e.g. <c>f => f.Items[0].Name</c>).
        /// </param>
        /// <returns></returns>
        IFieldsBuilder<TInput> Exclude<TResult>(Expression<Func<TInput, TResult>> expression);
    }
}
