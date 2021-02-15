using System;
using System.Linq;
using FluentSpotifyApi.Core.Utils;
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
    internal class SearchBuilder : BuilderBase, ISearchBuilder
    {
        public SearchBuilder(RootBuilder root)
            : base(root, "search".Yield())
        {
        }

        public ISearchTypeBuilder<SimplifiedAlbumsPageResponse> Albums => new SearchTypeBuilder<SimplifiedAlbumsPageResponse>(this, new[] { Entity.Album });

        public ISearchTypeBuilder<ArtistsPageResponse> Artists => new SearchTypeBuilder<ArtistsPageResponse>(this, new[] { Entity.Artist });

        public ISearchTypeBuilder<SimplifiedPlaylistsPageResponse> Playlists => new SearchTypeBuilder<SimplifiedPlaylistsPageResponse>(this, new[] { Entity.Playlist });

        public ISearchTypeBuilder<TracksPageResponse> Tracks => new SearchTypeBuilder<TracksPageResponse>(this, new[] { Entity.Track });

        public ISearchTypeBuilder<SimplifiedShowsPageResponse> Shows => new SearchTypeBuilder<SimplifiedShowsPageResponse>(this, new[] { Entity.Show });

        public ISearchTypeBuilder<SimplifiedEpisodesPageResponse> Episodes => new SearchTypeBuilder<SimplifiedEpisodesPageResponse>(this, new[] { Entity.Episode });

        public ISearchTypeBuilder<SearchResponse> Entities(params Entity[] entities)
        {
            SpotifyArgumentAssertUtils.ThrowIfNull(entities, nameof(entities));

            return new SearchTypeBuilder<SearchResponse>(this, entities);
        }

        public ISearchTypeBuilder<SearchResponse> Entities()
        {
            return new SearchTypeBuilder<SearchResponse>(this, Enum.GetValues(typeof(Entity)).Cast<Entity>());
        }
    }
}
