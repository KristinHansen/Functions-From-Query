using Azure.Functions.Extensions.FromQuery;
using Azure.Functions.Extensions.FromQuery.Converters;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds all internally implemented <see cref="IStringValuesConverter"/>s.
        /// </summary>
        internal static IServiceCollection AddConverters(this IServiceCollection services)
        {
            return services
                .AddTransient<IStringValuesConverter, GuidConverter>()
                .AddTransient<IStringValuesConverter, IntConverter>();
        }
    }
}
