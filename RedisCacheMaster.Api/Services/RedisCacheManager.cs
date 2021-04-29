using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RedisCacheMaster.Api.Utilities.Results;
using StackExchange.Redis;

namespace RedisCacheMaster.Api.Services
{
    public class RedisCacheManager: ICacheService
    {
        private readonly RedisServer _redisServer;

        public RedisCacheManager(RedisServer redisServer)
        {
            _redisServer = redisServer;
        }


        public async Task<T> GetCacheValueAsync<T>(string key)
        {
            var cacheData = await _redisServer.Database.StringGetAsync(key);
            return JsonConvert.DeserializeObject<T>(cacheData);
        }

        public async Task SetCacheValueAsync<T>(string key, object data, int expirationTime = 600)
        {
            var cacheData = JsonConvert.SerializeObject(data);
            await _redisServer.Database.StringSetAsync(key, cacheData);
            await _redisServer.Database.KeyExpireAsync(key, TimeSpan.FromSeconds(expirationTime));
        }

        public async Task RemoveCacheValueAsync(string key)
        {
            await _redisServer.Database.KeyDeleteAsync(key);
        }

        public async Task RemoveCacheByPatternAsync(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline |RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var redisKeys = _redisServer.Database.Multiplexer.GetServer("localhost:6379").Keys().ToList();
            var removeKeys = redisKeys.Where(k => k.ToString().Contains(regex.ToString())).ToList();
            foreach (var key in removeKeys)
            {
                await _redisServer.Database.KeyDeleteAsync(key);
            }

        }

        public async Task<bool> Exist(string key)
        {
            return await _redisServer.Database.KeyExistsAsync(key);
        }
    }
}
