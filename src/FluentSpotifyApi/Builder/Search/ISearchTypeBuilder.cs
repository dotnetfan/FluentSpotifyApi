using System;
using System.Linq.Expressions;
using FluentSpotifyApi.Expressions.Query;

namespace FluentSpotifyApi.Builder.Search
{
    /// <summary>
    /// The builder for "search?type={entities}" endpoint.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearchTypeBuilder<T>
    {
        /// <summary>
        /// Gets builder for "search?type={entities}&amp;q={query}" endpoint.
        /// </summary>
        /// <param name="query">The query.</param>
        ISearchTypeQueryBuilder<T> Matching(string query);

        /// <summary>
        /// Gets builder for "search?type={entities}&amp;q={query}" endpoint.
        /// </summary>
        /// <param name="predicate">
        /// The predicate expression. The <c>==</c> operator is used for exact match while <see cref="String.Contains(string)"/> method is used for partial match. 
        /// The <c>&lt;=</c> and <c>&gt;=</c> operators can be used to specify <see cref="QueryFields.Year"/> range. Multiple statements can be combined using <c>&amp;&amp;</c> 
        /// or <c>||</c> operators. Since search query does not support precedence of operators, the operands and operators are just emitted from left to right.
        /// There is also a limited support for negation. The <c>!=</c> operator or <c>!</c><see cref="String.Contains(string)"/> method call can be used to negate match.
        /// To negate <see cref="QueryFields.Year"/> range, the range expression must be prefixed with <c>!</c> operator (e.g. <c>!(f.Year &gt;= 2000 &amp;&amp; f.Year &lt;= 2015)</c>). 
        /// The <see cref="QueryProvider.Get{TQueryFields}(Expression{Func{TQueryFields, bool}})"/> method can be used to get query in string format.
        /// </param>
        ISearchTypeQueryBuilder<T> Matching(Expression<Func<QueryFields, bool>> predicate);
    }
}
