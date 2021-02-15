using System;

namespace FluentSpotifyApi.Expressions.Query
{
    /// <summary>
    /// Indicates that the property or field is a query field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class QueryFieldAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryFieldAttribute" /> class.
        /// </summary>
        /// <param name="name">The query field name.</param>
        public QueryFieldAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// The query field name.
        /// </summary>
        public string Name { get; }
    }
}
