using NRedisStack;
using NRedisStack.RedisStackCommands;
using NRedisStack.Search;
using NRedisStack.Search.Aggregation;
using NRedisStack.Search.Literals.Enums;
using Redis.OM;
using Redis.OM.Aggregation;
using StackExchange.Redis;
using System.Data.Common;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;
// https://developer.redis.com/create/windows/
// https://redis.io/docs/data-types/json/#command-line-option
// sudo service redis-server restart
// redis-cli
// sudo nano /etc/redis/redis.conf
// https://redis.com/blog/redis-om-net/
namespace distributed_memory_cache
{
    public class DistributedRedisCache : IDistributedRedisCache
    {
        private readonly IDatabase db;
        private readonly RedisConnectionProvider provider;
        public DistributedRedisCache()
        {
            // Redis OM // Nuget Package Manager
            if(provider == null)
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379,abortConnect=false,connectTimeout=30000,responseTimeout=30000");
                provider = new RedisConnectionProvider(redis);
            }
             //Normal Redis Mostly with the redis-cli
            //if(db == null)
            //{
            //    ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379,abortConnect=false,connectTimeout=30000,responseTimeout=30000");
            //    db = redis.GetDatabase();
            //}

        }
        public string Test()
        {
            if(db != null)
            {
                db.StringSet("foo", "bar");
                return db.StringGet("foo").ToString() ?? "";
            }
            return "";
        }
        public string TestHashMap()
        {
            if (db != null)
            {
                var hash = new HashEntry[] 
                {
                    new HashEntry("name", "Jhon"),
                    new HashEntry("surname", "Smith"),
                    new HashEntry("company", "Redis"),
                    new HashEntry("age", "29"),
                };
                db.HashSet("user-session:123", hash);
                var hashFields = db.HashGetAll("user-session:123");
                return string.Join("; ", hashFields);
            }
            return "";
        }
        public string TestRedisCluster()
        {
            if (db != null)
            {
                ISearchCommands ft = db.FT();
                var user1 = new
                {
                    name = "Paul John",
                    email = "paul.john@example.com",
                    age = 42,
                    city = "London"
                };

                var user2 = new
                {
                    name = "Eden Zamir",
                    email = "eden.zamir@example.com",
                    age = 29,
                    city = "Tel Aviv"
                };

                var user3 = new
                {
                    name = "Paul Zamir",
                    email = "paul.zamir@example.com",
                    age = 35,
                    city = "Tel Aviv"
                };
                var schema = new Schema()
                    .AddTextField(new FieldName("$.name", "name"))
                    .AddTagField(new FieldName("$.city", "city"))
                    .AddNumericField(new FieldName("$.age", "age"));
                
                var request1 = new AggregationRequest("*");
                var result1 = ft.Aggregate("idx:users", request1);


                ft.Create(
                   "idx:users",
                   new FTCreateParams().On(IndexDataType.JSON).Prefix("user:"),
                   schema
                   );

                IJsonCommands json = db.JSON();
                json.Set("user:1", "$", user1);
                json.Set("user:2", "$", user2);
                json.Set("user:3", "$", user3);
                
                //find the user paul and filter the results by age
                var res = ft.Search("idx:users", new Query("Paul @age:[30,40]")).Documents.Select(x => x["json"]);
                var findTheUserPaul = string.Join("\n", res);

                //return only the city field
                var res_cities = ft.Search("idx:users", new Query("Paul").ReturnFields(new FieldName("$.city", "city"))).Documents.Select(x => x["city"]);
                var theCityField = string.Join(',', res_cities);

                //count all users in the same city
                var request = new AggregationRequest("*").GroupBy("@city", Reducers.Count().As("count"));
                var result = ft.Aggregate("idx:users", request);

                for (int i = 0; i< result.TotalResults; i++)
                {
                    var row = result.GetRow(i);
                }

                return findTheUserPaul;
            }
            return "";
        }
        public async Task<Customer> AddCustomer()
        {
            try
            {
                if (provider != null)
                {
                    var bob = new Customer
                    {
                        Id = Guid.NewGuid(),
                        Age = 35,
                        Email = "foo@bar.com",
                        FirstName = "Bob",
                        LastName = "Smith"
                    };
                    var idxInfo = await provider.Connection.GetIndexInfoAsync(typeof(Customer));
                    if (idxInfo == null)
                    {
                        await provider.Connection.CreateIndexAsync(typeof(Customer));
                    }
                    var customers = provider.RedisCollection<Customer>();
                    var customerId = await customers.InsertAsync(bob);
                    var alsoBob = await customers.FirstOrDefaultAsync(x => x.Email == "foo@bar.com");
                    return alsoBob;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public async Task<Customer> UpdateCustomer()
        {
            try
            {
                if (provider != null)
                {
                    var idxInfo = await provider.Connection.GetIndexInfoAsync(typeof(Customer));
                    if (idxInfo == null)
                    {
                        await provider.Connection.CreateIndexAsync(typeof(Customer));
                    }
                    var customers = provider.RedisCollection<Customer>();
                    var alsoBob = await customers.FirstOrDefaultAsync(x => x.Email == "foo@bar.com");
                    if(alsoBob != null)
                    {
                        alsoBob.Email = "alok@bar.com";
                        await customers.UpdateAsync(alsoBob);
                    }
                    var updatedBob = await customers.FirstOrDefaultAsync(x => x.Email == "alok@bar.com");
                    return updatedBob;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public async Task<Customer> DeleteCustomer()
        {
            try
            {
                if (provider != null)
                {
                    var idxInfo = await provider.Connection.GetIndexInfoAsync(typeof(Customer));
                    if (idxInfo == null)
                    {
                        await provider.Connection.CreateIndexAsync(typeof(Customer));
                    }
                    var customers = provider.RedisCollection<Customer>();
                    var alsoBob = await customers.FirstOrDefaultAsync(x => x.Email == "alok@bar.com");
                    if (alsoBob != null)
                    {
                        await customers.DeleteAsync(alsoBob);
                    }
                    var updatedBob = await customers.FirstOrDefaultAsync(x => x.Email == "alok@bar.com");
                    return updatedBob;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        public async Task<Customer?> TestModelRedisOM()
        {
            try
            {
                if (provider != null)
                {
                    var bob = new Customer
                    {
                        Id = Guid.NewGuid(),
                        Age = 35,
                        Email = "foo@bar.com",
                        FirstName = "Bob",
                        LastName = "Smith"
                    };
                    var idxInfo = await provider.Connection.GetIndexInfoAsync(typeof(Customer));
                    if (idxInfo == null)
                    {
                        await provider.Connection.CreateIndexAsync(typeof(Customer));
                    }
                    var customers = provider.RedisCollection<Customer>();
                    var customerId = await customers.InsertAsync(bob);
                    var alsoBob = await customers.FirstOrDefaultAsync(x => x.Email == "foo@bar.com");
                    //var collection = new RedisAggregationSet<Customer>(provider.Connection);
                    await customers.DeleteAsync(alsoBob);
                    //var alsoBob = await customers.FindByIdAsync(customerId);
                    //await customers.DeleteAsync(alsoBob);
                    //var afterDeleteResult = await customers.FirstOrDefaultAsync(x=> x.Email == "foo@bar.com");
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}
