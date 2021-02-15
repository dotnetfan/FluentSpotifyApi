using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Model.Albums;
using FluentSpotifyApi.Model.Artists;
using FluentSpotifyApi.Model.Episodes;
using FluentSpotifyApi.Model.Playlists;
using FluentSpotifyApi.Model.Search;
using FluentSpotifyApi.Model.Shows;
using FluentSpotifyApi.Model.Tracks;

namespace FluentSpotifyApi.Builder.Search
{
    internal class SearchTypeQueryBuilder<T> : BuilderBase, ISearchTypeQueryBuilder<T>
    {
        private readonly IList<Entity> entities;

        private readonly string query;

        public SearchTypeQueryBuilder(BuilderBase parent, IList<Entity> entities, string query)
            : base(parent)
        {
            this.entities = entities;
            this.query = query;
        }

        public async Task<T> GetAsync(string market, int? limit, int? offset, IEnumerable<ExternalContent> includeExternal, CancellationToken cancellationToken)
        {
            var result = await this.GetAsync<SearchResponse>(
                cancellationToken,
                queryParams: new
                {
                    q = this.query,
                    type = this.entities.Select(item => item.GetEnumMemberValue()).JoinWithComma(),
                    market,
                    limit,
                    offset,
                    include_external = includeExternal?.Select(item => item.GetEnumMemberValue()).JoinWithComma()
                });

            if (typeof(T) == typeof(SimplifiedAlbumsPageResponse))
            {
                return (T)(object)(new SimplifiedAlbumsPageResponse { Page = result.Albums });
            }
            else if (typeof(T) == typeof(ArtistsPageResponse))
            {
                return (T)(object)(new ArtistsPageResponse { Page = result.Artists });
            }
            else if (typeof(T) == typeof(TracksPageResponse))
            {
                return (T)(object)(new TracksPageResponse { Page = result.Tracks });
            }
            else if (typeof(T) == typeof(SimplifiedPlaylistsPageResponse))
            {
                return (T)(object)(new SimplifiedPlaylistsPageResponse { Page = result.Playlists });
            }
            else if (typeof(T) == typeof(SimplifiedShowsPageResponse))
            {
                return (T)(object)(new SimplifiedShowsPageResponse { Page = result.Shows });
            }
            else if (typeof(T) == typeof(SimplifiedEpisodesPageResponse))
            {
                return (T)(object)(new SimplifiedEpisodesPageResponse { Page = result.Episodes });
            }
            else if (typeof(T) == typeof(SearchResponse))
            {
                return (T)(object)result;
            }
            else
            {
                throw new InvalidOperationException($"Search does not support result of type '{typeof(T)}'.");
            }
        }
    }
}
