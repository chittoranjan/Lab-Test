using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.DistributedRedisCache
{
    public class DistributedRedisCacheService
    {
        public readonly IDistributedCache _iDistributedCashe;

        public DistributedRedisCacheService(IDistributedCache iDistributedCashe)
        {
            _iDistributedCashe = iDistributedCashe;
        }

        #region Set
        public async Task<bool> SetAsync(string key, List<object> values)
        {
            if (values == null || values.Count <= 0) return false;

            string serializeList = JsonConvert.SerializeObject(values);
            byte[] encodedList = Encoding.UTF8.GetBytes(serializeList);
            var option = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(30))
                .SetAbsoluteExpiration(TimeSpan.FromHours(6));
            await _iDistributedCashe.SetAsync(key, encodedList, option);
            return true;
        }
        public async Task<bool> SetStringAsync(string key, List<object> values)
        {
            if (values == null || values.Count <= 0) return false;

            string serializeList = JsonConvert.SerializeObject(values);
            var option = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(6));
            await _iDistributedCashe.SetStringAsync(key, serializeList, option);
            return true;


        }
        #endregion

        #region Get
        public async Task<List<object>> GetAsync(string key)
        {
            var data = new List<object>();
            byte[] encodedList = await _iDistributedCashe.GetAsync(key);
            if (encodedList != null)
            {
                string serializeList = Encoding.UTF8.GetString(encodedList);
                data = JsonConvert.DeserializeObject<List<object>>(serializeList);
            }
            return data;
        }

        public async Task<List<object>> GetStringAsync(string key)
        {
            var data = new List<object>();
            string serializeList = await _iDistributedCashe.GetStringAsync(key);
            if (serializeList != null)
            {
                data = JsonConvert.DeserializeObject<List<object>>(serializeList);
            }
            return data;
        }

        #endregion

        #region Refresh
        public async Task<bool> RefreshAsync(string key)
        {
            if (key == null) return false;
            await _iDistributedCashe.RefreshAsync(key);
            return true;
        }
        #endregion

        #region Remove
        public async Task<bool> RemoveAsync(string key)
        {
            if (key == null) return false;
            await _iDistributedCashe.RemoveAsync(key);
            return true;
        }

        #endregion
    }
}
