namespace EXM.Common.Data.Contracts
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}
