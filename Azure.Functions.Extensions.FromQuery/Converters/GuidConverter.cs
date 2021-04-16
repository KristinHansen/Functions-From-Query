using Microsoft.Extensions.Primitives;
using System;

namespace Azure.Functions.Extensions.FromQuery.Converters
{
    /// <summary>
    /// A <see cref="StringValues"/> to <see cref="Guid"/> converter.
    /// </summary>
    public class GuidConverter : IStringValuesConverter
    {
        public Type Type => typeof(Guid);

        public object Convert(StringValues values)
            => Guid.Parse(values);
    }
}
