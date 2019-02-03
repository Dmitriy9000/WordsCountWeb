using System;
using System.Runtime.Caching;

namespace Backend.Domain
{
    public class CacheService : ICacheService
    {
        public T Get<T>(
            string entry, 
            int cacheForSeconds, 
            Func<T> obtainFunc)
        {
            ObjectCache cache = MemoryCache.Default;
            var cachedObject = (T)cache[entry];
            if (cachedObject == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(cacheForSeconds);
                cachedObject = obtainFunc();
                cache.Set(entry, cachedObject, policy);
            }
            return cachedObject;
        }
    }
}
