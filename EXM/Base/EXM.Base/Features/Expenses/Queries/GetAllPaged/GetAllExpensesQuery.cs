using EXM.Base.Extensions;
using EXM.Base.Interfaces.Repositories;
using EXM.Base.Specifications.Catalog;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace EXM.Base.Features.Expenses.Queries.GetAllPaged
{
    public class GetAllExpensesQuery : IRequest<PaginatedResult<GetAllPagedExpensesResponse>>
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetAllExpensesQuery(int year, int month, int pageNumber, int pageSize, string searchString, string orderBy)
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

    internal class GetAllExpensesQueryHandler : IRequestHandler<GetAllExpensesQuery, PaginatedResult<GetAllPagedExpensesResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllExpensesQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<GetAllPagedExpensesResponse>> Handle(GetAllExpensesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Expense, GetAllPagedExpensesResponse>> expression = e => new GetAllPagedExpensesResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Amount = e.Amount,
                Date = e.Date,
                ExpenseCategory = e.Category.Name,
                ExpenseCategoryId = e.ExpenseCategoryId
            };
            var productFilterSpec = new ExpenseFilterSpecification(request.SearchString);
            if (request.OrderBy?.Any() != true)
            {
                var data = await _unitOfWork.Repository<Expense>().Entities
                   .Where(x => x.Date.Year == request.Year && x.Date.Month == request.Month)
                   .Specify(productFilterSpec)
                   .Select(expression)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;
            }
            else
            {
                var ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                var data = await _unitOfWork.Repository<Expense>().Entities
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
