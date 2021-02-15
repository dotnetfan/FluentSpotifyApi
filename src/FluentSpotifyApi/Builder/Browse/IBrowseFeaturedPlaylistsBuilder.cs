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
        /// Gets a list of Spotify featured playlists (shown, for example, on a Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="country">
        /// A country: an ISO 3166-1 alpha-2 country code. Provide this parameter if you want the list of returned items to be relevant to a particular country.
        /// If omitted, the returned items will be relevant to all countries.
        /// </param>
        /// <param name="locale">
        /// The desired language, consisting of a lowercase ISO 639-1 language code and an uppercase ISO 3166-1 alpha-2 country code, joined by an underscore.
        /// For example: es_MX, meaning “Spanish (Mexico)”. Provide this parameter if you want the results returned in a particular language (where available).
        /// Note that, if locale is not supplied, or if the specified language is not available, all strings will be returned in the Spotify default language (American English).
        /// </param>
        /// <param name="timestamp">
        /// Use this parameter to specify the user’s local time to get results tailored for that specific date and time in the day.
        /// If not provided, the response defaults to the current UTC time.
        /// </param>
        /// <param name="limit">The limit. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object). Use with <paramref name="limit"/> to get the next set of items.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<FeaturedPlaylists> GetAsync(
            string country = null,
            string locale = null,
            DateTime? timestamp = null,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
