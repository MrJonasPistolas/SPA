using EXM.Base.Specifications.Base;
using EXM.Domain.Entities;

namespace EXM.Base.Specifications.Catalog
{
    public class IncomeCategoryFilterSpecification : EXMSpecification<IncomeCategory>
    {
        public IncomeCategoryFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Name.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}
