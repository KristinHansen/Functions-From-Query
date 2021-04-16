using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;

namespace Azure.Functions.Extensions.FromQuery
{
    /// <summary>
    /// The FromQuery extension configuration.
    /// </summary>
    [Extension("FromQuery")]
    internal class FromQueryConfigProvider : IExtensionConfigProvider
    {
        private readonly IBindingProvider _BindingProvider;

        public FromQueryConfigProvider(FromQueryBindingProvider bindingProvider)
        {
            _BindingProvider = bindingProvider;
        }

        /// <inheritdoc/>
        public void Initialize(ExtensionConfigContext context) 
            => context.AddBindingRule<FromQueryAttribute>().Bind(_BindingProvider);
    }
}
