using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RedisCacheMaster.Api.Aspects.Cache;
using RedisCacheMaster.Api.Models;
using RedisCacheMaster.Api.Services;

namespace RedisCacheMaster.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CacheController:ControllerBase, ICacheController
    {

        private readonly ICacheService _cacheService;
        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetCacheData([FromRoute] string key)
        {
            var data = await _cacheService.GetCacheValueAsync(key);
            return string.IsNullOrEmpty(data) ? (IActionResult)NotFound() : Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> SetCacheData([FromBody]CacheModel cacheRequest)
        {
            await _cacheService.SetCacheValueAsync<CacheModel>(cacheRequest.Key, cacheRequest.Value);
            return Ok();
        }

        [HttpGet]
        [CacheAspect(expirationTime:60, Priorty = 1)]
        public IActionResult SetStaticCacheItem()
        {
            var products = GetProducts();
            return Ok(products);
        }

        [CacheAspect] // interceptor çalışmıyor aq
        public List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product{Id = 1, Category = "Computers", Name = "Macbook Pro", UnitPrice = 2500, UnitsInStock = 20},
                new Product{Id = 1, Category = "Smart Phones", Name = "Poco X3", UnitPrice = 250, UnitsInStock = 75},
            };
        }
    }
}
