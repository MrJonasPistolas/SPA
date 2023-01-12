using EXM.Common.Models.RoleClaims.Responses;

namespace EXM.Common.Models.Roles.Responses
{
    public class PermissionResponse
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<RoleClaimResponse> RoleClaims { get; set; }
    }
}
