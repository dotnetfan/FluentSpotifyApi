using System;

namespace FluentSpotifyApi.Expressions.Fields
{
    /// <summary>
    /// The fields provider.
    /// </summary>
    public static class FieldsProvider
    {
        /// <summary>
        /// Gets fields from the specified fields builder action.
        /// </summary>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <param name="buildFields">The action for building fields.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when an include or exclude expression is not valid.
        /// </exception>
        public static string Get<TInput>(Action<IFieldsBuilder<TInput>> buildFields)
        {
            var fieldsBuilder = new FieldsBuilder<TInput>();
            buildFields(fieldsBuilder);
            return fieldsBuilder.GetFields();
        }
    }
}
