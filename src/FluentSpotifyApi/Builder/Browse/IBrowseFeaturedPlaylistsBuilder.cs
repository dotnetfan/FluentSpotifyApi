using System;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Browse;

namespace FluentSpotifyApi.Builder.Browse
{
    /// <summary>
    /// The builder for "browse/featured-playlists" endpoint.
    /// </summary>
    public interface IBrowseFeaturedPlaylistsBuilder
    {
        /// <summary>
        /// Get a list of Spotify featured playlists (shown, for example, on a Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="locale">
        /// The desired language, consisting of a lowercase ISO 639 language code and an uppercase ISO 3166-1 alpha-2 country code, 
        /// joined by an underscore. (e.g. es_MX)
        /// </param>
        /// <param name="country">A country: an ISO 3166-1 alpha-2 country code.</param>
        /// <param name="timestamp">A timestamp in ISO 8601 format: yyyy-MM-ddTHH:mm:ss.</param>
        /// <param name="limit">The limit. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object).</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<FeaturedPlaylists> GetAsync( 
            string locale = null, 
            string country = null, 
            DateTime? timestamp = null, 
            int limit = 20,
            int offset = 0,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
