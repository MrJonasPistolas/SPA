using System.Threading.Tasks;

namespace EXM.Base.Interfaces.Repositories
{
    public interface IIncomeRepository
    {
        Task<bool> IsCategoryUsed(int categoryId);
    }
}
