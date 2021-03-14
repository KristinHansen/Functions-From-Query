using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eli.Azure.Functions.Extensions.FromQuery
{
    public class FromQueryValueProvider : IValueProvider
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IEnumerable<IStringValueConverter> _Converters;
        private readonly ConverterOptions _ConverterOptions;

        public Type? Type { get; set; }
        public string? ParameterName { get; set; }

        public FromQueryValueProvider(IHttpContextAccessor httpContextAccessor, IEnumerable<IStringValueConverter> converters, IOptions<ConverterOptions> converterOptions)
        {
            _HttpContextAccessor = httpContextAccessor;
            _Converters = converters;
            _ConverterOptions = converterOptions.Value;
        }

        public Task<object> GetValueAsync()
        {
            if (Type is null)
            {
                throw new ArgumentNullException(nameof(Type));
            }

            if (string.IsNullOrWhiteSpace(ParameterName))
            {
                throw new ArgumentNullException(nameof(ParameterName));
            }

            if (_HttpContextAccessor.HttpContext is null)
            {
                throw new ArgumentNullException(nameof(_HttpContextAccessor.HttpContext));
            }

            StringValues stringValues = _HttpContextAccessor.HttpContext.Request.Query.ContainsKey(ParameterName)
                ? _HttpContextAccessor.HttpContext.Request.Query[ParameterName]
                : new StringValues();

            Type resolvedType = ResolveType(Type);


            object[] convertedValues = typeof(string).IsAssignableFrom(resolvedType)
                ? stringValues.ToArray()
                : ConvertValues(stringValues, resolvedType);

            if (typeof(Array).IsAssignableFrom(Type))
            {
                object array = Array.CreateInstance(resolvedType, convertedValues.Length)!;
                
                for (int i = 0; i < ((Array)array).Length; i++)
                {
                    ((Array)array).SetValue(convertedValues[i], i);
                }

                return Task.FromResult(array);
            }

            if (typeof(IEnumerable).IsAssignableFrom(Type))
            {
                Type genericTypeDefinition = Type.GetGenericTypeDefinition()!;
                Type genericList = genericTypeDefinition.MakeGenericType(resolvedType)!;
                object initializedGenericList = Activator.CreateInstance(genericList)!;

                for (int i = 0; i < convertedValues.Length; i++)
                {
                    ((IList)initializedGenericList).Add(convertedValues[i]);
                }

                return Task.FromResult(initializedGenericList);
            }

            object convertedValue = Activator.CreateInstance(resolvedType, convertedValues.Single())!;
            return Task.FromResult(convertedValue);
        }

        private object[] ConvertValues(StringValues stringValues, Type resolvedType)
        {
            IStringValueConverter converter = _ConverterOptions.Converters.Any(converter => converter.Type == resolvedType)
                ? _ConverterOptions.Converters.First(converter => converter.Type == resolvedType)
                : _Converters.First(converter => converter.Type == resolvedType);

            return stringValues
                .ToList()
                .Select(value => converter.Convert(value))
                .ToArray();
        }

        private Type ResolveType(Type type)
        {
            if (typeof(Array).IsAssignableFrom(type))
            {
                return type.GetElementType()!;
            }

            if (typeof(IEnumerable).IsAssignableFrom(type) && type.IsGenericType)
            {
                return type.GetGenericArguments().Single();
            }

            return type;
        }

        public string ToInvokeString()
        {
            return string.Empty;
        }
    }
}
