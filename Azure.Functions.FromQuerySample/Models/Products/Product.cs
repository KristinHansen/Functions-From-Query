using System;

namespace Azure.Functions.FromQuerySample
{
    public class Product : IEntity
    {
        public int Ix { get; set; }
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }
}
