using EXM.Common.Data.Specifications.Base;
using EXM.Common.Entities;

namespace EXM.Data.Domain.Specifications.Catalog
{
    public class ExpenseCategoryFilterSpecification : EXMSpecification<ExpenseCategory>
    {
        public ExpenseCategoryFilterSpecification(string searchString)
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
