using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCacheMaster.Api.Services
{
    public interface ICacheService
    {
        Task<T> GetCacheValueAsync<T>(string key);
        Task SetCacheValueAsync<T>(string key, object data, int expirationTime = 600);
        Task RemoveCacheValueAsync(string key);
        Task RemoveCacheByPatternAsync(string pattern);
        Task<bool> Exist(string key);
    }
}
