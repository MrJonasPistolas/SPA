using AutoMapper;
using EXM.Base.Responses.Identity;
using EXM.Infrastructure.Models.Identity;

namespace EXM.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, EXMRole>().ReverseMap();
        }
    }
}
