using memory_cache.Data;

namespace memory_cache.bal.entries
{
    public interface ITestData
    {
        public List<Customer>? Get();
        public List<Customer>? GetCustomerVesion2();
    }
}
