using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Internal.Extensions;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Messages;

namespace FluentSpotifyApi.Builder.Search
{
    internal class SearchBuilder : BuilderBase, ISearchBuilder
    {
        public SearchBuilder(ContextData contextData) : base(contextData, "search")
        {
        }

        public ISearchEntityBuilder<SimpleAlbumsPageMessage> Albums => new SearchEntitiesBuilder<SimpleAlbumsPageMessage>(this, Entity.Album);

        public ISearchEntityBuilder<FullArtistsPageMessage> Artists => new SearchEntitiesBuilder<FullArtistsPageMessage>(this, Entity.Artist);

        public ISearchEntityBuilder<SimplePlaylistsPageMessage> Playlists => new SearchEntitiesBuilder<SimplePlaylistsPageMessage>(this, Entity.Playlist);

        public ISearchEntityBuilder<FullTracksPageMessage> Tracks => new SearchEntitiesBuilder<FullTracksPageMessage>(this, Entity.Track);

        public ISearchEntitiesBuilder Entities(params Entity[] entities)
        {
            return new SearchEntitiesBuilder<object>(this, entities);
        }

        public ISearchEntitiesBuilder Entities()
        {
            return new SearchEntitiesBuilder<object>(this);
        }

        private class SearchEntitiesBuilder<T> : ISearchEntitiesBuilder, ISearchEntityBuilder<T>
        {
            private readonly SearchBuilder searchBuilder;

            private readonly IList<Entity> entities;

            public SearchEntitiesBuilder(SearchBuilder searchBuilder) : this(searchBuilder, EnumExtensions.GetValues<Entity>())
            {
            }

            public SearchEntitiesBuilder(SearchBuilder searchBuilder, Entity entity) : this(searchBuilder, entity.Yield())
            {
            }

            public SearchEntitiesBuilder(SearchBuilder searchBuilder, IEnumerable<Entity> entities)
            {
                this.searchBuilder = searchBuilder;
                this.entities = entities.Distinct().ToList();
            }

            public ISearchEntitiesQueryBuilder Matching(string query)
            {
                return new SearchEntitiesQueryBuilder(this.searchBuilder, this.entities, query);
            }

            ISearchEntityQueryBuilder<T> ISearchEntityBuilder<T>.Matching(string query)
            {
                return new SearchEntitiesQueryBuilder(this.searchBuilder, this.entities, query);
            }

            private class SearchEntitiesQueryBuilder : ISearchEntitiesQueryBuilder, ISearchEntityQueryBuilder<T>
            {
                private readonly SearchBuilder searchBuilder;

                private readonly IList<Entity> entities;

                private readonly string query;

                public SearchEntitiesQueryBuilder(SearchBuilder searchBuilder, IList<Entity> entities, string query)
                {
                    this.searchBuilder = searchBuilder;
                    this.entities = entities;
                    this.query = query;
                }

                async Task<T> ISearchEntityQueryBuilder<T>.GetAsync(string market, int limit, int offset, CancellationToken cancellationToken)
                {
                    var result = await this.GetAsync(market, limit, offset, cancellationToken).ConfigureAwait(false);
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
                    else
                    {
                        throw new ArgumentException(nameof(T));
                    }
                }

                public Task<SearchResult> GetAsync(string market, int limit, int offset, CancellationToken cancellationToken)
                {
                    return this.searchBuilder.GetAsync<SearchResult>(
                        cancellationToken, 
                        queryStringParameters: new { q = this.query, type = string.Join(",", this.entities.Select(item => item.GetDescription())), limit, offset },
                        optionalQueryStringParameters: new { market });
                }
            }
        }
    }
}
