using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedisCacheMaster.Api.Models;

namespace RedisCacheMaster.Api.Controllers
{
    public interface ICacheController
    {
        List<Product> GetProducts();
    }
}
