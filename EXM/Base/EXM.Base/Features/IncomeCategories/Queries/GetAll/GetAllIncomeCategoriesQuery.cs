using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using EXM.Base.Specifications.Catalog;
using EXM.Base.Extensions;

namespace EXM.Base.Features.IncomeCategories.Queries.GetAll
{
    public class GetAllIncomeCategoriesQuery : IRequest<PaginatedResult<GetAllIncomeCategoriesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; }
        public GetAllIncomeCategoriesQuery(int pageNumber, int pageSize, string searchString, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                OrderBy = orderBy.Split(',');
            }
        }
    }

    internal class GetAllIncomeCategoriesQueryHandler : IRequestHandler<GetAllIncomeCategoriesQuery, PaginatedResult<GetAllIncomeCategoriesResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllIncomeCategoriesQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<GetAllIncomeCategoriesResponse>> Handle(GetAllIncomeCategoriesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<IncomeCategory, GetAllIncomeCategoriesResponse>> expression = e => new GetAllIncomeCategoriesResponse
            {
                Id = e.Id,
                Name = e.Name
            };
            var incomeCategoryFilterSpec = new IncomeCategoryFilterSpecification(request.SearchString);
            if (request.OrderBy?.Any() != true)
            {
                var data = await _unitOfWork.Repository<IncomeCategory>().Entities
                    .Specify(incomeCategoryFilterSpec)
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;
            }
            else
            {
                var ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                var data = await _unitOfWork.Repository<IncomeCategory>().Entities
                   .Specify(incomeCategoryFilterSpec)
                   .OrderBy(ordering) // require system.linq.dynamic.core
                   .Select(expression)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;

            }
        }

        //public async Task<Result<List<GetAllIncomeCategoriesResponse>>> Handle(GetAllIncomeCategoriesQuery request, CancellationToken cancellationToken)
        //{
        //    Func<Task<List<IncomeCategory>>> getAllIncomeCategories = () => _unitOfWork.Repository<IncomeCategory>().GetAllAsync();
        //    var incomeCategoriesList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllIncomeCategoriesCacheKey, getAllIncomeCategories);
        //    var mappedIncomeCategories = _mapper.Map<List<GetAllIncomeCategoriesResponse>>(incomeCategoriesList);
        //    return await Result<List<GetAllIncomeCategoriesResponse>>.SuccessAsync(mappedIncomeCategories);
        //}
    }
}
