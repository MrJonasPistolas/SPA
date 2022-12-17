using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EXM.Base.Features.Incomes.Queries.GetAllByYear
{
    public class GetIncomesByYearQuery : IRequest<Result<List<GetIncomesByYearResponse>>>
    {
        public int Year { get; set; }
        public GetIncomesByYearQuery(int year)
        {
            Year = year;
        }
    }

    internal class GetIncomesByYearQueryHandler : IRequestHandler<GetIncomesByYearQuery, Result<List<GetIncomesByYearResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetIncomesByYearQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetIncomesByYearResponse>>> Handle(GetIncomesByYearQuery request, CancellationToken cancellationToken)
        {
            var resultYear = await _unitOfWork.Repository<Income>().Entities.Where(e => e.Date.Year == request.Year).OrderBy(e => e.Date).ToListAsync();

            var result = new List<GetIncomesByYearResponse>();

            if (resultYear.Count > 0)
            {
                var totalNumber = resultYear.Count;
                var totalMoney = resultYear.Sum(exp => exp.Amount);

                for (int i = 1; i <= 12; i++)
                {
                    var totalNumberMonth = resultYear.Where(exp => exp.Date.Month == i).Count();
                    var totalMoneyMonth = resultYear.Where(exp => exp.Date.Month == i).Sum(exp => exp.Amount);
                    var percentage = Math.Round((double)(100 * totalMoneyMonth) / totalMoney, 2);

                    result.Add(new GetIncomesByYearResponse
                    {
                        Month = i,
                        Incomes = new List<IncomesByYear>
                        {
                            new IncomesByYear
                            {
                                Name = "Income",
                                Percentage = percentage
                            },
                            new IncomesByYear
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
                    result.Add(new GetIncomesByYearResponse
                    {
                        Month = i,
                        Incomes = new List<IncomesByYear>
                        {
                            new IncomesByYear
                            {
                                Name = "Income",
                                Percentage = 0
                            },
                            new IncomesByYear
                            {
                                Name = "Remaining",
                                Percentage = 100
                            }
                        }
                    });
                }
            }

            return await Result<List<GetIncomesByYearResponse>>.SuccessAsync(result);
        }
    }
}
