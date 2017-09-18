using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.Core.Internal
{
    /// <summary>
    /// The wrapper used for internal registrations in <see cref="IServiceCollection"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWrapper<out T>
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        T Value { get; }
    }
}
