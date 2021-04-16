using Azure.Functions.Extensions.FromQuery;
using Microsoft.Extensions.Primitives;
using System;

namespace Azure.Functions.FromQuerySample
{
    public class DateTimeConverter : IStringValuesConverter
    {
        public Type Type => typeof(DateTime);

        public object Convert(StringValues values)
            => DateTime.Parse(values);
    }
}
