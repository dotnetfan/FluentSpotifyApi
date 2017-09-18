namespace FluentSpotifyApi.AuthorizationFlows.UWP.Exceptions
{
    /// <summary>
    /// The authorization error reason.
    /// </summary>
    public enum AuthorizationErrorReason
    {
        /// <summary>
        /// The unknown error.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The authorization has been canceled by user.
        /// </summary>
        CanceledByUser,

        /// <summary>
        /// An HTTP error has occurred.
        /// </summary>
        HttpError,
    }
}
