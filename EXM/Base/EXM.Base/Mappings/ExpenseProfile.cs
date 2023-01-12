using AutoMapper;
using EXM.Base.Features.Expenses.Commands.AddEdit;
using EXM.Domain.Entities;

namespace EXM.Base.Mappings
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<AddEditExpenseCommand, Expense>().ReverseMap();
        }
    }
}
