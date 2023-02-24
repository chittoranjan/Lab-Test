using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository.IBaseRepository
{
    public interface IBaseRepository<T> where T : class
    {
        DbContext Db { get; set; }
        DbSet<T> Table => Db.Set<T>();

        #region Add
        bool Add(T entity);
        bool AddRange(ICollection<T> entities);
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(ICollection<T> entities);
        #endregion

        #region Update
        bool Update(T entity);
        bool UpdateRange(ICollection<T> entities);
        Task<bool> UpdateAsync(T entity);
        Task<bool> UpdateRangeAsync(ICollection<T> entities);
        #endregion

        #region AddOrUpdate
        bool AddOrUpdate(Expression<Func<T, object>> identifier, ICollection<T> entityCollections);
        Task<bool> AddOrUpdateAsync(Expression<Func<T, object>> identifier, ICollection<T> entityCollections);

        #endregion

        #region Remove
        bool Remove(T entity, bool isRemove);
        Task<bool> RemoveAsync(T entity, bool isRemove);
        bool RemoveRange(ICollection<T> entities, bool isRemove);
        Task<bool> RemoveRangeAsync(ICollection<T> entities, bool isRemove);
        #endregion

        #region GetById
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        #endregion

        #region GetAll
        ICollection<T> GetAll();
        Task<ICollection<T>> GetAllAsync();

        ICollection<T> Get(Expression<Func<T, bool>> predicate, bool isTracking = true);
        ICollection<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        ICollection<T> Get(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes);

        Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate, bool isTracking = true);
        Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes);

        #endregion

        #region GetFirstOrDefault
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate, bool isTracking = true);
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes);

        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTracking = true);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes);
        #endregion

        #region GetLastOrDefault
        T GetLastOrDefault(Expression<Func<T, bool>> predicate, bool isTracking = true);
        T GetLastOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        T GetLastOrDefault(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes);

        Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTracking = true);
        Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes);

        #endregion

        #region GetDeleted
        ICollection<T> GetDeleted(Expression<Func<T, bool>> predicate, bool isTracking = true);
        Task<List<T>> GetDeletedAsync(Expression<Func<T, bool>> predicate, bool isTracking = true);

        #endregion
    }
}
