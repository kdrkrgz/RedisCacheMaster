using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RedisCacheMaster.Api.DependencyResolvers;
using RedisCacheMaster.Api.Services;
using RedisCacheMaster.Api.Utilities;

namespace RedisCacheMaster.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddDependencyResolvers
            (this IServiceCollection services)
        {
            services.AddSingleton<RedisServer>();
            services.AddSingleton<ICacheService, RedisCacheManager>();
            return ServiceTool.Create(services);
        }
    }
}
