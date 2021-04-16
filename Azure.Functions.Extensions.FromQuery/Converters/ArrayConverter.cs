using System;
using System.Collections;

namespace Azure.Functions.Extensions.FromQuery.Converters
{
    /// <summary>
    /// A <see cref="ICollection"/> to <see cref="Array"/> converter.
    /// </summary>
    internal class ArrayConverter : ICollectionConverter
    {
        private readonly Type _ResolvedType;

        public ArrayConverter(Type resolvedType)
        {
            _ResolvedType = resolvedType;
        }

        public ICollection Convert(ICollection collection)
        {
            var array = Array.CreateInstance(_ResolvedType, collection.Count)!;
            collection.CopyTo(array, 0);
            return array;
        }
    }
}
