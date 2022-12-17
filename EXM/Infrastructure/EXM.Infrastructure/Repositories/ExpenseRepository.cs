using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EXM.Infrastructure.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IRepositoryAsync<Expense, int> _repository;

        public ExpenseRepository(IRepositoryAsync<Expense, int> repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsCategoryUsed(int categoryId)
        {
            return await _repository.Entities.AnyAsync(i => i.ExpenseCategoryId == categoryId);
        }
    }
}
