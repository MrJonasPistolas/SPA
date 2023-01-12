using AutoMapper;
using EXM.Base.Responses.Identity;
using EXM.Infrastructure.Models.Identity;

namespace EXM.Infrastructure.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserResponse, EXMUser>().ReverseMap();
        }
    }
}
