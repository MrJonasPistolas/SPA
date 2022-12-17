using AutoMapper;
using LazyCache;
using MediatR;
using EXM.Common.Wrapper;
using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Constants.Application;

namespace EXM.Base.Features.ExpenseCategories.Queries.GetAll
{
    public class GetAllExpenseCategoriesQuery : IRequest<Result<List<GetAllExpenseCategoriesResponse>>>
    {
        public GetAllExpenseCategoriesQuery() { }
    }

    internal class GetAllExpenseCategoriesQueryHandler : IRequestHandler<GetAllExpenseCategoriesQuery, Result<List<GetAllExpenseCategoriesResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllExpenseCategoriesQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllExpenseCategoriesResponse>>> Handle(GetAllExpenseCategoriesQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<ExpenseCategory>>> getAllExpenseCategories = () => _unitOfWork.Repository<ExpenseCategory>().GetAllAsync();
            var ExpenseCategoriesList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllExpenseCategoriesCacheKey, getAllExpenseCategories);
            var mappedExpenseCategories = _mapper.Map<List<GetAllExpenseCategoriesResponse>>(ExpenseCategoriesList);
            return await Result<List<GetAllExpenseCategoriesResponse>>.SuccessAsync(mappedExpenseCategories);
        }
    }
}
