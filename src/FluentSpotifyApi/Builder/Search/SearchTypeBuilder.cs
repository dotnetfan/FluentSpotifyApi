using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentSpotifyApi.Expressions.Query;

namespace FluentSpotifyApi.Builder.Search
{
    internal class SearchTypeBuilder<T> : BuilderBase, ISearchTypeBuilder<T>
    {
        private readonly IList<Entity> entities;

        public SearchTypeBuilder(BuilderBase parent, IEnumerable<Entity> entities)
            : base(parent)
        {
            this.entities = entities.ToList();
        }

        public ISearchTypeQueryBuilder<T> Matching(string query)
        {
            return new SearchTypeQueryBuilder<T>(this, this.entities, query);
        }

        public ISearchTypeQueryBuilder<T> Matching(Expression<Func<QueryFields, bool>> predicate)
        {
            return new SearchTypeQueryBuilder<T>(this, this.entities, QueryProvider.Get(predicate));
        }

        public ISearchTypeQueryBuilder<T> Matching(Expression<Func<QueryFields, bool>> predicate, QueryOptions queryOptions)
        {
            return new SearchTypeQueryBuilder<T>(this, this.entities, QueryProvider.Get(predicate, queryOptions));
        }
    }
}
