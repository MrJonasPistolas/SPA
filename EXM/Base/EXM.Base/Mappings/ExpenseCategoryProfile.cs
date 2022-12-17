using AutoMapper;
using EXM.Base.Features.ExpenseCategories.Commands.AddEdit;
using EXM.Base.Features.ExpenseCategories.Queries.GetAll;
using EXM.Base.Features.ExpenseCategories.Queries.GetById;
using EXM.Domain.Entities;

namespace EXM.Base.Mappings
{
    public class ExpenseCategoryProfile : Profile
    {
        public ExpenseCategoryProfile()
        {
            CreateMap<AddEditExpenseCategoryCommand, ExpenseCategory>().ReverseMap();
            CreateMap<GetExpenseCategoryByIdResponse, ExpenseCategory>().ReverseMap();
            CreateMap<GetAllExpenseCategoriesResponse, ExpenseCategory>().ReverseMap();
        }
    }
}
