using AutoMapper;
using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Constants.Application;
using EXM.Common.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace EXM.Base.Features.IncomeCategories.Commands.AddEdit
{
    public partial class AddEditIncomeCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }

    internal class AddEditIncomeCategoryCommandHandler : IRequestHandler<AddEditIncomeCategoryCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditIncomeCategoryCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditIncomeCategoryCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditIncomeCategoryCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditIncomeCategoryCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (command.Id == 0)
                {
                    var incomeCategory = _mapper.Map<IncomeCategory>(command);
                    await _unitOfWork.Repository<IncomeCategory>().AddAsync(incomeCategory);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(incomeCategory.Id, "Income Category Saved");
                }
                else
                {
                    var incomeCategory = await _unitOfWork.Repository<IncomeCategory>().GetByIdAsync(command.Id);
                    if (incomeCategory != null)
                    {
                        incomeCategory.Name = command.Name ?? incomeCategory.Name;
                        await _unitOfWork.Repository<IncomeCategory>().UpdateAsync(incomeCategory);
                        await _unitOfWork.Commit(cancellationToken);
                        return await Result<int>.SuccessAsync(incomeCategory.Id, "Income Category Updated");
                    }
                    else
                    {
                        return await Result<int>.FailAsync("Income Category Not Found!");
                    }
                }
            }
            catch (Exception ex)
            {
                return await Result<int>.FailAsync($"{ex.Message} - {ex.StackTrace}");
            }
            
        }
    }
}
