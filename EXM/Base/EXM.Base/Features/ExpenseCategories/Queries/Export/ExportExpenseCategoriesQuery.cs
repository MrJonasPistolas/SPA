using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using EXM.Common.Wrapper;
using EXM.Base.Interfaces.Repositories;
using EXM.Base.Interfaces.Services;
using EXM.Base.Specifications.Catalog;
using EXM.Domain.Entities;
using EXM.Base.Extensions;

namespace EXM.Base.Features.ExpenseCategories.Queries.Export
{
    public class ExportExpenseCategoriesQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportExpenseCategoriesQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportExpenseCategoriesQueryHandler : IRequestHandler<ExportExpenseCategoriesQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportExpenseCategoriesQueryHandler> _localizer;

        public ExportExpenseCategoriesQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportExpenseCategoriesQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportExpenseCategoriesQuery request, CancellationToken cancellationToken)
        {
            var ExpenseCategoryFilterSpec = new ExpenseCategoryFilterSpecification(request.SearchString);
            var ExpenseCategories = await _unitOfWork.Repository<ExpenseCategory>().Entities
                .Specify(ExpenseCategoryFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(ExpenseCategories, mappers: new Dictionary<string, Func<ExpenseCategory, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name }
            }, sheetName: _localizer["Expense Categories"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
