using EXM.Common.Data.Contracts;
using EXM.Common.Data.Repositories;

namespace EXM.Data.Domain.Repositories
{
    public abstract class RepositoryBase<T> : DataRepositoryBase<T, EXMContext>
        where T : class, IEntity, new()
    {
        public RepositoryBase(EXMContext context) : base(context)
        {

        }
    }
}
