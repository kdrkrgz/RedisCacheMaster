using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RedisCacheMaster.Api.Services;

namespace RedisCacheMaster.Api.DependencyResolvers
{
    public class CoreModule: ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddSingleton<RedisServer>();
            services.AddSingleton<ICacheService, RedisCacheManager>();
        }
    }
}
