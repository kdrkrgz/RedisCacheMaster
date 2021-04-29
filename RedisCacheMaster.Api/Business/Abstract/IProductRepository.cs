using RedisCacheMaster.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedisCacheMaster.Api.Utilities.Results;

namespace RedisCacheMaster.Api.Business.Abstract
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        void AddProduct(Product product);
    }
}
