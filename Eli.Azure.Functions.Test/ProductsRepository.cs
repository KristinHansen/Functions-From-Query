using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eli.Azure.Functions.Test
{
    internal class ProductsRepository : IRepository<Product>
    {
        private readonly IDictionary<Guid, Product> _Products;

        public ProductsRepository()
        {
            _Products = CreateTestProducts();
        }

        public Task<int> GetTotal()
        {
            return Task.FromResult(_Products.Count);
        }

        public Task Delete(Guid id)
        {
            if (_Products.ContainsKey(id))
            {
                _Products.Remove(id);
            }

            return Task.CompletedTask;
        }

        public Task<Product> Get(Guid id)
        {
            if (_Products.ContainsKey(id))
            {
                return Task.FromResult(_Products[id]);
            }

            throw new KeyNotFoundException(id.ToString());
        }

        public Task<ICollection<Product>> GetMany()
        {
            return GetMany(Array.Empty<int>());
        }

        public Task<ICollection<Product>> GetMany(params int[] ids)
        {
            IEnumerable<Product> result = _Products
                .Select(kvp => kvp.Value)
                .Where(product => ids?.Any() is true ? ids.Contains(product.Ix) : true)
                .ToList();

            return Task.FromResult((ICollection<Product>)result);
        }

        public Task<ICollection<Product>> GetMany(params Guid[] ids)
        {
            IEnumerable<Product> result = _Products
                .Where(kvp => ids?.Any() is true ? ids.Contains(kvp.Key) : true)
                .Select(kvp => kvp.Value)
                .ToList();

            return Task.FromResult((ICollection<Product>)result);
        }

        public Task Put(Product product)
        {
            _Products[product.Id] = product;
            return Task.CompletedTask;
        }

        private IDictionary<Guid, Product> CreateTestProducts()
        {
            return Enumerable.Range(1, 100)
                .Select(ix => new Product
                {
                    Ix = ix,
                    Id = Guid.NewGuid(),
                    Name = RandomSauce.RandomString(),
                    Price = RandomSauce.RandomPrice(),
                })
                .ToDictionary(x => x.Id, x => x);
        }
    }
}
