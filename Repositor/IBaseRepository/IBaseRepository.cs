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
        bool Add(T entity);
        bool AddRange(ICollection<T> entities);
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(ICollection<T> entities);
        bool Update(T entity);
        bool UpdateRange(ICollection<T> entities);
        Task<bool> UpdateAsync(T entity);
        Task<bool> UpdateRangeAsync(ICollection<T> entities);
        bool AddOrUpdate(Expression<Func<T, object>> identifier, ICollection<T> entityCollections);

        bool Remove(T entity, bool isRemove);
        bool RemoveRange(ICollection<T> entities, bool isRemove);

        T GetById(int id);
        Task<T> GetByIdAsync(int id);

        ICollection<T> GetAll();
        Task<ICollection<T>> GetAllAsync();

        ICollection<T> Get(Expression<Func<T, bool>> predicate, bool isTracking = true);
        ICollection<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        ICollection<T> Get(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes);

        Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate, bool isTracking = true);
        Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes);

        T GetFirstOrDefault(Expression<Func<T, bool>> predicate, bool isTracking = true);
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes);

        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTracking = true);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes);


        T GetLastOrDefault(Expression<Func<T, bool>> predicate, bool isTracking = true);
        T GetLastOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        T GetLastOrDefault(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes);

        Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTracking = true);
        Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes);

        ICollection<T> GetDeleted(Expression<Func<T, bool>> predicate, bool isTracking = true);
        Task<List<T>> GetDeletedAsync(Expression<Func<T, bool>> predicate, bool isTracking = true);

    }
}
