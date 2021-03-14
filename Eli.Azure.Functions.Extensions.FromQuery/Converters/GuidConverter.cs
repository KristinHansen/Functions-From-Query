using System;

namespace Eli.Azure.Functions.Extensions.Converters
{
    public class GuidConverter : IStringValueConverter
    {
        public Type? Type => typeof(Guid);

        public object Convert(string value)
        {
            return Guid.Parse(value);
        }
    }
}
