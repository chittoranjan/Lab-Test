using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.DistributedRedisCache
{
    public interface ICacheService
    {
        Task<bool> SetAsync<T>(string key, List<T> values);
        Task<bool> SetStringAsync<T>(string key, List<T> values);

        Task<List<T>> GetAsync<T>(string key);
        Task<List<T>> GetStringAsync<T>(string key);

        Task<bool> RemoveAsync(string key);
        Task<bool> RefreshAsync(string key);
    }
}
