using System;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Model.Browse;

namespace FluentSpotifyApi.Builder.Browse
{
    internal class BrowseFeaturedPlaylistsBuilder : BuilderBase, IBrowseFeaturedPlaylistsBuilder
    {
        public BrowseFeaturedPlaylistsBuilder(BuilderBase parent)
            : base(parent, "featured-playlists".Yield())
        {
        }

        public Task<FeaturedPlaylists> GetAsync(
            string country,
            string locale,
            DateTime? timestamp,
            int? limit,
            int? offset,
            CancellationToken cancellationToken)
        {
            return this.GetAsync<FeaturedPlaylists>(
                cancellationToken,
                queryParams: new { country, locale, timestamp = timestamp?.ToIsoWithUnspecifiedLocation(), limit, offset });
        }
    }
}
