using EXM.Common.Data.Specifications.Base;
using EXM.Common.Entities.Identity;

namespace EXM.Data.Domain.Specifications
{
    public class UserFilterSpecification : EXMSpecification<EXMUser>
    {
        public UserFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.FirstName.Contains(searchString) || p.LastName.Contains(searchString) || p.Email.Contains(searchString) || p.PhoneNumber.Contains(searchString) || p.UserName.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}
