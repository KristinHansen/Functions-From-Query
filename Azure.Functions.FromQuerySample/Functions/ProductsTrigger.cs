using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.Functions.FromQuerySample
{
    public class ProductsTrigger
    {
        private readonly IRepository<Product> _ProductsRepository;
        private readonly IMapper _Mapper;

        public ProductsTrigger(
            IRepository<Product> productsRepository,
            IMapper mapper)
        {
            _ProductsRepository = productsRepository;
            _Mapper = mapper;
        }

        [FunctionName("GetProducts")]
        public async Task<IActionResult> GetProductsList(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products")] HttpRequest request,
            [FromQuery] List<Guid> guids,
            [FromQuery] int[] ints)
        {
            var products = new List<Product>();

            if (guids.Any())
                products.AddRange(await _ProductsRepository.GetMany(guids.ToArray()));

            if (ints.Any())
                products.AddRange(await _ProductsRepository.GetMany(ints));

            if (!products.Any())
                products.AddRange(await _ProductsRepository.GetMany());

            var productModels = products
                .Distinct(new ProductComparer())
                .Select(product => _Mapper.Map<ProductModel>(product))
                .ToList();

            return new OkObjectResult(new ProductsModel
            {
                Products = productModels,
            });
        }

        private class ProductComparer : IEqualityComparer<Product>
        {
            public bool Equals([AllowNull] Product x, [AllowNull] Product y)
                => x is not null && y is not null && x.Ix.Equals(y.Ix);

            public int GetHashCode([DisallowNull] Product obj)
                => obj.GetHashCode();
        }
    }
}
