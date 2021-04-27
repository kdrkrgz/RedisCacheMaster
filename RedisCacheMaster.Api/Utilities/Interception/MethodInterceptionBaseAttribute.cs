using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace RedisCacheMaster.Api.Utilities.Interception
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class MethodInterceptionBaseAttribute: Attribute, IInterceptor
    {
        public int Priorty { get; set; }

        public virtual void Intercept(IInvocation invocation)
        {
        }
    }
}
