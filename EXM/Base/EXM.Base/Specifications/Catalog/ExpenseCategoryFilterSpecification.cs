using EXM.Base.Specifications.Base;
using EXM.Domain.Entities;

namespace EXM.Base.Specifications.Catalog
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
