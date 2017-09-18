using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Artists
{
    /// <summary>
    /// The builder for "artists/{id}/albums" endpoint.
    /// </summary>
    public interface IArtistAlbumsBuilder
    {
        /// <summary>
        /// Get Spotify catalog information about an artist’s albums. 
        /// </summary>
        /// <param name="albumTypes">The album types. Can be used together with <paramref name="dynamicAlbumTypes"/>.</param>
        /// <param name="dynamicAlbumTypes">The dynamic album types. Can be used together with <paramref name="albumTypes"/>.</param>
        /// <param name="market">An ISO 3166-1 alpha-2 country code.</param>
        /// <param name="limit">The number of album objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first album to return. Default: 0 (i.e., the first album).</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Page<SimpleAlbum>> GetAsync(           
            IEnumerable<AlbumType> albumTypes = null, 
            IEnumerable<string> dynamicAlbumTypes = null, 
            string market = null, 
            int limit = 20, 
            int offset = 0,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
