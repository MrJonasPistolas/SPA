using AutoMapper;
using EXM.Base.Requests.Identity;
using EXM.Base.Responses.Identity;
using EXM.Infrastructure.Models.Identity;

namespace EXM.Infrastructure.Mappings
{
    public class RoleClaimProfile : Profile
    {
        public RoleClaimProfile()
        {
            CreateMap<RoleClaimResponse, EXMRoleClaim>()
                .ForMember(nameof(EXMRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(EXMRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();

            CreateMap<RoleClaimRequest, EXMRoleClaim>()
                .ForMember(nameof(EXMRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(EXMRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();
        }
    }
}
