using AutoMapper;
using EXM.Base.Features.IncomeCategories.Commands.AddEdit;
using EXM.Base.Features.IncomeCategories.Queries.GetAll;
using EXM.Base.Features.IncomeCategories.Queries.GetById;
using EXM.Domain.Entities;

namespace EXM.Base.Mappings
{
    public class IncomeCategoryProfile : Profile
    {
        public IncomeCategoryProfile()
        {
            CreateMap<AddEditIncomeCategoryCommand, IncomeCategory>().ReverseMap();
            CreateMap<GetIncomeCategoryByIdResponse, IncomeCategory>().ReverseMap();
            CreateMap<GetAllIncomeCategoriesResponse, IncomeCategory>().ReverseMap();
        }
    }
}
