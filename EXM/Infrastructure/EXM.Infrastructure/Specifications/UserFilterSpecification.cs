using EXM.Base.Specifications.Base;
using EXM.Infrastructure.Models.Identity;

namespace EXM.Infrastructure.Specifications
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
