using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RedisCacheMaster.Api.DependencyResolvers;
using RedisCacheMaster.Api.Utilities;

namespace RedisCacheMaster.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddDependencyResolvers
            (this IServiceCollection serviceCollection, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }

            return ServiceTool.Create(serviceCollection);
        }
    }
}
