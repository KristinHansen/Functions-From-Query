using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace Azure.Functions.Extensions.FromQuery
{
    /// <summary>
    /// Value provider for query non-collection parameters.
    /// </summary>
    internal class FromQueryValueProvider : IValueProvider
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IStringValuesConverter _Converter;
        private readonly string _ParameterName;

        public Type Type { get; }

        public FromQueryValueProvider(IHttpContextAccessor httpContextAccessor, IStringValuesConverter converter, string parameterName, Type type)
        {
            Type = type;

            _HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(IHttpContextAccessor));
            _Converter = converter ?? throw new ArgumentNullException(nameof(IStringValuesConverter));
            _ParameterName = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
        }

        /// <inheritdoc/>
        public Task<object> GetValueAsync()
        {
            if (_HttpContextAccessor.HttpContext?.Request?.Query is null)
                throw new InvalidOperationException(nameof(_HttpContextAccessor.HttpContext.Request.Query));

            var stringValues = _HttpContextAccessor.HttpContext.Request.Query.ContainsKey(_ParameterName)
                ? _HttpContextAccessor.HttpContext.Request.Query[_ParameterName]
                : new StringValues();

            return Task.FromResult(_Converter.Convert(stringValues));
        }

        /// <inheritdoc/>
        public string ToInvokeString()
            => string.Empty;
    }
}
