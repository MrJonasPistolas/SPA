using EXM.Base.Extensions;
using EXM.Base.Interfaces.Repositories;
using EXM.Base.Specifications.Catalog;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace EXM.Base.Features.Incomes.Queries.GetAllPaged
{
    public class GetAllIncomesQuery : IRequest<PaginatedResult<GetAllPagedIncomesResponse>>
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetAllIncomesQuery(int year, int month, int pageNumber, int pageSize, string searchString, string orderBy)
        {
            Year = year;
            Month = month;
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                OrderBy = orderBy.Split(',');
            }
        }
    }

    internal class GetAllIncomesQueryHandler : IRequestHandler<GetAllIncomesQuery, PaginatedResult<GetAllPagedIncomesResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllIncomesQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<GetAllPagedIncomesResponse>> Handle(GetAllIncomesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Income, GetAllPagedIncomesResponse>> expression = e => new GetAllPagedIncomesResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Amount = e.Amount,
                Date = e.Date,
                IncomeCategory = e.Category.Name,
                IncomeCategoryId = e.IncomeCategoryId
            };
            var productFilterSpec = new IncomeFilterSpecification(request.SearchString);
            if (request.OrderBy?.Any() != true)
            {
                var data = await _unitOfWork.Repository<Income>().Entities
                   .Where(x => x.Date.Year == request.Year && x.Date.Month == request.Month)
                   .Specify(productFilterSpec)
                   .Select(expression)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;
            }
            else
            {
                var ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                var data = await _unitOfWork.Repository<Income>().Entities
                   .Where(x => x.Date.Year == request.Year && x.Date.Month == request.Month)
                   .Specify(productFilterSpec)
                   .OrderBy(ordering) // require system.linq.dynamic.core
                   .Select(expression)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;

            }
        }
    }
}
