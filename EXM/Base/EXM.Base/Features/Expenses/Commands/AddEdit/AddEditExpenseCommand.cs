using AutoMapper;
using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace EXM.Base.Features.Expenses.Commands.AddEdit
{
    public partial class AddEditExpenseCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int ExpenseCategoryId { get; set; }
    }

    internal class AddEditExpenseCommandHandler : IRequestHandler<AddEditExpenseCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<AddEditExpenseCommandHandler> _localizer;

        public AddEditExpenseCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditExpenseCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditExpenseCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var Expense = _mapper.Map<Expense>(command);
                await _unitOfWork.Repository<Expense>().AddAsync(Expense);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(Expense.Id, _localizer["Expense Saved"]);
            }
            else
            {
                var Expense = await _unitOfWork.Repository<Expense>().GetByIdAsync(command.Id);
                if (Expense != null)
                {
                    Expense.Name = command.Name ?? Expense.Name;
                    Expense.Description = command.Description ?? Expense.Description;
                    Expense.ExpenseCategoryId = (command.ExpenseCategoryId == 0) ? Expense.ExpenseCategoryId : command.ExpenseCategoryId;
                    Expense.Amount = (command.Amount == 0) ? Expense.Amount : command.Amount;
                    Expense.Date = command.Date ?? Expense.Date;

                    await _unitOfWork.Repository<Expense>().UpdateAsync(Expense);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(Expense.Id, _localizer["Expense Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Expense Not Found!"]);
                }
            }
        }
    }
}
