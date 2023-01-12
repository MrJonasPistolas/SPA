using EXM.Base.Extensions;
using EXM.Base.Interfaces.Repositories;
using EXM.Base.Interfaces.Services;
using EXM.Base.Specifications.Catalog;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EXM.Base.Features.Expenses.Queries.Export
{
    public class ExportExpensesQuery : IRequest<Result<string>>
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string SearchString { get; set; }

        public ExportExpensesQuery(int year, int month, string searchString = "")
        {
            Year = year;
            Month = month;
            SearchString = searchString;
        }
    }

    internal class ExportExpensesQueryHandler : IRequestHandler<ExportExpensesQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportExpensesQueryHandler> _localizer;

        public ExportExpensesQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportExpensesQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportExpensesQuery request, CancellationToken cancellationToken)
        {
            var ExpenseFilterSpec = new ExpenseFilterSpecification(request.SearchString);
            var Expenses = await _unitOfWork.Repository<Expense>().Entities
                .Where(x => x.Date.Year == request.Year && x.Date.Month == request.Month)
                .Specify(ExpenseFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(Expenses, mappers: new Dictionary<string, Func<Expense, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name },
                { _localizer["Amount"], item => item.Amount },
                { _localizer["Date"], item => item.Date },
                { _localizer["Description"], item => item.Description },
                { _localizer["Category"], item => item.Category.Name }
            }, sheetName: _localizer["Expenses"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
