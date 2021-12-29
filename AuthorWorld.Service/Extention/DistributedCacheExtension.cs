using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text;
using System.Text.Json;

namespace AuthorWorld.Service.Extention
{
    public static class DistributedCacheExtension
    {
        public static void AddCache<TObject>(this IDistributedCache cache, 
            string key, 
            TObject cacheData, 
            TimeSpan? absoluteExpiration = null, 
            TimeSpan? slidingExpiration = null)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (cacheData == null) throw new ArgumentNullException(nameof(cacheData));

            string value = JsonSerializer.Serialize<TObject>(cacheData, new JsonSerializerOptions() { ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve});

            DistributedCacheEntryOptions opt = new DistributedCacheEntryOptions();
            opt.AbsoluteExpirationRelativeToNow = absoluteExpiration;
            opt.SlidingExpiration = slidingExpiration;

            cache.SetString(key, value, opt);
        }

        public static TObject GetCache<TObject>(this IDistributedCache cache,
            string key
            )
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            string value = cache.GetString(key);

            if (string.IsNullOrWhiteSpace(value)) return default(TObject);

            return JsonSerializer.Deserialize<TObject>(value, new JsonSerializerOptions() { ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve });

        }
    }
}
