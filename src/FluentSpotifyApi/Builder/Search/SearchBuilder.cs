using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Internal.Extensions;
using FluentSpotifyApi.Expressions.Query;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Messages;

namespace FluentSpotifyApi.Builder.Search
{
    internal class SearchBuilder : BuilderBase, ISearchBuilder
    {
        public SearchBuilder(ContextData contextData) : base(contextData, "search")
        {
        }

        public ISearchTypeBuilder<SimpleAlbumsPageMessage> Albums => new SearchTypeBuilder<SimpleAlbumsPageMessage>(this, Entity.Album);

        public ISearchTypeBuilder<FullArtistsPageMessage> Artists => new SearchTypeBuilder<FullArtistsPageMessage>(this, Entity.Artist);

        public ISearchTypeBuilder<SimplePlaylistsPageMessage> Playlists => new SearchTypeBuilder<SimplePlaylistsPageMessage>(this, Entity.Playlist);

        public ISearchTypeBuilder<FullTracksPageMessage> Tracks => new SearchTypeBuilder<FullTracksPageMessage>(this, Entity.Track);

        public ISearchTypeBuilder<SearchResult> Entities(params Entity[] entities)
        {
            return new SearchTypeBuilder<SearchResult>(this, entities);
        }

        public ISearchTypeBuilder<SearchResult> Entities()
        {
            return new SearchTypeBuilder<SearchResult>(this);
        }

        private class SearchTypeBuilder<T> : ISearchTypeBuilder<T>
        {
            private readonly SearchBuilder searchBuilder;

            private readonly IList<Entity> entities;

            public SearchTypeBuilder(SearchBuilder searchBuilder) : this(searchBuilder, EnumExtensions.GetValues<Entity>())
            {
            }

            public SearchTypeBuilder(SearchBuilder searchBuilder, Entity entity) : this(searchBuilder, entity.Yield())
            {
            }

            public SearchTypeBuilder(SearchBuilder searchBuilder, IEnumerable<Entity> entities)
            {
                this.searchBuilder = searchBuilder;
                this.entities = entities.Distinct().ToList();
            }

            public ISearchTypeQueryBuilder<T> Matching(string query)
            {
                return new SearchTypeQueryBuilder(this.searchBuilder, this.entities, query);
            }

            public ISearchTypeQueryBuilder<T> Matching(Expression<Func<QueryFields, bool>> predicate)
            {
                return new SearchTypeQueryBuilder(this.searchBuilder, this.entities, QueryProvider.Get(predicate));
            }

            private class SearchTypeQueryBuilder : ISearchTypeQueryBuilder<T>
            {
                private readonly SearchBuilder searchBuilder;

                private readonly IList<Entity> entities;

                private readonly string query;

                public SearchTypeQueryBuilder(SearchBuilder searchBuilder, IList<Entity> entities, string query)
                {
                    this.searchBuilder = searchBuilder;
                    this.entities = entities;
                    this.query = query;
                }

                public async Task<T> GetAsync(string market, int limit, int offset, CancellationToken cancellationToken)
                {
                    var result = await this.searchBuilder.GetAsync<SearchResult>(
                        cancellationToken,
                        queryStringParameters: new { q = this.query, type = string.Join(",", this.entities.Select(item => item.GetDescription())), limit, offset },
                        optionalQueryStringParameters: new { market });

                    if (typeof(T) == typeof(SimpleAlbumsPageMessage))
                    {
                        return (T)(object)(new SimpleAlbumsPageMessage { Page = result.Albums });
                    }
                    else if (typeof(T) == typeof(FullArtistsPageMessage))
                    {
                        return (T)(object)(new FullArtistsPageMessage { Page = result.Artists });
                    }
                    else if (typeof(T) == typeof(FullTracksPageMessage))
                    {
                        return (T)(object)(new FullTracksPageMessage { Page = result.Tracks });
                    }
                    else if (typeof(T) == typeof(SimplePlaylistsPageMessage))
                    {
                        return (T)(object)(new SimplePlaylistsPageMessage { Page = result.Playlists });
                    }
                    else if (typeof(T) == typeof(SearchResult))
                    {
                        return (T)(object)result;
                    }
                    else
                    {
                        throw new ArgumentException(nameof(T));
                    }
                }
            }
        }
    }
}
