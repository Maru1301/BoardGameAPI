using BoardGame.Providers.Interfaces;
using BoardGame.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace BoardGame.Services
{
    public class CacheService(IMemoryCache memoryCache) : ICacheService, IService
    {
        private readonly IMemoryCache memoryCache = memoryCache;

        public T? Get<T>(object key) where T : class, new()
        {
            T? result;
            if (memoryCache is ICacheProvider cacheProvider)
            {
                result = cacheProvider.Get<T>(key);
            }
            else
            {
                memoryCache.TryGetValue(key, out result);
            }

            return result;
        }

        public void Set<T>(string key, T value, TimeSpan timeSpan) where T : class, new()
        {
            memoryCache.Set(key, value, new DateTimeOffset(DateTime.Now.Add(timeSpan)));
        }

        public void Remove<T>(object key)
        {
            memoryCache.Remove(key);
        }
    }
}
