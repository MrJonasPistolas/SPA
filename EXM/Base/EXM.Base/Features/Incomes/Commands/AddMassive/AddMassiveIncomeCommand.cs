using AutoMapper;
using EXM.Base.Interfaces.Repositories;
using EXM.Base.Models;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EXM.Base.Features.Incomes.Commands.AddMassive
{
    public partial class AddMassiveIncomeCommand : IRequest<Result<int>>
    {
        public List<UploadIncomeModel> Incomes { get; set; }
    }

    internal class AddMassiveIncomeCommandHandler : IRequestHandler<AddMassiveIncomeCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<AddMassiveIncomeCommandHandler> _localizer;

        public AddMassiveIncomeCommandHandler(IMapper mapper, IUnitOfWork<int> unitOfWork, IStringLocalizer<AddMassiveIncomeCommandHandler> localizer)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddMassiveIncomeCommand command, CancellationToken cancellationToken)
        {
            if (command.Incomes.Count == 0)
            {
                return await Result<int>.FailAsync(_localizer["Incomes Not Found!"]);
            }
            else
            {
                try
                {
                    foreach (var commandIncome in command.Incomes)
                    {
                        var income = _mapper.Map<Income>(commandIncome);
                        await _unitOfWork.Repository<Income>().AddAsync(income);
                        await _unitOfWork.Commit(cancellationToken);
                    }
                    return await Result<int>.SuccessAsync(0, _localizer["Incomes Loaded"]);
                }
                catch
                {
                    return await Result<int>.FailAsync(_localizer["Incomes Error Saving"]);
                }
            }
        }
    }
}
