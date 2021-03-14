using Eli.Azure.Functions.Extensions;
using System;

namespace Eli.Azure.Functions.Test
{
    public class DateTimeConverter : IStringValueConverter
    {
        public Type? Type => typeof(DateTime);

        public object Convert(string value)
        {
            return DateTime.Parse(value);
        }
    }
}
