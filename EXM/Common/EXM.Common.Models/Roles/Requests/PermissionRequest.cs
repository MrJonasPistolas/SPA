using EXM.Common.Models.RoleClaims.Requests;

namespace EXM.Common.Models.Roles.Requests
{
    public class PermissionRequest
    {
        public string RoleId { get; set; }
        public IList<RoleClaimRequest> RoleClaims { get; set; }
    }
}
