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
        /// <summary>
        /// Get specific value from Redis server
        /// </summary>
        /// <remarks>
        /// You can check product cache with this key => RedisCacheMaster.Api.Business.Abstract.IProductRepository.GetProducts()
        /// </remarks>
        [HttpGet("{key}")]
        public async Task<IActionResult> GetCacheData([FromRoute] string key)
        {
            var data = await _cacheService.GetCacheValueAsync<List<Product>>(key);
            return data == null ? (IActionResult)NotFound() : Ok(data);
        }

    }
}
