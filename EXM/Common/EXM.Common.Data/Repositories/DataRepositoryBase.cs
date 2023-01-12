using EXM.Common.Data.Contracts;
using EXM.Common.Data.Extensions;
using EXM.Common.Data.Models;
using EXM.Common.Data.Specifications.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EXM.Common.Data.Repositories
{
    public abstract class DataRepositoryBase<T, U> : IDisposable, 
        IDataRepository<T> where T : class,
        IEntity, new() where U : DbContext
    {
        private U _context = null;

        protected U Context
        {
            get { return _context; }
        }

        public DataRepositoryBase(U context)
        {
            _context = context;
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public T Add(T entity)
        {
            T result = AddEntity(Context, entity);
            Context.SaveChanges();
            SetState(result, EntityState.Detached);
            return result;
        }

        public async Task<T> AddAsync(T entity)
        {
            T result = AddEntity(Context, entity);
            await Context.SaveChangesAsync();
            SetState(result, EntityState.Detached);
            return result;
        }

        protected virtual T AddEntity(U context, T entity)
        {
            SetState(entity, EntityState.Added);
            return entity;
        }

        public IEnumerable<T> Add(IEnumerable<T> entities)
        {
            var results = AddEntities(Context, entities);
            Context.SaveChanges();
            SetState(results, EntityState.Detached);
            return results;
        }

        public async Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities)
        {
            var results = AddEntities(Context, entities);
            await Context.SaveChangesAsync();
            SetState(results, EntityState.Detached);
            return results;
        }

        protected virtual IEnumerable<T> AddEntities(U context, IEnumerable<T> entities)
        {
            SetState(entities, EntityState.Added);
            return entities;
        }

        public IEnumerable<T> Remove(Expression<Func<T, bool>> predicate)
        {
            var results = RemoveEntities(Context, predicate);
            Context.SaveChanges();
            SetState(results, EntityState.Detached);
            return results;
        }

        public async Task<IEnumerable<T>> RemoveAsync(Expression<Func<T, bool>> predicate)
        {
            var results = RemoveEntities(Context, predicate);
            await Context.SaveChangesAsync();
            SetState(results, EntityState.Detached);
            return results;
        }

        protected virtual IEnumerable<T> RemoveEntities(U context, Expression<Func<T, bool>> predicate)
        {
            var entities = context.Set<T>().Where(predicate);
            SetState(entities, EntityState.Deleted);
            return entities;
        }

        public T Remove(T entity)
        {
            var result = RemoveEntity(Context, entity);
            Context.SaveChanges();
            SetState(entity, EntityState.Detached);
            return result;
        }

        public async Task<T> RemoveAsync(T entity)
        {
            var result = RemoveEntity(Context, entity);
            await Context.SaveChangesAsync();
            SetState(entity, EntityState.Detached);
            return result;
        }

        protected virtual T RemoveEntity(U entityContext, T entity)
        {
            SetState(entity, EntityState.Deleted);
            return entity;
        }

        public IEnumerable<T> Remove(IEnumerable<T> entities)
        {
            var results = RemoveEntities(Context, entities);
            Context.SaveChanges();
            SetState(entities, EntityState.Detached);
            return results;
        }

        public async Task<IEnumerable<T>> RemoveAsync(IEnumerable<T> entities)
        {
            var results = RemoveEntities(Context, entities);
            await Context.SaveChangesAsync();
            SetState(entities, EntityState.Detached);
            return results;
        }

        protected virtual IEnumerable<T> RemoveEntities(U context, IEnumerable<T> entities)
        {
            SetState(entities, EntityState.Deleted);
            return entities;
        }

        public T Update(T entity)
        {
            var result = UpdateEntity(Context, entity);
            Context.SaveChanges();
            SetState(entity, EntityState.Detached);
            return result;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var result = UpdateEntity(Context, entity);
            await Context.SaveChangesAsync();
            SetState(entity, EntityState.Detached);
            return result;
        }

        protected virtual T UpdateEntity(U context, T entity)
        {
            SetState(entity, EntityState.Modified);
            return entity;
        }

        public IEnumerable<T> Update(IEnumerable<T> entities)
        {
            var results = UpdateEntities(Context, entities);
            Context.SaveChanges();
            SetState(entities, EntityState.Detached);
            return results;
        }

        public async Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities)
        {
            var results = UpdateEntities(Context, entities);
            await Context.SaveChangesAsync();
            SetState(entities, EntityState.Detached);
            return results;
        }

        protected virtual IEnumerable<T> UpdateEntities(U context, IEnumerable<T> entities)
        {
            SetState(entities, EntityState.Modified);
            return entities;
        }

        public IQueryable<T> Get()
        {
            var results = GetEntities(Context);
            return results;
        }

        protected virtual IQueryable<T> GetEntities(U context)
        {
            return context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            var results = GetEntities(Context, predicate); ;
            return results;
        }

        protected virtual IQueryable<T> GetEntities(U entityContext, Expression<Func<T, bool>> predicate)
        {
            return entityContext.Set<T>().AsNoTracking().Where(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return FirstOrDefaultEntity(_context, predicate);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await FirstOrDefaultEntityAsync(_context, predicate);
        }

        protected virtual T FirstOrDefaultEntity(U entityContext, Expression<Func<T, bool>> predicate)
        {
            return entityContext.Set<T>().AsNoTracking().FirstOrDefault(predicate);
        }

        protected virtual async Task<T> FirstOrDefaultEntityAsync(U entityContext, Expression<Func<T, bool>> predicate)
        {
            return await entityContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<DtResult<T>> GetPagedAsync(DtParameters dtParameters, ISpecification<T> specification)
        {
            // if we have an empty search then just order the results by Id ascending
            var orderCriteria = "Id";
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir == "asc";
            }

            var result = _context.Set<T>().AsQueryable();

            result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria, "asc") : result.OrderByDynamic(orderCriteria, "desc");

            var filteredResultsCount = await result.CountAsync();
            var totalResultsCount = await _context.Set<T>().CountAsync();

            return new DtResult<T>
            {
                Draw = dtParameters.Draw,
                RecordsTotal = totalResultsCount,
                RecordsFiltered = filteredResultsCount,
                Data = await result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .Specify(specification)
                    .ToListAsync()
            };
        }

        protected void SetState(IEnumerable<T> entities, EntityState state)
        {
            foreach (var entity in entities)
            {
                SetState(entity, state);
            }
        }

        protected void SetState(T entity, EntityState state)
        {
            Context.Entry(entity).State = state;
        }

        #region IDisposable Members
        ~DataRepositoryBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
        #endregion
    }
}
