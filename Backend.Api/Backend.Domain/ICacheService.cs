using System;

namespace Backend.Domain
{
    public interface ICacheService
    {
        T Get<T>(string entry, int cacheForSeconds, Func<T> obtainFunc);
    }
}