using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.DistributedRedisCache
{
    public class CacheService : ICacheService
    {
        public readonly IDistributedCache DistributedCache;

        public CacheService(IDistributedCache iDistributedCache)
        {
            DistributedCache = iDistributedCache;
        }

        #region Set
        public async Task<bool> SetAsync<T>(string key, List<T> values)
        {
            if (values is not { Count: > 0 }) return false;

            string serializeList = JsonConvert.SerializeObject(values);
            byte[] encodedList = Encoding.UTF8.GetBytes(serializeList);
            var option = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(30))
                .SetAbsoluteExpiration(TimeSpan.FromHours(6));
            await DistributedCache.SetAsync(key, encodedList, option);
            return true;
        }
        public async Task<bool> SetStringAsync<T>(string key, List<T> values)
        {
            if (values is not { Count: > 0 }) return false;

            string serializeList = JsonConvert.SerializeObject(values);
            var option = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(6));
            await DistributedCache.SetStringAsync(key, serializeList, option);
            return true;


        }
        #endregion

        #region Get
        public async Task<List<T>> GetAsync<T>(string key)
        {
            var data = new List<T>();
            byte[] encodedList = await DistributedCache.GetAsync(key);
            if (encodedList == null) return data;
            string serializeList = Encoding.UTF8.GetString(encodedList);
            data = JsonConvert.DeserializeObject<List<T>>(serializeList);
            return data;
        }

        public async Task<List<T>> GetStringAsync<T>(string key)
        {
            var data = new List<T>();
            string serializeList = await DistributedCache.GetStringAsync(key);
            if (serializeList != null)
            {
                data = JsonConvert.DeserializeObject<List<T>>(serializeList);
            }
            return data;
        }

        #endregion

        #region Refresh
        public async Task<bool> RefreshAsync(string key)
        {
            if (key == null) return false;
            await DistributedCache.RefreshAsync(key);
            return true;
        }
        #endregion

        #region Remove
        public async Task<bool> RemoveAsync(string key)
        {
            if (key == null) return false;
            await DistributedCache.RemoveAsync(key);
            return true;
        }

        #endregion
    }
}
