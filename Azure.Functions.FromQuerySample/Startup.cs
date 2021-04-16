using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Azure.Functions.FromQuerySample.FunctionStartup))]
[assembly: WebJobsStartup(typeof(Azure.Functions.FromQuerySample.WebJobsStartup))]
namespace Azure.Functions.FromQuerySample
{
    public class WebJobsStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddFromQueryExtension(options =>
            {
                options.Converters.Add(new DateTimeConverter());
            });
        }
    }

    public class FunctionStartup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
            => ConfigureServices(builder.Services);

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRepository<Product>, ProductsRepository>();
            services.AddAutoMapper(typeof(FunctionStartup));
        }
    }
}
