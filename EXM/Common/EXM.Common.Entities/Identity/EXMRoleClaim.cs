using EXM.Common.Interfaces.Contracts;
using Microsoft.AspNetCore.Identity;

namespace EXM.Common.Entities.Identity
{
    public class EXMRoleClaim : IdentityRoleClaim<string>, IAuditableEntity<int>
    {
        public string Description { get; set; }
        public string Group { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public virtual EXMRole Role { get; set; }

        public EXMRoleClaim() : base()
        {
        }

        public EXMRoleClaim(string roleClaimDescription = null, string roleClaimGroup = null) : base()
        {
            Description = roleClaimDescription;
            Group = roleClaimGroup;
        }
    }
}
