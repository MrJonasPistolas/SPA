using EXM.Common.Interfaces;

namespace EXM.Services.Contracts
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}
