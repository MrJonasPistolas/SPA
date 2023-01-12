using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Constants.Application;
using EXM.Common.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EXM.Base.Features.IncomeCategories.Commands.Delete
{
    public class DeleteIncomeCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteIncomeCategoryCommandHandler : IRequestHandler<DeleteIncomeCategoryCommand, Result<int>>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IStringLocalizer<DeleteIncomeCategoryCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteIncomeCategoryCommandHandler(IUnitOfWork<int> unitOfWork, IIncomeRepository incomeRepository, IStringLocalizer<DeleteIncomeCategoryCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _incomeRepository = incomeRepository;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteIncomeCategoryCommand command, CancellationToken cancellationToken)
        {
            var isCategoryUsed = await _incomeRepository.IsCategoryUsed(command.Id);
            if (!isCategoryUsed)
            {
                var incomeCategory = await _unitOfWork.Repository<IncomeCategory>().GetByIdAsync(command.Id);
                if (incomeCategory != null)
                {
                    await _unitOfWork.Repository<IncomeCategory>().DeleteAsync(incomeCategory);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllIncomeCategoriesCacheKey);
                    return await Result<int>.SuccessAsync(incomeCategory.Id, _localizer["Income Category Deleted"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Income Category Not Found!"]);
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Deletion Not Allowed"]);
            }
        }
    }
}
