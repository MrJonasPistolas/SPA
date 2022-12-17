using EXM.Base.Interfaces.Common;

namespace EXM.Base.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}
