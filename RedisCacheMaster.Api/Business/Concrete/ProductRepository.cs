using RedisCacheMaster.Api.Aspects.Cache;
using RedisCacheMaster.Api.Business.Abstract;
using RedisCacheMaster.Api.Models;
using System.Collections.Generic;

namespace RedisCacheMaster.Api.Business.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private static List<Product> _products;

        public ProductRepository()
        {
            CreateProductList();
        }

        [CacheAspect(expirationTime:500)]
        public List<Product> GetProducts()
        {
            return _products;
        }

        [RemoveCacheAspect("IProductRepository.Get")]
        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        private void CreateProductList()
        {
            _products = new List<Product>
            {
                new Product{Id = 1, Category = "Computers", Name = "Macbook Pro", UnitPrice = 2500, UnitsInStock = 20},
                new Product{Id = 1, Category = "Smart Phones", Name = "Poco X3", UnitPrice = 250, UnitsInStock = 75},
            };
        }
    }
}
