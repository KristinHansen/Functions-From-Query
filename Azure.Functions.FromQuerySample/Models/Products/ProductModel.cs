using System;

namespace Azure.Functions.FromQuerySample
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
    }
}
