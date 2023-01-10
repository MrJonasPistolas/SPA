using AutoMapper;
using EXM.Base.Interfaces.Repositories;
using EXM.Domain.Entities;
using EXM.Common.Wrapper;
using MediatR;

namespace EXM.Base.Features.IncomeCategories.Queries.GetById
{
    public class GetIncomeCategoryByIdQuery : IRequest<Result<GetIncomeCategoryByIdResponse>>
    {
        public int Id { get; set; }

        public GetIncomeCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }

    internal class GetIncomeCategoryByIdQueryHandler : IRequestHandler<GetIncomeCategoryByIdQuery, Result<GetIncomeCategoryByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetIncomeCategoryByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetIncomeCategoryByIdResponse>> Handle(GetIncomeCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var incomeCategory = await _unitOfWork.Repository<IncomeCategory>().GetByIdAsync(query.Id);
            var mappedIncomeCategory = _mapper.Map<GetIncomeCategoryByIdResponse>(incomeCategory);
            return await Result<GetIncomeCategoryByIdResponse>.SuccessAsync(mappedIncomeCategory);
        }
    }
}
