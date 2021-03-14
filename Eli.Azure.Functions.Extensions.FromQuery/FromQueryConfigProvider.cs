using Eli.Azure.Functions.Extensions.Attributes;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;

namespace Eli.Azure.Functions.Extensions.FromQuery
{
    [Extension("FromQuery")]
    internal class FromQueryConfigProvider : IExtensionConfigProvider
    {
        private readonly IBindingProvider _BindingProvider;

        public FromQueryConfigProvider(FromQueryBindingProvider bindingProvider)
        {
            _BindingProvider = bindingProvider;
        }

        public void Initialize(ExtensionConfigContext context)
        {
            context.AddBindingRule<FromQueryAttribute>().Bind(_BindingProvider);
        }
    }
}
