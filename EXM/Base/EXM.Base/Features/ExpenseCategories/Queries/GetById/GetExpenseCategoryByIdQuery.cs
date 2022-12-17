using AutoMapper;
using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;

namespace EXM.Base.Features.ExpenseCategories.Queries.GetById
{
    public class GetExpenseCategoryByIdQuery : IRequest<Result<GetExpenseCategoryByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetExpenseCategoryByIdQueryHandler : IRequestHandler<GetExpenseCategoryByIdQuery, Result<GetExpenseCategoryByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetExpenseCategoryByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetExpenseCategoryByIdResponse>> Handle(GetExpenseCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var ExpenseCategory = await _unitOfWork.Repository<ExpenseCategory>().GetByIdAsync(query.Id);
            var mappedExpenseCategory = _mapper.Map<GetExpenseCategoryByIdResponse>(ExpenseCategory);
            return await Result<GetExpenseCategoryByIdResponse>.SuccessAsync(mappedExpenseCategory);
        }
    }
}
