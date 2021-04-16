using Microsoft.Extensions.Primitives;
using System;

namespace Azure.Functions.Extensions.FromQuery
{
    /// <summary>
    /// Converter used for converting <see cref="StringValues"/> into the distinct converter's <see cref="Type"/>.
    /// </summary>
    public interface IStringValuesConverter
    {
        /// <summary>
        /// The type this converter will convert the <see cref="StringValues"/> into.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Converts the incoming <paramref name="values"/> into <see cref="Type"/>.
        /// </summary>
        /// <param name="values">The <see cref="StringValues"/> to convert.</param>
        /// <returns>An instance of type <see cref="Type"/> reflecting the values from <paramref name="values"/>.</returns>
        object Convert(StringValues values);
    }
}
