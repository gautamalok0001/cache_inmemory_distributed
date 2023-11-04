using distributed_memory_cache;
using memory_cache.bal.entries;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace memory_cache.api.Controllers
{
    [Route("api/testdata")]
    [ApiController]
    public class TestDataController : ControllerBase
    {

        //Some common command
        //sudo cp /home/alokubuntu/redis/RedisJSON/bin/linux-x64-release/rejson.so /var/lib/redis/modules/.
        //loadmodule /var/lib/redis/modules/rejson.so
        //git clone --recursive https://github.com/RedisJSON/RedisJSON.git --branch v2.6.6
        //curl https://sh.rustup.rs -sSf | sh
        //Delete the redis - cli keys
        //redis-cli KEYS "user*" | xargs redis-cli DEL

        private readonly ITestData _testData;
        private readonly IDistributedRedisCache _redisCache;
        public TestDataController(ITestData testData, IDistributedRedisCache redisCache)
        {
            _testData = testData;
            _redisCache = redisCache;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var result = _redisCache.Test();
            return Ok(result);
        }
        [HttpGet]
        [Route("hashmap")]
        public IActionResult GetHashMap()
        {
            var result = _redisCache.TestHashMap();
            return Ok(result);
        }
        [HttpGet]
        [Route("rediscluster")]
        public IActionResult GetRedisCluster()
        {
            var result = _redisCache.TestRedisCluster();
            return Ok(result);
        }
        [HttpGet("id")]
        public IActionResult GetById()
        {
            var result = _testData.GetCustomerVesion2();
            return Ok(result);
        }
        [HttpGet]
        [Route("redisom/model/add")]
        public async Task<IActionResult> AddRedisOMModel()
        {
            var result = await _redisCache.AddCustomer();
            return Ok(result);
        }
        [HttpGet]
        [Route("redisom/model/update")]
        public async Task<IActionResult> UpdateRedisOMModel()
        {
            var result = await _redisCache.UpdateCustomer();
            return Ok(result);
        }
        [HttpGet]
        [Route("redisom/model/delete")]
        public async Task<IActionResult> DeleteRedisOMModel()
        {
            var result = await _redisCache.DeleteCustomer();
            return Ok(result);
        }
        [HttpGet]
        [Route("redisom/model/mapping")]
        public async Task<IActionResult> GetRedisOMModel()
        {
            var result = await _redisCache.TestModelRedisOM();
            return Ok(result);
        }
    }
}
