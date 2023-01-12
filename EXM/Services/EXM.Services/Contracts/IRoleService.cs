using EXM.Common.Data.Contracts;
using EXM.Common.Data.Wrapper;
using EXM.Common.Models.Roles.Requests;
using EXM.Common.Models.Roles.Responses;

namespace EXM.Services.Contracts
{
    public interface IRoleService : IService
    {
        Task<Result<List<RoleResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleResponse>> GetByIdAsync(string id);

        Task<Result<string>> SaveAsync(RoleRequest request);

        Task<Result<string>> DeleteAsync(string id);

        Task<Result<PermissionResponse>> GetAllPermissionsAsync(string roleId);

        Task<Result<string>> UpdatePermissionsAsync(PermissionRequest request);
    }
}