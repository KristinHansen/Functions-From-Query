using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.Functions.Extensions.FromQuery
{
    /// <summary>
    /// Value provider for query collection parameters.
    /// </summary>
    internal class FromQueryValuesProvider : IValueProvider
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IStringValuesConverter _Converter;
        private readonly ICollectionConverter _CollectionConverter;
        private readonly string _ParameterName;

        public Type Type { get; }

        public FromQueryValuesProvider(
            IHttpContextAccessor httpContextAccessor,
            ICollectionConverter collectionConverter,
            IStringValuesConverter converter,
            string parameterName,
            Type type)
        {
            Type = type;

            _HttpContextAccessor = httpContextAccessor;
            _CollectionConverter = collectionConverter;
            _Converter = converter;
            _ParameterName = parameterName;
        }

        /// <inheritdoc/>
        public Task<object> GetValueAsync()
        {
            if (_HttpContextAccessor.HttpContext?.Request?.Query is null)
                throw new InvalidOperationException(nameof(_HttpContextAccessor.HttpContext.Request.Query));

            var stringValues = _HttpContextAccessor.HttpContext.Request.Query.ContainsKey(_ParameterName)
                ? _HttpContextAccessor.HttpContext.Request.Query[_ParameterName]
                : new StringValues();

            var convertedValues = stringValues
                .ToArray()
                .Select(value => _Converter.Convert(value))
                .ToList();

            var collection = _CollectionConverter.Convert(convertedValues);

            return Task.FromResult<object>(collection);
        }

        /// <inheritdoc/>
        public string ToInvokeString() 
            => string.Empty;
    }
}
