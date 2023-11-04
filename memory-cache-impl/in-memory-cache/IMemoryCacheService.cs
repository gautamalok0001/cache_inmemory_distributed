namespace in_memory_cache
{
    public interface IMemoryCacheService
    {
        public T Get<T>(string key);
        public T Set<T>(string key, T data);
        public T SetSlidingExpirationInSeconds<T>(string key, T data, int slidingExpirationTime);
        public T SetAbsoluteExpirationInSeconds<T>(string key, T data, int slidingExpirationTime, int absoluteExpirationTime);
        public T SetSlidingExpirationInMinutes<T>(string key, T data, int slidingExpirationTime);
        public T SetAbsoluteExpirationInMinutes<T>(string key, T data, int slidingExpirationTime, int absoluteExpirationTime);
        public T SetSlidingExpirationInHours<T>(string key, T data, int slidingExpirationTime);
        public T SetAbsoluteExpirationInHours<T>(string key, T data, int slidingExpirationTime, int absoluteExpirationTime);
        public T SetSlidingExpirationInDays<T>(string key, T data, int slidingExpirationTime);
        public T SetAbsoluteExpirationInDays<T>(string key, T data, int slidingExpirationTime, int absoluteExpirationTime);
        public bool Remove<T>(string key);
    }
}
