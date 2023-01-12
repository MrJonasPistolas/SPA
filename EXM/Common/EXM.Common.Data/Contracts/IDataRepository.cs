using EXM.Common.Data.Models;
using EXM.Common.Data.Specifications.Base;
using System.Linq.Expressions;

namespace EXM.Common.Data.Contracts
{
    public interface IDataRepository : IDisposable
    {
    }

    public interface IDataRepository<T> : IDisposable, IDataRepository where T : class, IEntity, new()
    {
        T Add(T entity);
        Task<T> AddAsync(T entity);

        IEnumerable<T> Add(IEnumerable<T> entities);
        Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities);

        T Remove(T entity);
        Task<T> RemoveAsync(T entity);

        IEnumerable<T> Remove(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> RemoveAsync(Expression<Func<T, bool>> predicate);

        IEnumerable<T> Remove(IEnumerable<T> entities);
        Task<IEnumerable<T>> RemoveAsync(IEnumerable<T> entities);

        T Update(T entity);
        Task<T> UpdateAsync(T entity);

        IEnumerable<T> Update(IEnumerable<T> entities);
        Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities);

        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task<DtResult<T>> GetPagedAsync(DtParameters dtParameters, ISpecification<T> specification);
    }
}
