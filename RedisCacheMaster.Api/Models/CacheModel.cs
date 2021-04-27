using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCacheMaster.Api.Models
{
    public class CacheModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
