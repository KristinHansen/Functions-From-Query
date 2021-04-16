using Microsoft.Azure.WebJobs.Host.Bindings;
using System.Threading.Tasks;

namespace Azure.Functions.Extensions.FromQuery
{
    internal class FromQueryBindingProvider : IBindingProvider
    {
        private readonly FromQueryBindingFactory _BindingFactory;

        public FromQueryBindingProvider(FromQueryBindingFactory bindingFactory)
        {
            _BindingFactory = bindingFactory;
        }

        /// <inheritdoc/>
        public Task<IBinding> TryCreateAsync(BindingProviderContext context) 
            => Task.FromResult(_BindingFactory.Create(context.Parameter));
    }
}
