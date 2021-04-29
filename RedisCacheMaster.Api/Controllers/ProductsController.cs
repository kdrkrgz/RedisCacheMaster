using Microsoft.AspNetCore.Mvc;
using RedisCacheMaster.Api.Business.Abstract;
using RedisCacheMaster.Api.Models;
using RedisCacheMaster.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

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

        /// <summary>
        /// Get products from ProductRepository and add returned data to redis cache with specific key
        /// </summary>
        [SwaggerOperation(Summary = "Get products from ProductRepository and add returned data to redis cache with specific key")]
        [HttpGet]
        public IActionResult Get()
        {
            var result = _productRepository.GetProducts();
            return Ok(result);
        }

        /// <summary>
        /// Push product to static products list in ProductRepository and clear redis cache data by given pattern
        /// </summary>
        [SwaggerOperation(Summary = "Push product to static products list in ProductRepository and clear redis cache data by given pattern")]
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            _productRepository.AddProduct(product);
            return Ok();
        }
    }
}
