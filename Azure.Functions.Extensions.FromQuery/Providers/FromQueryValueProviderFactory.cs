using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using Azure.Functions.Extensions.FromQuery.Converters;

namespace Azure.Functions.Extensions.FromQuery
{
    /// <summary>
    /// <see cref="IValueProvider"/> factory, specifically for value providers related to the FromQuery extension.
    /// </summary>
    internal class FromQueryValueProviderFactory
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IEnumerable<IStringValuesConverter> _Converters;
        private readonly ConverterOptions _ConverterOptions;

        public FromQueryValueProviderFactory(
            IHttpContextAccessor httpContextAccessor,
            IEnumerable<IStringValuesConverter> converters,
            IOptions<ConverterOptions> converterOptions)
        {
            _HttpContextAccessor = httpContextAccessor;
            _Converters = converters;
            _ConverterOptions = converterOptions.Value;
        }

        /// <summary>
        /// Creates an <see cref="IValueProvider"/>.
        /// </summary>
        /// <param name="parameterInfo">The binding parameter's information.</param>
        /// <returns>A <see cref="IValueProvider"/> capable of providing values of type determined by the incoming <paramref name="parameterInfo"/>.</returns>
        public IValueProvider Create(ParameterInfo parameterInfo)
        {
            var parameterName = parameterInfo.GetCustomAttribute<FromQueryAttribute>() is FromQueryAttribute fromQuery
                ? fromQuery.QueryParameterName ?? parameterInfo.Name
                : parameterInfo.Name;

            var type = parameterInfo.ParameterType;
            var isEnumerable = typeof(IEnumerable).IsAssignableFrom(type);
            var isArray = typeof(Array).IsAssignableFrom(type);
            var isGeneric = type.IsGenericType || isArray;
            if (isEnumerable && !isGeneric)
                throw new InvalidOperationException("The FromQuery extension only supports generic collections.");
            
            var resolvedType = isEnumerable
                ? GetGenericType(type)
                : type;

            var collectionConverter = isArray
                ? (ICollectionConverter) new ArrayConverter(resolvedType)
                : new GenericCollectionConverter(resolvedType);

            var converter = _ConverterOptions.Converters.FirstOrDefault(converter => converter.Type == resolvedType) is IStringValuesConverter customConverter
                ? customConverter
                : _Converters.First(converter => converter.Type == resolvedType);

            return isEnumerable
                ? new FromQueryValuesProvider(_HttpContextAccessor, collectionConverter, converter, parameterName, type)
                : new FromQueryValueProvider(_HttpContextAccessor, converter, parameterName, type);
        }

        private Type GetGenericType(Type type)
        {
            return typeof(Array).IsAssignableFrom(type)
                ? type.GetElementType()!
                : type.GenericTypeArguments.Single();
        }
    }
}
