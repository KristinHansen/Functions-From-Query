using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Azure.Functions.Extensions.FromQuery
{
    public class FromQueryBinding : IBinding
    {
        private readonly IValueProvider _ValueProvider;

        public bool FromAttribute { get; }
        public ParameterInfo? Parameter { get; set; }

        public FromQueryBinding(IValueProvider valueProvider)
        {
            _ValueProvider = valueProvider;
        }

        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
            => throw new NotImplementedException();

        public Task<IValueProvider> BindAsync(BindingContext context)
            => Task.FromResult(_ValueProvider);

        public ParameterDescriptor ToParameterDescriptor()
            => new ParameterDescriptor();
    }
}
