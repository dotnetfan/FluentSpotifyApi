namespace FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode
{
    /// <summary>
    /// The <see cref="IAuthenticationManager.RestoreSessionOrAuthorizeUserAsync(System.Threading.CancellationToken)"/> result.
    /// </summary>
    public enum RestoreSessionOrAuthorizeUserResult
    {
        /// <summary>
        /// Session is already cached in memory.
        /// </summary>
        NoAction,

        /// <summary>
        /// Session was loaded from local secure storage to memory.
        /// </summary>
        RestoredSessionFromLocalStorage,

        /// <summary>
        /// User authorization was performed.
        /// </summary>
        PerfomedUserAuthorization
    }
}
