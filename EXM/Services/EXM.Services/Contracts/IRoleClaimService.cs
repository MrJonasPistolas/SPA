using EXM.Common.Models.RoleClaims.Responses;
using EXM.Common.Models.RoleClaims.Requests;
using EXM.Common.Data.Contracts;
using EXM.Common.Data.Wrapper;

namespace EXM.Services.Contracts
{
    public interface IRoleClaimService : IService
    {
        Task<Result<List<RoleClaimResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleClaimResponse>> GetByIdAsync(int id);

        Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId);

        Task<Result<string>> SaveAsync(RoleClaimRequest request);

        Task<Result<string>> DeleteAsync(int id);
    }
}