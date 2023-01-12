using EXM.Base.Extensions;
using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EXM.Base.Features.Expenses.Queries.GetAllByYear
{
    public class GetAllExpensesByYearQuery : IRequest<Result<List<GetAllExpensesByYearResponse>>>
    {
        public int Year { get; set; }
        public GetAllExpensesByYearQuery(int year)
        {
            Year = year;
        }
    }

    internal class GetAllExpensesByYearQueryHandler: IRequestHandler<GetAllExpensesByYearQuery, Result<List<GetAllExpensesByYearResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllExpensesByYearQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetAllExpensesByYearResponse>>> Handle(GetAllExpensesByYearQuery request, CancellationToken cancellationToken)
        {
            var resultYear = await _unitOfWork.Repository<Expense>().Entities.Where(e => e.Date.Year == request.Year).OrderBy(e => e.Date).ToListAsync();

            var result = new List<GetAllExpensesByYearResponse>();

            if (resultYear.Count > 0)
            {
                var totalNumber = resultYear.Count;
                var totalMoney = resultYear.Sum(exp => exp.Amount);

                for (int i = 1; i <= 12; i++)
                {
                    var totalNumberMonth = resultYear.Where(exp => exp.Date.Month == i).Count();
                    var totalMoneyMonth = resultYear.Where(exp => exp.Date.Month == i).Sum(exp => exp.Amount);

                    result.Add(new GetAllExpensesByYearResponse
                    {
                        Year = request.Year,
                        Month = i,
                        Number = totalNumberMonth,
                        Percentage = (int)((100 * totalMoneyMonth) / totalMoney).Round(5),
                        Percent = Math.Round((double)(100 * totalMoneyMonth) / totalMoney, 2),
                        Amount = Math.Round(totalMoneyMonth, 2)
                    });
                }
            }
            else
            {
                for (int i = 1; i <= 12; i++)
                {
                    result.Add(new GetAllExpensesByYearResponse
                    {
                        Month = i,
                        Number = 0,
                        Percentage = 0,
                        Amount = 0,
                        Percent = 0
                    });
                }
            }

            return await Result<List<GetAllExpensesByYearResponse>>.SuccessAsync(result);
        }
    }
}
