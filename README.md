Install using: <pre>Install-Package Hansen.Azure.Functions.Extensions.FromQuery -Version 1.0.1</pre>

Add the extension to WebJobs using the WebJobsStartUp:
```csharp
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
}
```

See the sample project for a full example with a working `HttpTrigger`.
