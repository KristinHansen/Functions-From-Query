using System.Collections.Generic;

namespace Azure.Functions.Extensions.FromQuery
{
    public class ConverterOptions
    {
        /// <summary>
        /// Collection of custom <see cref="IStringValuesConverter"/>s.<br/>
        /// The FromQuery extension will always priotize custom converters over
        /// built-in converters. If two or more converters resolves to the same
        /// <see cref="IStringValuesConverter.Type"/>, first converter of said
        /// type added to the ecollection wins ("first-in-wins").
        /// </summary>
        public ICollection<IStringValuesConverter> Converters { get; set; } = new List<IStringValuesConverter>();
    }
}
