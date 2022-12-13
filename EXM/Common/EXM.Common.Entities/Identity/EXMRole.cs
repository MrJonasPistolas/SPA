using EXM.Common.Interfaces.Contracts;
using Microsoft.AspNetCore.Identity;

namespace EXM.Common.Entities.Identity
{
    public class EXMRole : IdentityRole, IAuditableEntity<string>
    {
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public virtual ICollection<EXMRoleClaim> RoleClaims { get; set; }

        public EXMRole() : base()
        {
            RoleClaims = new HashSet<EXMRoleClaim>();
        }

        public EXMRole(string roleName, string roleDescription = null) : base(roleName)
        {
            RoleClaims = new HashSet<EXMRoleClaim>();
            Description = roleDescription;
        }
    }
}
