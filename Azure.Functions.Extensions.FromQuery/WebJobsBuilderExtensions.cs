using Azure.Functions.Extensions.FromQuery;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microsoft.Azure.WebJobs.Hosting
{
    public static class WebJobsBuilderExtensions
    {
        /// <summary>
        /// Adds the <see cref="FromQueryAttribute"/> extension which enables array and list bindings for Azure Functions,
        /// when using HttpTriggers.<br/>
        /// Use the <paramref name="options"/> parameter to configure your own <see cref="IStringValuesConverter"/>s.<br/>
        /// The value provider will always check for a value converter in <see cref="ConverterOptions.Converters"/>
        /// before using the built in converters for <see cref="Guid"/> and <see cref="int"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IWebJobsBuilder"/> to add the extensions to.</param>
        /// <param name="options">The options for configuring custom <see cref="IStringValuesConverter"/>s.</param>
        /// <returns>A reference to this instance for chaining.</returns>
        public static IWebJobsBuilder AddFromQueryExtension(this IWebJobsBuilder builder, Action<ConverterOptions>? options = null)
        {
            builder.AddExtension<FromQueryConfigProvider>();
            builder.Services
                .Configure(options ?? new Action<ConverterOptions>(_ => { }))
                .AddTransient<FromQueryBindingProvider>()
                .AddTransient<FromQueryBindingFactory>()
                .AddTransient<FromQueryValueProviderFactory>()
                .AddConverters();

            return builder;
        }
    }
}
