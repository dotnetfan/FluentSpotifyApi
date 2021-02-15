using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Albums;

namespace FluentSpotifyApi.Builder.Artists
{
    /// <summary>
    /// The builder for "artists/{id}/albums" endpoint.
    /// </summary>
    public interface IArtistAlbumsBuilder
    {
        /// <summary>
        /// Gets Spotify catalog information about an artist’s albums.
        /// </summary>
        /// <param name="includeGroups">The list of keywords that will be used to filter the response. If not supplied, all album types will be returned.</param>
        /// <param name="market">
        /// Synonym for country. An ISO 3166-1 alpha-2 country code or the string from_token.
        /// Supply this parameter to limit the response to one particular geographical market.
        /// </param>
        /// <param name="limit">The number of album objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first album to return. Default: 0 (i.e., the first album). Use with <paramref name="limit"/> to get the next set of albums.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Page<SimplifiedAlbum>> GetAsync(
            IEnumerable<AlbumType> includeGroups = null,
            string market = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
