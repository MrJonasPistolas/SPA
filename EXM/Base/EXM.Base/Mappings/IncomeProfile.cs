using AutoMapper;
using EXM.Base.Features.Incomes.Commands.AddEdit;
using EXM.Base.Models;
using EXM.Domain.Entities;

namespace EXM.Base.Mappings
{
    public class IncomeProfile : Profile
    {
        public IncomeProfile()
        {
            CreateMap<AddEditIncomeCommand, Income>().ReverseMap();
            CreateMap<UploadIncomeModel, Income>().ReverseMap();
        }
    }
}
