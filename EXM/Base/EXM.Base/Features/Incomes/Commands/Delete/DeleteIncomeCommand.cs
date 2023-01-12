using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EXM.Base.Features.Incomes.Commands.Delete
{
    public class DeleteIncomeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteIncomeCommandHandler : IRequestHandler<DeleteIncomeCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<DeleteIncomeCommandHandler> _localizer;

        public DeleteIncomeCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteIncomeCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteIncomeCommand command, CancellationToken cancellationToken)
        {
            var income = await _unitOfWork.Repository<Income>().GetByIdAsync(command.Id);
            if (income != null)
            {
                await _unitOfWork.Repository<Income>().DeleteAsync(income);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(income.Id, _localizer["Income Deleted"]);
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Income Not Found!"]);
            }
        }
    }
}
