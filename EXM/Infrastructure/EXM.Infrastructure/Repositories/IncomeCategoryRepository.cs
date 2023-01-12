using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;

namespace EXM.Infrastructure.Repositories
{
    public class IncomeCategoryRepository : IIncomeCategoryRepository
    {
        private readonly IRepositoryAsync<IncomeCategory, int> _repository;

        public IncomeCategoryRepository(IRepositoryAsync<IncomeCategory, int> repository)
        {
            _repository = repository;
        }
    }
}
