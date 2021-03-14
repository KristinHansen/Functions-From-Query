using Microsoft.Azure.WebJobs.Host.Bindings;
using System.Reflection;

namespace Eli.Azure.Functions.Extensions.FromQuery
{
    internal class FromQueryBindingFactory
    {
        private readonly FromQueryValueProviderFactory _ValueProviderFactory;

        public FromQueryBindingFactory(FromQueryValueProviderFactory valueProviderFactory)
        {
            _ValueProviderFactory = valueProviderFactory;
        }

        public IBinding Create(ParameterInfo parameterInfo)
        {
            return new FromQueryBinding(_ValueProviderFactory.Create(parameterInfo));
        }
    }
}
