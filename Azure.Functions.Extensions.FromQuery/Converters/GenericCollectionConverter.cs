using System;
using System.Collections;
using System.Collections.Generic;

namespace Azure.Functions.Extensions.FromQuery.Converters
{
    /// <summary>
    /// A <see cref="ICollection"/> to <see cref="List{T}"/> converter.
    /// </summary>
    internal class GenericCollectionConverter : ICollectionConverter
    {
        private readonly Type _ResolvedType;

        public GenericCollectionConverter(Type resolvedType)
        {
            _ResolvedType = resolvedType;
        }

        public ICollection Convert(ICollection collection)
        {
            var genericTypeDefinition = typeof(List<>).GetGenericTypeDefinition()!;
            var genericList = genericTypeDefinition.MakeGenericType(_ResolvedType)!;
            var initializedGenericList = (IList)Activator.CreateInstance(genericList)!;

            foreach (var item in collection)
                initializedGenericList.Add(item);

            return initializedGenericList;
        }
    }
}
