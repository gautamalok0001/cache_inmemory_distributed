using System.Threading.Tasks;

namespace distributed_memory_cache
{
    public interface IDistributedRedisCache
    {
        public string Test();
        public string TestHashMap();
        public string TestRedisCluster();
        public Task<Customer?> TestModelRedisOM();
        public Task<Customer> AddCustomer();
        public Task<Customer> UpdateCustomer();
        public Task<Customer> DeleteCustomer();
    }
}
