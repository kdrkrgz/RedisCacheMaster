using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace RedisCacheMaster.Api.DependencyResolvers
{
    public interface ICoreModule
    {
        void Load(IServiceCollection services);
    }
}
