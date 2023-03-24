using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.DistributedRedisCache
{
    public interface ICacheService
    {
        Task<bool> SetAsync(string key, List<object> values);
        Task<bool> SetStringAsync(string key, List<object> values);

        Task<List<object>> GetAsync(string key);
        Task<List<object>> GetStringAsync(string key);

        Task<bool> RemoveAsync(string key);
        Task<bool> RefreshAsync(string key);
    }
}
