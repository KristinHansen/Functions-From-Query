using System.Collections.Generic;

namespace Eli.Azure.Functions.Extensions.FromQuery
{
    public class ConverterOptions
    {
        public ICollection<IStringValueConverter> Converters { get; set; } = new List<IStringValueConverter>();
    }
}
