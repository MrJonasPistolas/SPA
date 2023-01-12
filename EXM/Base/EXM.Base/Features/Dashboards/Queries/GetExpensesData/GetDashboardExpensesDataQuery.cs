using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EXM.Base.Features.Dashboards.Queries.GetExpensesData
{
    public class GetDashboardExpensesDataQuery : IRequest<Result<DashboardExpensesDataResponse>>
    {

    }

    internal class GetDashboardExpensesDataQueryHandler : IRequestHandler<GetDashboardExpensesDataQuery, Result<DashboardExpensesDataResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetDashboardExpensesDataQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DashboardExpensesDataResponse>> Handle(GetDashboardExpensesDataQuery query, CancellationToken cancellationToken)
        {
            var response = new DashboardExpensesDataResponse
            {
                ExpenseCount = await _unitOfWork.Repository<Expense>().Entities.CountAsync(cancellationToken)
            };

            return await Result<DashboardExpensesDataResponse>.SuccessAsync(response);
        }
    }
}
