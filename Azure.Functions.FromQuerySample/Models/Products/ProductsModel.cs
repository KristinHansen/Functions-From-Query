using System.Collections.Generic;

namespace Azure.Functions.FromQuerySample
{
    public class ProductsModel
    {
        public ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();
    }
}
