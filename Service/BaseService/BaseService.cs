using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Repository.IBaseRepository;
using Service.IBaseService;

namespace Service.BaseService
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        private IBaseRepository<T> Repository { get; set; }

        protected BaseService(IBaseRepository<T> iBaseRepository)
        {
            Repository = iBaseRepository;
        }
        public virtual bool Add(T entity)
        {
            return Repository.Add(entity);
        }

        public virtual bool AddRange(ICollection<T> entities)
        {
            return Repository.AddRange(entities);
        }

        public virtual async Task<bool> AddAsync(T entity)
        {
            return await Repository.AddAsync(entity);
        }

        public virtual async Task<bool> AddRangeAsync(ICollection<T> entities)
        {
            return await Repository.AddRangeAsync(entities);
        }

        public virtual bool Update(T entity)
        {
            return Repository.Update(entity);
        }

        public virtual bool UpdateRange(ICollection<T> entities)
        {
            return Repository.UpdateRange(entities);
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            return await Repository.UpdateAsync(entity);
        }

        public virtual async Task<bool> UpdateRangeAsync(ICollection<T> entities)
        {
            return await Repository.UpdateRangeAsync(entities);
        }

        public virtual bool AddOrUpdate(Expression<Func<T, object>> identifier, ICollection<T> entityCollections)
        {
            return Repository.AddOrUpdate(identifier, entityCollections);
        }

        public virtual bool Remove(T entity, bool isRemove)
        {
            return Repository.Remove(entity, isRemove);
        }

        public virtual bool RemoveRange(ICollection<T> entities, bool isRemove)
        {
            return Repository.RemoveRange(entities, isRemove);
        }

        public virtual T GetById(int id)
        {
            return Repository.GetById(id);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await Repository.GetByIdAsync(id);
        }

        public virtual ICollection<T> GetAll()
        {
            return Repository.GetAll();
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await Repository.GetAllAsync();
        }

        public virtual ICollection<T> Get(Expression<Func<T, bool>> predicate, bool isTracking = true)
        {
            return Repository.Get(predicate, isTracking);
        }

        public virtual ICollection<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return Repository.Get(predicate, includes);
        }

        public virtual ICollection<T> Get(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes)
        {
            return Repository.Get(predicate, isTracking, includes);
        }

        public virtual async Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate, bool isTracking = true)
        {
            return await Repository.GetAsync(predicate, isTracking);
        }

        public virtual async Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return await Repository.GetAsync(predicate, includes);
        }

        public virtual async Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes)
        {
            return await Repository.GetAsync(predicate, isTracking, includes);
        }

        public virtual T GetFirstOrDefault(Expression<Func<T, bool>> predicate, bool isTracking = true)
        {
            return Repository.GetFirstOrDefault(predicate, isTracking);
        }

        public virtual T GetFirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return Repository.GetFirstOrDefault(predicate, includes);
        }

        public virtual T GetFirstOrDefault(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes)
        {
            return Repository.GetFirstOrDefault(predicate, isTracking, includes);
        }

        public virtual async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTracking = true)
        {
            return await Repository.GetFirstOrDefaultAsync(predicate, isTracking);
        }

        public virtual async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return await Repository.GetFirstOrDefaultAsync(predicate, includes);
        }

        public virtual async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes)
        {
            return await Repository.GetFirstOrDefaultAsync(predicate, isTracking, includes);
        }

        public virtual T GetLastOrDefault(Expression<Func<T, bool>> predicate, bool isTracking = true)
        {
            return Repository.GetLastOrDefault(predicate, isTracking);
        }

        public virtual T GetLastOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return Repository.GetLastOrDefault(predicate, includes);
        }

        public virtual T GetLastOrDefault(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes)
        {
            return Repository.GetLastOrDefault(predicate, isTracking, includes);
        }

        public virtual async Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTracking = true)
        {
            return await Repository.GetLastOrDefaultAsync(predicate, isTracking);
        }

        public virtual async Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return await Repository.GetLastOrDefaultAsync(predicate, includes);
        }

        public virtual async Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes)
        {
            return await Repository.GetLastOrDefaultAsync(predicate, isTracking, includes);
        }

        public virtual ICollection<T> GetDeleted(Expression<Func<T, bool>> predicate, bool isTracking = true)
        {
            return Repository.GetDeleted(predicate, isTracking);
        }

        public virtual async Task<List<T>> GetDeletedAsync(Expression<Func<T, bool>> predicate, bool isTracking = true)
        {
            return await Repository.GetDeletedAsync(predicate, isTracking);
        }
    }
}
