using System;

namespace Eli.Azure.Functions.Extensions.Converters
{
    public class IntConverter : IStringValueConverter
    {
        public Type? Type => typeof(int);

        public object Convert(string value)
        {
            return int.Parse(value);
        }
    }
}
