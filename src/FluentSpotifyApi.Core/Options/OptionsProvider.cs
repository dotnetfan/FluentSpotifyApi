using System;
using Microsoft.Extensions.Options;

namespace FluentSpotifyApi.Core.Options
{
    /// <summary>
    /// The options provider implementation using <see cref="IOptions{TOptions}"/>.
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    public class OptionsProvider<TOptions> : IOptionsProvider<TOptions> where TOptions : class, new()
    {
        private readonly IOptions<TOptions> options;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsProvider{TOptions}"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public OptionsProvider(IOptions<TOptions> options)
        {
            this.options = options;
        }

        /// <summary>
        /// Gets the current options.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">Thrown when current options value is null.</exception>
        public TOptions Get()
        {
            var value = this.options.Value;

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value is IValidatable validatable)
            {
                validatable.Validate();
            }

            return value;
        }
    }
}
