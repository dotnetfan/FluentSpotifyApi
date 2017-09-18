using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native
{
    /// <summary>
    /// The interface for storing a string value securely.
    /// </summary>
    public interface ISecureStorage
    {
        /// <summary>
        /// Tries to get value from the storage.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>(true, value) in case there is a value in storage. (false, null) otherwise.</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1009:ClosingParenthesisMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
        Task<(bool IsSuccess, string Value)> TryGetAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Saves value to the storage.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task SaveAsync(string value, CancellationToken cancellationToken);

        /// <summary>
        /// Removes value from the storage.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task RemoveAsync(CancellationToken cancellationToken);
    }
}
