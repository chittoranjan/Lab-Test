using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.ModelContracts;
using Repository.IBaseRepository;

namespace Repository.BaseRepository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        public DbContext Db { get; set; }
        protected BaseRepository(DbContext db)
        {
            Db = db;
        }

        public DbSet<T> Table => Db.Set<T>();

        public virtual bool Add(T entity)
        {
            Table.Add(entity);
            return Db.SaveChanges() > 0;
        }

        public bool AddRange(ICollection<T> entities)
        {
            Table.AddRange(entities);
            return Db.SaveChanges() > 0;
        }

        public async Task<bool> AddAsync(T entity)
        {
            Table.Add(entity);
            return await Db.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddRangeAsync(ICollection<T> entities)
        {
            Table.AddRange(entities);
            return await Db.SaveChangesAsync() > 0;
        }

        public virtual bool Update(T entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            return Db.SaveChanges() > 0;
        }

        public bool UpdateRange(ICollection<T> entities)
        {
            Table.UpdateRange(entities);
            return Db.SaveChanges() > 0;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            return await Db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRangeAsync(ICollection<T> entities)
        {
            Table.UpdateRange(entities);
            return await Db.SaveChangesAsync() > 0;
        }

        public bool AddOrUpdate(Expression<Func<T, object>> identifier, ICollection<T> entityCollections)
        {
            var result = false;
            foreach (var entity in entityCollections)
            {
                result = AddOrUpdate(entity);
            }

            result = Db.SaveChanges() > 0;
            return result;
        }
        private bool AddOrUpdate(T entity)
        {
            var entityEntry = Db.Entry(entity);

            var primaryKeyName = entityEntry.Context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Select(c => c.Name).Single();

            var primaryKeyField = entity.GetType().GetProperty(primaryKeyName);

            var t = typeof(T);
            if (primaryKeyField == null)
            {
                throw new Exception($"{t.FullName} does not have a primary key specified. Unable to exec AddOrUpdate call.");
            }

            var keyVal = primaryKeyField.GetValue(entity);
            var dbVal = Table.Find(keyVal);

            if (dbVal != null)
            {
                Db.Entry(dbVal).CurrentValues.SetValues(entity);
                Table.Update(dbVal);
            }
            else
            {
                Table.Add(entity);
            }

            return Db.SaveChanges() > 0;
        }
        public virtual bool Remove(T entity, bool isRemove)
        {
            if (entity == null) { return false; }
            if (isRemove)
            { Table.Remove(entity); return Db.SaveChanges() > 0; }

            if (entity is IDelete model)
            {
                model.IsDelete = true;
                Update(entity);
            }

            return false;

        }

        public bool RemoveRange(ICollection<T> entities, bool isRemove)
        {
            if (entities == null || entities.Count <= 0) return false;
            if (isRemove)
            {
                Table.RemoveRange(entities);
            }

            foreach (var entity in entities)
            {
                if (entity is IDelete model) model.IsDelete = true;
            }

            var isDeleted = UpdateRange(entities);
            return isDeleted;
        }

        public virtual T GetById(int id)
        {
            return Table.FirstOrDefault(c => c.Id == id);

        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await Table.FirstOrDefaultAsync(c => c.Id == id);
        }

        public virtual ICollection<T> GetAll()
        {
            return Table.ToList();
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await Table.ToListAsync();
        }


        public virtual ICollection<T> Get(Expression<Func<T, bool>> predicate, bool isTracking = true)
        {
            return isTracking ? Table.Where(predicate).ToList() : Table.Where(predicate).AsNoTracking().ToList();
        }

        public virtual ICollection<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            if (includes == null || !includes.Any())
            {
                return Get(predicate);
            }

            var result = includes.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include), c => c.Where(predicate)).ToList();

            return result;
        }
        public virtual ICollection<T> Get(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes)
        {
            if (includes == null || !includes.Any())
            {
                return Get(predicate, isTracking);
            }
            var result = isTracking ? includes.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include), c => c.Where(predicate)).ToList() : includes.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include), c => c.Where(predicate)).AsNoTracking().ToList();
            return result;
        }

        public async Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate, bool isTracking = true)
        {
            return await (isTracking ? Table.Where(predicate).ToListAsync() : Table.Where(predicate).AsNoTracking().ToListAsync());
        }

        public async Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            if (includes == null || !includes.Any())
            {
                return await GetAsync(predicate);
            }

            var result = await includes.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include), c => c.Where(predicate)).ToListAsync();
            return result;
        }

        public async Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes)
        {
            if (includes == null || !includes.Any())
            {
                return await GetAsync(predicate, isTracking);
            }

            var result = await (isTracking ? includes.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include), c => c.Where(predicate)).ToListAsync() : includes.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include), c => c.Where(predicate)).AsNoTracking().ToListAsync());

            return result;
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate, bool isTracking = true)
        {
            return isTracking ? Table.FirstOrDefault(predicate) : Table.AsNoTracking().FirstOrDefault(predicate);
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            if (includes == null || !includes.Any())
            {
                return Table.FirstOrDefault(predicate);
            }

            var result = includes.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include), c => c.FirstOrDefault(predicate));

            return result;
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes)
        {
            if (includes == null || !includes.Any())
            {
                return GetFirstOrDefault(predicate, isTracking);
            }
            var result = isTracking ? includes.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include), c => c.FirstOrDefault(predicate)) : includes.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include), c => c.AsNoTracking().FirstOrDefault(predicate));
            return result;
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTracking = true)
        {
            return await (isTracking ? Table.FirstOrDefaultAsync(predicate) : Table.AsNoTracking().FirstOrDefaultAsync(predicate));
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            if (includes == null || !includes.Any())
            {
                return await Table.FirstOrDefaultAsync(predicate);
            }

            var result = await includes.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include), c => c.FirstOrDefaultAsync(predicate));
            return result;
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes)
        {
            if (includes == null || !includes.Any())
            {
                return await GetFirstOrDefaultAsync(predicate, isTracking);
            }

            var result = await (isTracking ? includes.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include), c => c.FirstOrDefaultAsync(predicate)) : includes.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include), c => c.AsNoTracking().FirstOrDefaultAsync(predicate)));
            return result;
        }

        public T GetLastOrDefault(Expression<Func<T, bool>> predicate, bool isTracking = true)
        {
            var result = isTracking ? Table.Where(predicate).OrderByDescending(c => c.Id).FirstOrDefault() : Table.Where(predicate).OrderByDescending(c => c.Id).AsNoTracking().FirstOrDefault();
            return result;
        }

        public T GetLastOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            if (includes == null || !includes.Any())
            {
                return GetLastOrDefault(predicate);
            }

            var result = includes.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include), c => c.OrderByDescending(entity => entity.Id).LastOrDefault(predicate));
            return result;
        }

        public T GetLastOrDefault(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes)
        {
            if (includes == null || !includes.Any())
            {
                return GetLastOrDefault(predicate, isTracking);
            }

            var result = isTracking ? includes.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include), c => c.OrderByDescending(entity => entity.Id).LastOrDefault(predicate)) : includes.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include), c => c.OrderByDescending(entity => entity.Id).AsNoTracking().LastOrDefault(predicate));
            return result;
        }

        public async Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTracking = true)
        {
            var result = await (isTracking ? Table.Where(predicate).OrderByDescending(c => c.Id).LastOrDefaultAsync() : Table.Where(predicate).OrderByDescending(c => c.Id).AsNoTracking().LastOrDefaultAsync());
            return result;
        }

        public async Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            if (includes == null || !includes.Any())
            {
                return await GetLastOrDefaultAsync(predicate);
            }
            var result = await includes.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include), c => c.OrderByDescending(entity => entity.Id).LastOrDefaultAsync(predicate));
            return result;
        }

        public async Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTracking = true, params Expression<Func<T, object>>[] includes)
        {
            if (includes == null || !includes.Any())
            {
                return await GetLastOrDefaultAsync(predicate, isTracking);
            }

            var result = await (isTracking ? includes.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include), c => c.OrderByDescending(entity => entity.Id).LastOrDefaultAsync(predicate)) : includes.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include), c => c.OrderByDescending(entity => entity.Id).AsNoTracking().LastOrDefaultAsync(predicate)));

            return result;
        }

        public ICollection<T> GetDeleted(Expression<Func<T, bool>> predicate, bool isTracking = true)
        {
            return isTracking ? Table.Where(predicate).IgnoreQueryFilters().ToList() : Table.Where(predicate).IgnoreQueryFilters().AsNoTracking().ToList();
        }

        public async Task<List<T>> GetDeletedAsync(Expression<Func<T, bool>> predicate, bool isTracking = true)
        {
            return await (isTracking ? Table.Where(predicate).IgnoreQueryFilters().ToListAsync() : Table.Where(predicate).IgnoreQueryFilters().AsNoTracking().ToListAsync());

        }


        public virtual void Dispose()
        {
            Db?.Dispose();
        }
    }
}
