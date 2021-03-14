using Microsoft.Azure.WebJobs.Description;
using System;

namespace Eli.Azure.Functions.Extensions.Attributes
{
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FromQueryAttribute : Attribute
    {
        /// <summary>
        /// If left null the query parameter name will be resolved to the decorated parameter's name.
        /// </summary>
        public string? QueryParameterName { get; set; }
    }
}
