using Microsoft.Extensions.Primitives;
using System;

namespace Azure.Functions.Extensions.FromQuery.Converters
{
    /// <summary>
    /// A <see cref="StringValues"/> to <see cref="int"/> converter.
    /// </summary>
    public class IntConverter : IStringValuesConverter
    {
        public Type Type => typeof(int);

        public object Convert(StringValues values)
            => int.Parse(values);
    }
}
