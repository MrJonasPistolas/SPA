using EXM.Common.Data.Specifications.Base;
using EXM.Common.Entities;

namespace EXM.Data.Domain.Specifications.Catalog
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
