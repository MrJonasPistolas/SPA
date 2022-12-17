using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EXM.Base.Features.Dashboards.Queries.GetExpensesCategoriesData
{
    public class GetDashboardExpensesCategoriesDataQuery : IRequest<Result<DashboardExpensesCategoriesDataResponse>>
    {

    }

    internal class GetDashboardExpensesCategoriesDataQueryHandler : IRequestHandler<GetDashboardExpensesCategoriesDataQuery, Result<DashboardExpensesCategoriesDataResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetDashboardExpensesCategoriesDataQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DashboardExpensesCategoriesDataResponse>> Handle(GetDashboardExpensesCategoriesDataQuery query, CancellationToken cancellationToken)
        {
            var response = new DashboardExpensesCategoriesDataResponse
            {
                ExpenseCategoriesCount = await _unitOfWork.Repository<ExpenseCategory>().Entities.CountAsync(cancellationToken)
            };

            return await Result<DashboardExpensesCategoriesDataResponse>.SuccessAsync(response);
        }
    }
}
