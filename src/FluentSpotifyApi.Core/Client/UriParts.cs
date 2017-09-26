using System;
using System.Collections.Generic;

namespace FluentSpotifyApi.Core.Client
{
    /// <summary>
    /// The URI parts.
    /// </summary>
    public class UriParts
    {
        /// <summary>
        /// The base URI.
        /// </summary>
        public Uri BaseUri { get; set; }

        /// <summary>
        /// The query string parameters represented as an object.
        /// </summary>
        public object QueryStringParameters { get; set; }

        /// <summary>
        /// The route values represented as a sequence of objects.
        /// </summary>
        public IEnumerable<object> RouteValues { get; set; }
    }
}
