using AutoMapper;
using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Constants.Application;
using EXM.Common.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace EXM.Base.Features.ExpenseCategories.Commands.AddEdit
{
    public partial class AddEditExpenseCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }

    internal class AddEditExpenseCategoryCommandHandler : IRequestHandler<AddEditExpenseCategoryCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditExpenseCategoryCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditExpenseCategoryCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditExpenseCategoryCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditExpenseCategoryCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var ExpenseCategory = _mapper.Map<ExpenseCategory>(command);
                await _unitOfWork.Repository<ExpenseCategory>().AddAsync(ExpenseCategory);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllExpenseCategoriesCacheKey);
                return await Result<int>.SuccessAsync(ExpenseCategory.Id, _localizer["Expense Category Saved"]);
            }
            else
            {
                var ExpenseCategory = await _unitOfWork.Repository<ExpenseCategory>().GetByIdAsync(command.Id);
                if (ExpenseCategory != null)
                {
                    ExpenseCategory.Name = command.Name ?? ExpenseCategory.Name;
                    await _unitOfWork.Repository<ExpenseCategory>().UpdateAsync(ExpenseCategory);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllExpenseCategoriesCacheKey);
                    return await Result<int>.SuccessAsync(ExpenseCategory.Id, _localizer["Expense Category Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Expense Category Not Found!"]);
                }
            }
        }
    }
}
