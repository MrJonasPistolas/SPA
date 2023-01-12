using EXM.Common.Models.Users.Responses;

namespace EXM.Common.Models.Users.Requests
{
    public class UpdateUserRolesRequest
    {
        public string UserId { get; set; }
        public IList<UserRoleModel> UserRoles { get; set; }
    }
}
