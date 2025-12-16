using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Streetcode.BLL.Interfaces.Cache;

namespace Streetcode.BLL.Services.Cache
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(30);

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCacheService"/> class using the specified distributed cache.
        /// implementation.
        /// </summary>
        /// <param name="distributedCache">The distributed cache instance to be used for caching operations. Cannot be null.</param>
        public RedisCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        /// <summary>
        /// This method gets the value from cache by key and deserializes it to the specified type T.
        /// If cache is non-existent or empty, it returns default value of type T.
        /// I recommend to use in pair with SetAsync method for caching objects.
        /// </summary>
        /// <typeparam name="T">Type of returning object.</typeparam>
        /// <param name="key">Key for getting value.</param>
        /// <returns>Deserialized object of type T.</returns>
        public async Task<T?> GetAsync<T>(string key)
        {
            var cache = await _distributedCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cache))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(cache);
        }

        /// <summary>
        /// Serializes the given value and sets it in cache with the specified key.
        /// TTL can be set via absoluteExpiration parameter; if not provided, a default expiration time is used.
        /// Recommend to set adequate expiration time based on the nature of the cached data.
        /// </summary>
        /// <typeparam name="T">Type of returning object.</typeparam>
        /// <param name="key">Key for setting value.</param>
        /// <param name="value">Value that will be cached.</param>
        /// <param name="absoluteExpiration">Time, after which the cache will be automatically removed.</param>
        /// <returns>Nothing.</returns>
        public async Task SetAsync<T>(string key, T value, TimeSpan? absoluteExpiration = null)
        {
            string serialized = JsonSerializer.Serialize(value);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpiration ?? DefaultExpiration
            };

            await _distributedCache.SetStringAsync(key, serialized, cacheOptions);
        }

        /// <summary>
        /// Removes the cache entry for the specified key.
        /// Use for invalidation of data on Update/Remove operations, ensuring that stale data is not served from cache.
        /// Should be prioritized in scenarios where data consistency is critical.
        /// </summary>
        /// <param name="key">Key at which cache will be removed.</param>
        /// <returns>Nothing.</returns>
        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }
    }
}
