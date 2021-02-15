namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    /// <summary>
    /// The interface for providing code verifier.
    /// </summary>
    public interface ICodeVerifierProvider
    {
        /// <summary>
        /// Gets code verifier.
        /// </summary>
        string Get();
    }
}
