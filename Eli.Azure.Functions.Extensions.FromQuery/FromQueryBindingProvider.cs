using Microsoft.Azure.WebJobs.Host.Bindings;
using System.Threading.Tasks;

namespace Eli.Azure.Functions.Extensions.FromQuery
{
    internal class FromQueryBindingProvider : IBindingProvider
    {
        private readonly FromQueryBindingFactory _BindingFactory;

        public FromQueryBindingProvider(FromQueryBindingFactory bindingFactory)
        {
            _BindingFactory = bindingFactory;
        }

        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            return Task.FromResult(_BindingFactory.Create(context.Parameter));
        }
    }
}
