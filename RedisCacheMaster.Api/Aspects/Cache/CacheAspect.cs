using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RedisCacheMaster.Api.Models;
using RedisCacheMaster.Api.Services;
using RedisCacheMaster.Api.Utilities;
using RedisCacheMaster.Api.Utilities.Interception;
using RedisCacheMaster.Api.Utilities.Results;

namespace RedisCacheMaster.Api.Aspects.Cache
{
    public class CacheAspect : MethodInterception 
    {

        private readonly int _expirationTime;
        private readonly ICacheService _cacheManager;

        public CacheAspect(int expirationTime = 600)
        {
            _expirationTime = expirationTime;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheService>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            if (_cacheManager.Exist(key).Result)
            {
                var cacheData = _cacheManager.GetCacheValueAsync<List<Product>>(key);
                // Result olarak genel bir tür içinde data dönülürse sorun kalmayacak.
                var result = cacheData.Result;

                invocation.ReturnValue = result;
                return;
            }
            invocation.Proceed();
            _cacheManager.SetCacheValueAsync<object>(key, invocation.ReturnValue, _expirationTime);
        }

    }
}
