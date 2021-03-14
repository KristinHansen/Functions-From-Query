using System.Collections.Generic;

namespace Eli.Azure.Functions.Test
{
    public class ProductsModel
    {
        public ICollection<ProductModel> FromGuids { get; set; } = new List<ProductModel>();
        public List<ProductModel> FromIds { get; set; } = new List<ProductModel>();
    }
}
