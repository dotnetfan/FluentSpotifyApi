using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.Core.Internal
{
    /// <summary>
    /// The wrapper used for internal registrations in <see cref="IServiceCollection"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Internal.IWrapper{T}" />
    /// <seealso cref="System.IDisposable" />
    public class Wrapper<T> : IWrapper<T>, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Wrapper{T}" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="isOwned">if set to <c>true</c> the <paramref name="value"/> will be disposed.</param>
        public Wrapper(T value, bool isOwned)
        {
            this.Value = value;
            this.IsOwned = isOwned;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T Value { get; private set; }

        /// <summary>
        /// Determines whether the wrapped value will be disposed.
        /// </summary>
        public bool IsOwned { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.Value is IDisposable disposable && this.IsOwned)
            {
                disposable.Dispose();
            }
        }
    }
}
