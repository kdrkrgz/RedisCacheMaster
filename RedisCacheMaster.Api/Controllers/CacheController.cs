using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RedisCacheMaster.Api.Aspects.Cache;
using RedisCacheMaster.Api.Business.Abstract;
using RedisCacheMaster.Api.Models;
using RedisCacheMaster.Api.Services;

namespace RedisCacheMaster.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CacheController : ControllerBase
    {

        private readonly ICacheService _cacheService;
        private readonly IProductRepository _productRepository;
        public CacheController(ICacheService cacheService, IProductRepository productRepository)
        {
            _cacheService = cacheService;
            _productRepository = productRepository;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetCacheData([FromRoute] string key)
        {
            var data = "";//await _cacheService.GetCacheValueAsync<Product>(key);
            return data == null ? (IActionResult)NotFound() : Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> SetCacheData([FromBody] CacheModel cacheRequest)
        {
            await _cacheService.SetCacheValueAsync<CacheModel>(cacheRequest.Key, cacheRequest.Value);
            return Ok();
        }

        [HttpGet]
        public IActionResult SetStaticCacheItem()
        {
            var products = _productRepository.GetProducts();
            return Ok(products);
        }
    }
}
