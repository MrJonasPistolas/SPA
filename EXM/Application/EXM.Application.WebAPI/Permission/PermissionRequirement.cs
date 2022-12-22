using Microsoft.AspNetCore.Authorization;

namespace EXM.Application.WebAPI.Permission
{
    internal class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; private set; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}