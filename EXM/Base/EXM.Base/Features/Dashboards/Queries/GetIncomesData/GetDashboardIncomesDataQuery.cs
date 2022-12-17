using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EXM.Base.Features.Dashboards.Queries.GetIncomesData
{
    public class GetDashboardIncomesDataQuery : IRequest<Result<DashboardIncomesDataResponse>>
    {

    }

    internal class GetDashboardIncomesDataQueryHandler : IRequestHandler<GetDashboardIncomesDataQuery, Result<DashboardIncomesDataResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetDashboardIncomesDataQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DashboardIncomesDataResponse>> Handle(GetDashboardIncomesDataQuery query, CancellationToken cancellationToken)
        {
            var response = new DashboardIncomesDataResponse
            {
                IncomeCount = await _unitOfWork.Repository<Income>().Entities.CountAsync(cancellationToken)
            };

            return await Result<DashboardIncomesDataResponse>.SuccessAsync(response);
        }
    }
}
