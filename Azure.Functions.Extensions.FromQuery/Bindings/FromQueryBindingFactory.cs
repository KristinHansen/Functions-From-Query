using Microsoft.Azure.WebJobs.Host.Bindings;
using System.Reflection;

namespace Azure.Functions.Extensions.FromQuery
{
    internal class FromQueryBindingFactory
    {
        private readonly FromQueryValueProviderFactory _ValueProviderFactory;

        public FromQueryBindingFactory(FromQueryValueProviderFactory valueProviderFactory)
        {
            _ValueProviderFactory = valueProviderFactory;
        }

        public IBinding Create(ParameterInfo parameterInfo) 
            => new FromQueryBinding(_ValueProviderFactory.Create(parameterInfo));
    }
}
