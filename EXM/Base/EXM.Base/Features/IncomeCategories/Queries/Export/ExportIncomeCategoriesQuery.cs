using EXM.Base.Extensions;
using EXM.Base.Interfaces.Repositories;
using EXM.Base.Interfaces.Services;
using EXM.Base.Specifications.Catalog;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EXM.Base.Features.IncomeCategories.Queries.Export
{
    public class ExportIncomeCategoriesQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportIncomeCategoriesQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportIncomeCategoriesQueryHandler : IRequestHandler<ExportIncomeCategoriesQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportIncomeCategoriesQueryHandler> _localizer;

        public ExportIncomeCategoriesQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportIncomeCategoriesQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportIncomeCategoriesQuery request, CancellationToken cancellationToken)
        {
            var incomeCategoryFilterSpec = new IncomeCategoryFilterSpecification(request.SearchString);
            var incomeCategories = await _unitOfWork.Repository<IncomeCategory>().Entities
                .Specify(incomeCategoryFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(incomeCategories, mappers: new Dictionary<string, Func<IncomeCategory, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name }
            }, sheetName: _localizer["Income Categories"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
