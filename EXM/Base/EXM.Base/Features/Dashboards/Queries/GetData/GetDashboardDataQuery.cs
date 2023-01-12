using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using EXM.Common.Wrapper;
using EXM.Base.Interfaces.Repositories;
using EXM.Base.Interfaces.Services.Identity;
using EXM.Domain.Entities;

namespace EXM.Base.Features.Dashboards.Queries.GetData
{
    public class GetDashboardDataQuery : IRequest<Result<DashboardDataResponse>>
    {

    }

    internal class GetDashboardDataQueryHandler : IRequestHandler<GetDashboardDataQuery, Result<DashboardDataResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IStringLocalizer<GetDashboardDataQueryHandler> _localizer;

        public GetDashboardDataQueryHandler(IUnitOfWork<int> unitOfWork, IUserService userService, IRoleService roleService, IStringLocalizer<GetDashboardDataQueryHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _roleService = roleService;
            _localizer = localizer;
        }

        public async Task<Result<DashboardDataResponse>> Handle(GetDashboardDataQuery query, CancellationToken cancellationToken)
        {
            var response = new DashboardDataResponse
            {
                IncomeCount = await _unitOfWork.Repository<Income>().Entities.CountAsync(cancellationToken),
                IncomeCategoryCount = await _unitOfWork.Repository<IncomeCategory>().Entities.CountAsync(cancellationToken),
                ExpenseCount = await _unitOfWork.Repository<Expense>().Entities.CountAsync(cancellationToken),
                ExpenseCategoryCount = await _unitOfWork.Repository<ExpenseCategory>().Entities.CountAsync(cancellationToken),
                UserCount = await _userService.GetCountAsync(),
                RoleCount = await _roleService.GetCountAsync()
            };

            var selectedYear = DateTime.Now.Year;
            double[] expensesFigure = new double[13];
            double[] expenseCategoriesFigure = new double[13];
            double[] incomesFigure = new double[13];
            double[] incomeCategoriesFigure = new double[13];

            for (int i = 1; i <= 12; i++)
            {
                var month = i;
                var filterStartDate = new DateTime(selectedYear, month, 01);
                var filterEndDate = new DateTime(selectedYear, month, DateTime.DaysInMonth(selectedYear, month), 23, 59, 59); // Monthly Based

                expensesFigure[i - 1] = await _unitOfWork.Repository<Expense>().Entities.Where(x => x.CreatedOn >= filterStartDate && x.CreatedOn <= filterEndDate).CountAsync(cancellationToken);
                expenseCategoriesFigure[i - 1] = await _unitOfWork.Repository<ExpenseCategory>().Entities.Where(x => x.CreatedOn >= filterStartDate && x.CreatedOn <= filterEndDate).CountAsync(cancellationToken);
                incomesFigure[i - 1] = await _unitOfWork.Repository<Income>().Entities.Where(x => x.CreatedOn >= filterStartDate && x.CreatedOn <= filterEndDate).CountAsync(cancellationToken);
                incomeCategoriesFigure[i - 1] = await _unitOfWork.Repository<IncomeCategory>().Entities.Where(x => x.CreatedOn >= filterStartDate && x.CreatedOn <= filterEndDate).CountAsync(cancellationToken);
            }

            response.DataEnterBarChart.Add(new ChartSeries { Name = _localizer["Incomes"], Data = incomesFigure });
            response.DataEnterBarChart.Add(new ChartSeries { Name = _localizer["Income Categories"], Data = incomeCategoriesFigure });
            response.DataEnterBarChart.Add(new ChartSeries { Name = _localizer["Expenses"], Data = expensesFigure });
            response.DataEnterBarChart.Add(new ChartSeries { Name = _localizer["Expense Categories"], Data = expenseCategoriesFigure });

            return await Result<DashboardDataResponse>.SuccessAsync(response);
        }
    }
}