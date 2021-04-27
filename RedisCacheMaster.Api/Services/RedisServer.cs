using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace RedisCacheMaster.Api.Services
{
    public class RedisServer
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        public IDatabase Database { get; }
        private string _redisConnectionString { get; set; }
        private int _databaseId = 0;

        public RedisServer(IConfiguration configuration)
        {
            CreateRedisConnection(configuration);
            _connectionMultiplexer = ConnectionMultiplexer.Connect(_redisConnectionString);
            Database = _connectionMultiplexer.GetDatabase(_databaseId);
        }

        public void CreateRedisConnection(IConfiguration configuration)
        {
            var redisHost = configuration.GetSection("RedisConfig:Host").Value;
            _redisConnectionString = redisHost;
        }
    }
}
