using EXM.Common.Data.Contracts;
using EXM.Common.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EXM.Data.Logger.Repositories
{
    public abstract class RepositoryBase<T> : DataRepositoryBase<T, DbContext>
        where T : class, IEntity, new()
    {
        public RepositoryBase(DbContext context) : base(context) { }
    }
}
