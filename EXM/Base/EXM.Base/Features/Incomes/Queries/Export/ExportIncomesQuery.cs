using EXM.Base.Extensions;
using EXM.Base.Interfaces.Repositories;
using EXM.Base.Interfaces.Services;
using EXM.Base.Specifications.Catalog;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EXM.Base.Features.Incomes.Queries.Export
{
    public class ExportIncomesQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportIncomesQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportIncomesQueryHandler : IRequestHandler<ExportIncomesQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportIncomesQueryHandler> _localizer;

        public ExportIncomesQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportIncomesQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportIncomesQuery request, CancellationToken cancellationToken)
        {
            var incomeFilterSpec = new IncomeFilterSpecification(request.SearchString);
            var incomes = await _unitOfWork.Repository<Income>().Entities
                .Specify(incomeFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(incomes, mappers: new Dictionary<string, Func<Income, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name },
                { _localizer["Amount"], item => item.Amount },
                { _localizer["Date"], item => item.Date },
                { _localizer["Description"], item => item.Description },
                { _localizer["Category"], item => item.Category.Name }
            }, sheetName: _localizer["Incomes"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
