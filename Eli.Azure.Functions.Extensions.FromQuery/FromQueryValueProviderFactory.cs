using Eli.Azure.Functions.Extensions.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Reflection;

namespace Eli.Azure.Functions.Extensions.FromQuery
{
    internal class FromQueryValueProviderFactory
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IEnumerable<IStringValueConverter> _Converters;
        private readonly IOptions<ConverterOptions> _ConverterOptions;

        public FromQueryValueProviderFactory(
            IHttpContextAccessor httpContextAccessor,
            IEnumerable<IStringValueConverter> converters,
            IOptions<ConverterOptions> converterOptions)
        {
            _HttpContextAccessor = httpContextAccessor;
            _Converters = converters;
            _ConverterOptions = converterOptions;
        }

        public IValueProvider Create(ParameterInfo parameterInfo)
        {
            string parameterName = parameterInfo.GetCustomAttribute<FromQueryAttribute>() is FromQueryAttribute fromQuery
                ? fromQuery.QueryParameterName ?? parameterInfo.Name
                : parameterInfo.Name;

            return new FromQueryValueProvider(_HttpContextAccessor, _Converters, _ConverterOptions)
            {
                ParameterName = parameterName,
                Type = parameterInfo.ParameterType,
            };
        }
    }
}
