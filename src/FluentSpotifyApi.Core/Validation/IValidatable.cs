namespace FluentSpotifyApi.Core.Options
{
    /// <summary>
    /// Represents something that is validatable.
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        /// Performs validation. 
        /// </summary>
        void Validate();
    }
}
