namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    /// <summary>
    /// The authorization response.
    /// </summary>
    /// <typeparam name="TPayload">The type of the payload.</typeparam>
    public class AuthorizationResponse<TPayload>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationResponse{TPayload}"/> class.
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <param name="csrfToken">The CSRF token.</param>
        public AuthorizationResponse(TPayload payload, string csrfToken)
        {
            this.Payload = payload;
            this.CsrfToken = csrfToken;
        }
 
        /// <summary>
        /// Gets the payload.
        /// </summary>
        /// <value>
        /// The payload.
        /// </value>
        public TPayload Payload { get; private set; }

        /// <summary>
        /// Gets the CSRF token.
        /// </summary>
        /// <value>
        /// The CSRF token.
        /// </value>
        public string CsrfToken { get; private set; }
    }
}
