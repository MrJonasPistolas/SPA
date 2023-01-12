using AutoMapper;
using EXM.Base.Responses.Audit;
using EXM.Infrastructure.Models.Audit;

namespace EXM.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}
