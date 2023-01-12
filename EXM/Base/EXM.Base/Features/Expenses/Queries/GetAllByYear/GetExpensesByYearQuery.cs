using MediatR;
using Microsoft.EntityFrameworkCore;
using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;

namespace EXM.Base.Features.Expenses.Queries.GetAllByYear
{
    public class GetExpensesByYearQuery : IRequest<Result<List<GetExpensesByYearResponse>>>
    {
        public int Year { get; set; }
        public GetExpensesByYearQuery(int year)
        {
            Year = year;
        }
    }

    internal class GetExpensesByYearQueryHandler : IRequestHandler<GetExpensesByYearQuery, Result<List<GetExpensesByYearResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetExpensesByYearQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetExpensesByYearResponse>>> Handle(GetExpensesByYearQuery request, CancellationToken cancellationToken)
        {
            var resultYear = await _unitOfWork.Repository<Expense>().Entities.Where(e => e.Date.Year == request.Year).OrderBy(e => e.Date).ToListAsync();

            var result = new List<GetExpensesByYearResponse>();

            if (resultYear.Count > 0)
            {
                var totalNumber = resultYear.Count;
                var totalMoney = resultYear.Sum(exp => exp.Amount);

                for (int i = 1; i <= 12; i++)
                {
                    var totalNumberMonth = resultYear.Where(exp => exp.Date.Month == i).Count();
                    var totalMoneyMonth = resultYear.Where(exp => exp.Date.Month == i).Sum(exp => exp.Amount);
                    var percentage = Math.Round((double)(100 * totalMoneyMonth) / totalMoney, 2);

                    result.Add(new GetExpensesByYearResponse
                    {
                        Month = i,
                        Expenses = new List<ExpensesByYear>
                        {
                            new ExpensesByYear
                            {
                                Name = "Expense",
                                Percentage = percentage
                            },
                            new ExpensesByYear
                            {
                                Name = "Remaining",
                                Percentage = 100 - percentage
                            }
                        }
                    });
                }
            }
            else
            {
                for (int i = 1; i <= 12; i++)
                {
                    result.Add(new GetExpensesByYearResponse
                    {
                        Month = i,
                        Expenses = new List<ExpensesByYear>
                        {
                            new ExpensesByYear
                            {
                                Name = "Expense",
                                Percentage = 0
                            },
                            new ExpensesByYear
                            {
                                Name = "Remaining",
                                Percentage = 100
                            }
                        }
                    });
                }
            }

            return await Result<List<GetExpensesByYearResponse>>.SuccessAsync(result);
        }
    }
}
