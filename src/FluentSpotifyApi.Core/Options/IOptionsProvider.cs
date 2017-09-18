namespace FluentSpotifyApi.Core.Options
{
    /// <summary>
    /// The options provider interface.
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    public interface IOptionsProvider<out TOptions>
    {
        /// <summary>
        /// Gets the current options.
        /// </summary>
        TOptions Get();
    }
}
