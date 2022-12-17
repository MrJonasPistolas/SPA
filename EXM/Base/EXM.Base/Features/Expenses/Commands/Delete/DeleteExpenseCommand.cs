using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EXM.Base.Features.Expenses.Commands.Delete
{
    public class DeleteExpenseCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<DeleteExpenseCommandHandler> _localizer;

        public DeleteExpenseCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteExpenseCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteExpenseCommand command, CancellationToken cancellationToken)
        {
            var Expense = await _unitOfWork.Repository<Expense>().GetByIdAsync(command.Id);
            if (Expense != null)
            {
                await _unitOfWork.Repository<Expense>().DeleteAsync(Expense);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(Expense.Id, _localizer["Expense Deleted"]);
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Expense Not Found!"]);
            }
        }
    }
}
