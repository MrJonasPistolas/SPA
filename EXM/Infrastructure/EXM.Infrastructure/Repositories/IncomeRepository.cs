using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EXM.Infrastructure.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly IRepositoryAsync<Income, int> _repository;

        public IncomeRepository(IRepositoryAsync<Income, int> repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsCategoryUsed(int categoryId)
        {
            return await _repository.Entities.AnyAsync(i => i.IncomeCategoryId == categoryId);
        }
    }
}
