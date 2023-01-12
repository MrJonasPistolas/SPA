using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EXM.Base.Features.Dashboards.Queries.GetDataByYear
{
    public class GetDashboardByYearDataQuery : IRequest<Result<DashboardByYearDataResponse>>
    {
        public int Year { get; set; }
        public GetDashboardByYearDataQuery(int year)
        {
            Year = year;
        }
    }

    internal class GetDashboardByYearDataQueryHandler : IRequestHandler<GetDashboardByYearDataQuery, Result<DashboardByYearDataResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<GetDashboardByYearDataQueryHandler> _localizer;

        public GetDashboardByYearDataQueryHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<GetDashboardByYearDataQueryHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<DashboardByYearDataResponse>> Handle(GetDashboardByYearDataQuery query, CancellationToken cancellationToken)
        {
            var incomesYear = await _unitOfWork.Repository<Income>().Entities.Where(e => e.Date.Year == query.Year).OrderBy(e => e.Date).ToListAsync();
            var expensesYear = await _unitOfWork.Repository<Expense>().Entities.Where(e => e.Date.Year == query.Year).OrderBy(e => e.Date).ToListAsync();

            var result = new DashboardByYearDataResponse
            {
                Incomes = new List<BarSeries>(),
                Expenses = new List<BarSeries>(),
                Difference = new List<BarSeries>()
            };

            #region Incomes treatment
            if (incomesYear.Count > 0)
            {
                for (int i = 1; i <= 12; i++)
                {
                    result.Incomes.Add(new BarSeries
                    {
                        Month = i,
                        Name = _localizer[i.ToString()],
                        Sum = incomesYear.Where(inc => inc.Date.Month == i).Sum(inc => inc.Amount)
                    });
                }
            }
            else
            {
                for (int i = 1; i <= 12; i++)
                {
                    result.Incomes.Add(new BarSeries
                    {
                        Month = i,
                        Name = _localizer[i.ToString()],
                        Sum = 0
                    });
                }
            }
            #endregion

            #region Expenses treatment
            if (expensesYear.Count > 0)
            {
                for (int i = 1; i <= 12; i++)
                {
                    result.Expenses.Add(new BarSeries
                    {
                        Month = i,
                        Name = _localizer[i.ToString()],
                        Sum = expensesYear.Where(exp => exp.Date.Month == i).Sum(exp => exp.Amount)
                    });
                }
            }
            else
            {
                for (int i = 1; i <= 12; i++)
                {
                    result.Expenses.Add(new BarSeries
                    {
                        Month = i,
                        Name = _localizer[i.ToString()],
                        Sum = 0
                    });
                }
            }
            #endregion

            for (int i = 1; i <= 12; i++)
            {
                result.Difference.Add(new BarSeries
                {
                    Month = i,
                    Name = _localizer[i.ToString()],
                    Sum = result.Incomes[i - 1].Sum - result.Expenses[i - 1].Sum
                });
            }

            return await Result<DashboardByYearDataResponse>.SuccessAsync(result);
        }
    }
}
