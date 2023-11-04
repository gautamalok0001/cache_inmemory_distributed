using Microsoft.Extensions.Caching.Memory;
using System;

namespace in_memory_cache
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _memoryCache;
        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }
        public T Set<T>(string key, T data)
        {
            return _memoryCache.Set<T>(key, data);
        }
        public T SetSlidingExpirationInSeconds<T>(string key, T data, int slidingExpirationTime)
        {
            if (!_memoryCache.TryGetValue(key, out T cacheData))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(slidingExpirationTime));
                _memoryCache.Set(key, data, cacheEntryOptions);
            }
            return cacheData;
        }
        public T SetAbsoluteExpirationInSeconds<T>(string key, T data, int slidingExpirationTime, int absoluteExpirationTime)
        {
            if (!_memoryCache.TryGetValue(key, out T cacheData))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(slidingExpirationTime))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(absoluteExpirationTime)); ;
                _memoryCache.Set(key, data, cacheEntryOptions);
            }
            return cacheData;
        }
        public T SetSlidingExpirationInMinutes<T>(string key, T data, int slidingExpirationTime)
        {
            if (!_memoryCache.TryGetValue(key, out T cacheData))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(slidingExpirationTime));
                _memoryCache.Set(key, data, cacheEntryOptions);
            }
            return cacheData;
        }
        public T SetAbsoluteExpirationInMinutes<T>(string key, T data, int slidingExpirationTime, int absoluteExpirationTime)
        {
            if (!_memoryCache.TryGetValue(key, out T cacheData))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(slidingExpirationTime))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(absoluteExpirationTime)); ;
                _memoryCache.Set(key, data, cacheEntryOptions);
            }
            return cacheData;
        }
        public T SetSlidingExpirationInHours<T>(string key, T data, int slidingExpirationTime)
        {
            if (!_memoryCache.TryGetValue(key, out T cacheData))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(slidingExpirationTime));
                _memoryCache.Set(key, data, cacheEntryOptions);
            }
            return cacheData;
        }
        public T SetAbsoluteExpirationInHours<T>(string key, T data, int slidingExpirationTime, int absoluteExpirationTime)
        {
            if (!_memoryCache.TryGetValue(key, out T cacheData))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(slidingExpirationTime))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(absoluteExpirationTime)); ;
                _memoryCache.Set(key, data, cacheEntryOptions);
            }
            return cacheData;
        }
        public T SetSlidingExpirationInDays<T>(string key, T data, int slidingExpirationTime)
        {
            if (!_memoryCache.TryGetValue(key, out T cacheData))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(slidingExpirationTime));
                _memoryCache.Set(key, data, cacheEntryOptions);
            }
            return cacheData;
        }
        public T SetAbsoluteExpirationInDays<T>(string key, T data, int slidingExpirationTime, int absoluteExpirationTime)
        {
            if (!_memoryCache.TryGetValue(key, out T cacheData))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(slidingExpirationTime))
                    .SetAbsoluteExpiration(TimeSpan.FromDays(absoluteExpirationTime)); ;
                _memoryCache.Set(key, data, cacheEntryOptions);
            }
            return cacheData;
        }
        public bool Remove<T>(string key)
        {
            if (_memoryCache.TryGetValue(key, out T cacheData))
            {
                _memoryCache.Remove(key);
                return true;
            }
            return false;
        }
    }
}
