using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using RedisCacheMaster.Api.Business.Abstract;
using RedisCacheMaster.Api.Business.Concrete;
using RedisCacheMaster.Api.Controllers;
using RedisCacheMaster.Api.Services;
using RedisCacheMaster.Api.Utilities;
using RedisCacheMaster.Api.Utilities.Interception;

namespace RedisCacheMaster.Api.DependencyResolvers
{
    public class AutofacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<ProductRepository>().As<IProductRepository>().SingleInstance();
            //builder.RegisterType<CacheController>().SingleInstance();
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }

    }
}
