using Eli.Azure.Functions.Extensions.Converters;
using Eli.Azure.Functions.Extensions.FromQuery;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Eli.Azure.Functions.Extensions
{
    public static class WebJobsBuilderExtensions
    {
        public static IWebJobsBuilder AddFromQueryExtension(this IWebJobsBuilder builder, Action<ConverterOptions>? action = null)
        {
            builder.AddExtension<FromQueryConfigProvider>();

            if (action is null)
            {
                action = new Action<ConverterOptions>(options => { });
            }

            builder.Services
                .Configure(action)
                .AddTransient<IStringValueConverter, GuidConverter>()
                .AddTransient<IStringValueConverter, IntConverter>()
                .AddTransient<FromQueryBindingProvider>()
                .AddTransient<FromQueryBindingFactory>()
                .AddTransient<FromQueryBinding>()
                .AddTransient<FromQueryValueProviderFactory>()
                .AddTransient<FromQueryValueProvider>();

            return builder;
        }
    }
}
