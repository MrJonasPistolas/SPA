using AutoMapper;
using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Constants.Application;
using EXM.Common.Wrapper;
using LazyCache;
using MediatR;

namespace EXM.Base.Features.IncomeCategories.Queries.GetAll
{
    public class GetAllIncomeCategoriesQuery : IRequest<Result<List<GetAllIncomeCategoriesResponse>>>
    {
        public GetAllIncomeCategoriesQuery() { }
    }

    internal class GetAllIncomeCategoriesQueryHandler : IRequestHandler<GetAllIncomeCategoriesQuery, Result<List<GetAllIncomeCategoriesResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllIncomeCategoriesQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllIncomeCategoriesResponse>>> Handle(GetAllIncomeCategoriesQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<IncomeCategory>>> getAllIncomeCategories = () => _unitOfWork.Repository<IncomeCategory>().GetAllAsync();
            var incomeCategoriesList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllIncomeCategoriesCacheKey, getAllIncomeCategories);
            var mappedIncomeCategories = _mapper.Map<List<GetAllIncomeCategoriesResponse>>(incomeCategoriesList);
            return await Result<List<GetAllIncomeCategoriesResponse>>.SuccessAsync(mappedIncomeCategories);
        }
    }
}
