using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EXM.Base.Features.Dashboards.Queries.GetIncomesCategoriesData
{
    public class GetDashboardIncomesCategoriesDataQuery : IRequest<Result<DashboardIncomesCategoriesDataResponse>>
    {

    }

    internal class GetDashboardIncomesCategoriesDataQueryHandler : IRequestHandler<GetDashboardIncomesCategoriesDataQuery, Result<DashboardIncomesCategoriesDataResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetDashboardIncomesCategoriesDataQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DashboardIncomesCategoriesDataResponse>> Handle(GetDashboardIncomesCategoriesDataQuery query, CancellationToken cancellationToken)
        {
            var response = new DashboardIncomesCategoriesDataResponse
            {
                IncomeCategoriesCount = await _unitOfWork.Repository<IncomeCategory>().Entities.CountAsync(cancellationToken)
            };

            return await Result<DashboardIncomesCategoriesDataResponse>.SuccessAsync(response);
        }
    }
}
