using AutoMapper;
using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace EXM.Base.Features.Incomes.Commands.AddEdit
{
    public partial class AddEditIncomeCommand : IRequest<Result<int>>
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
        public int IncomeCategoryId { get; set; }
    }

    internal class AddEditIncomeCommandHandler : IRequestHandler<AddEditIncomeCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<AddEditIncomeCommandHandler> _localizer;

        public AddEditIncomeCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditIncomeCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditIncomeCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var income = _mapper.Map<Income>(command);
                await _unitOfWork.Repository<Income>().AddAsync(income);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(income.Id, _localizer["Income Saved"]);
            }
            else
            {
                var income = await _unitOfWork.Repository<Income>().GetByIdAsync(command.Id);
                if (income != null)
                {
                    income.Name = command.Name ?? income.Name;
                    income.Description = command.Description ?? income.Description;
                    income.IncomeCategoryId = (command.IncomeCategoryId == 0) ? income.IncomeCategoryId : command.IncomeCategoryId;
                    income.Amount = (command.Amount == 0) ? income.Amount : command.Amount;
                    income.Date = command.Date.Value;

                    await _unitOfWork.Repository<Income>().UpdateAsync(income);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(income.Id, _localizer["Income Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Income Not Found!"]);
                }
            }
        }
    }
}
