namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    /// <summary>
    /// The interface for providing CSRF token.
    /// </summary>
    public interface ICsrfTokenProvider
    {
        /// <summary>
        /// Gets CSRF token.
        /// </summary>
        /// <returns></returns>
        string Get();
    }
}
