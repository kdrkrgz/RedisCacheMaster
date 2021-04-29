using Castle.DynamicProxy;
using RedisCacheMaster.Api.Services;
using RedisCacheMaster.Api.Utilities;
using RedisCacheMaster.Api.Utilities.Interception;

namespace RedisCacheMaster.Api.Aspects.Cache
{
    public class RemoveCacheAspect: MethodInterception
    {
        private string _pattern;
        private ICacheService _cacheManager;

        public RemoveCacheAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = (ICacheService)ServiceTool.ServiceProvider.GetService(typeof(ICacheService));
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveCacheByPatternAsync(_pattern);
        }
    }


}
