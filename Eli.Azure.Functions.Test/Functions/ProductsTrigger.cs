using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eli.Azure.Functions.Test
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

        [FunctionName("GetProductsList")]
        public async Task<IActionResult> GetProductsList(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products/list")] HttpRequest request,
            [Extensions.Attributes.FromQuery] List<Guid> productIds,
            [Extensions.Attributes.FromQuery] int[] ids)
        {
            ICollection<Product> productsFromIds = await _ProductsRepository.GetMany(ids);
            List<ProductModel> productModelsFromIds = productsFromIds.Select(product => _Mapper.Map<ProductModel>(product)).ToList();

            ICollection<Product> productsFromGuids = await _ProductsRepository.GetMany(productIds.ToArray());
            List<ProductModel> productModelsFromGuids = productsFromGuids.Select(product => _Mapper.Map<ProductModel>(product)).ToList();

            return new OkObjectResult(new ProductsModel
            {
                FromGuids = productModelsFromGuids,
                FromIds = productModelsFromIds,
            });
        }

        [FunctionName("GetProductsArray")]
        public async Task<IActionResult> GetProductsArray(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products/array")] HttpRequest request,
            [Extensions.Attributes.FromQuery] Guid[] productIds,
            [Extensions.Attributes.FromQuery] List<int> ids)
        {
            ICollection<Product> productsFromIds = await _ProductsRepository.GetMany(ids.ToArray());
            List<ProductModel> productModelsFromIds = productsFromIds.Select(product => _Mapper.Map<ProductModel>(product)).ToList();

            ICollection<Product> productsFromGuids = await _ProductsRepository.GetMany(productIds);
            List<ProductModel> productModelsFromGuids = productsFromGuids.Select(product => _Mapper.Map<ProductModel>(product)).ToList();

            return new OkObjectResult(new ProductsModel
            {
                FromGuids = productModelsFromGuids,
                FromIds = productModelsFromIds,
            });
        }
    }
}
