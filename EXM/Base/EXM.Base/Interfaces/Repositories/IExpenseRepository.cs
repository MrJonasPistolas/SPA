using System.Threading.Tasks;

namespace EXM.Base.Interfaces.Repositories
{
    public interface IExpenseRepository
    {
        Task<bool> IsCategoryUsed(int categoryId);
    }
}
