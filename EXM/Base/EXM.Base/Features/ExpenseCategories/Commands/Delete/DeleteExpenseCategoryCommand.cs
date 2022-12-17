using MediatR;
using Microsoft.Extensions.Localization;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using EXM.Base.Interfaces.Repositories;
using EXM.Common.Constants.Application;

namespace EXM.Base.Features.ExpenseCategories.Commands.Delete
{
    public class DeleteExpenseCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteExpenseCategoryCommandHandler : IRequestHandler<DeleteExpenseCategoryCommand, Result<int>>
    {
        private readonly IExpenseRepository _ExpenseRepository;
        private readonly IStringLocalizer<DeleteExpenseCategoryCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteExpenseCategoryCommandHandler(IUnitOfWork<int> unitOfWork, IExpenseRepository ExpenseRepository, IStringLocalizer<DeleteExpenseCategoryCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _ExpenseRepository = ExpenseRepository;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteExpenseCategoryCommand command, CancellationToken cancellationToken)
        {
            var isCategoryUsed = await _ExpenseRepository.IsCategoryUsed(command.Id);
            if (!isCategoryUsed)
            {
                var ExpenseCategory = await _unitOfWork.Repository<ExpenseCategory>().GetByIdAsync(command.Id);
                if (ExpenseCategory != null)
                {
                    await _unitOfWork.Repository<ExpenseCategory>().DeleteAsync(ExpenseCategory);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllExpenseCategoriesCacheKey);
                    return await Result<int>.SuccessAsync(ExpenseCategory.Id, _localizer["Expense Category Deleted"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Expense Category Not Found!"]);
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Deletion Not Allowed"]);
            }
        }
    }
}
