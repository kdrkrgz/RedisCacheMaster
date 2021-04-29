using Microsoft.AspNetCore.Mvc;
using RedisCacheMaster.Api.Business.Abstract;
using RedisCacheMaster.Api.Models;
using RedisCacheMaster.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCacheMaster.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(ICacheService cacheService, IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            _productRepository.AddProduct(product);
            return Ok();
        }
    }
}
