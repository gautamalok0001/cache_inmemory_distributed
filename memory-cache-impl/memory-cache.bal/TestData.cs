using in_memory_cache;
using memory_cache.bal.entries;
using memory_cache.Data;

namespace memory_cache.bal
{
    public class TestData : ITestData
    {
        private readonly IMemoryCacheService _memoryCacheService;
        public TestData(IMemoryCacheService memoryCacheService)
        {
            _memoryCacheService = memoryCacheService;
        }
        public List<Customer>? Get()
        {
            var getCacheValue = _memoryCacheService.Get<List<Customer>>(CommonConstant.TestKey);
            if (getCacheValue == null)
            {
                getCacheValue = TestDumpData.customers;
                _memoryCacheService.SetSlidingExpirationInSeconds<List<Customer>>(CommonConstant.TestKey, getCacheValue, 60);
            }
            return getCacheValue;
        }
        public List<Customer>? GetCustomerVesion2()
        {
            var getCacheValue = _memoryCacheService.Get<List<Customer>>(CommonConstant.TestKey1);
            if (getCacheValue == null)
            {
                getCacheValue = TestDumpData.customers;
                _memoryCacheService.Set<List<Customer>>(CommonConstant.TestKey1, getCacheValue);
            }
            return getCacheValue;
        }

    }
}