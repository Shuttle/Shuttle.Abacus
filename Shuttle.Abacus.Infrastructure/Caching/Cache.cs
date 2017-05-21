using System;
using System.Runtime.Caching;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Infrastructure.Caching
{
    public class Cache<T>
    {
        private readonly CacheItemPolicy _policy = new CacheItemPolicy {  AbsoluteExpiration = DateTimeOffset.MaxValue };
        private readonly MemoryCache _cache;
        private readonly object _lock = new object();

        public Cache(string name)
        {
            _cache = new MemoryCache(name);
        }

        public T Get(string key, Func<T> get)
        {
            Guard.AgainstNullOrEmptyString(key, "key");

            if (_cache.Contains(key))
            {
                return (T)_cache[key];
            }

            lock (_lock)
            {
                if (_cache.Contains(key))
                {
                    return (T)_cache[key];
                }

                _cache.Add(new CacheItem(key, get.Invoke()), _policy);

                return (T)_cache[key];
            }
        }

        public void Trim(int percent)
        {
            _cache.Trim(percent);
        }
    }
}