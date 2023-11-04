using Redis.OM.Modeling;
using System;

namespace distributed_memory_cache
{
    [Document]
    public class Customer
    {
        [RedisIdField][RedisField][Indexed] public Guid Id { get; set; }
        [Indexed(Sortable = true)] public string FirstName { get; set; }
        [Indexed(Sortable = true)] public string LastName { get; set; }
        [Indexed(Sortable = true)] public string Email { get; set; }
        [Indexed(Sortable = true)] public int Age { get; set; }
    }
}
