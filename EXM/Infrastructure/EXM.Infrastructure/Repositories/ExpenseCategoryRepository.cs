using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;

namespace EXM.Infrastructure.Repositories
{
    public class ExpenseCategoryRepository : IExpenseCategoryRepository
    {
        private readonly IRepositoryAsync<ExpenseCategory, int> _repository;

        public ExpenseCategoryRepository(IRepositoryAsync<ExpenseCategory, int> repository)
        {
            _repository = repository;
        }
    }
}
