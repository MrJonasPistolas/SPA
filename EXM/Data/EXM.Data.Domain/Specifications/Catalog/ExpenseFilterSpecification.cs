using EXM.Common.Data.Specifications.Base;
using EXM.Common.Entities;

namespace EXM.Data.Domain.Specifications.Catalog
{
    public class ExpenseFilterSpecification : EXMSpecification<Expense>
    {
        public ExpenseFilterSpecification(string searchString)
        {
            Includes.Add(a => a.Category);
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Name.Contains(searchString) || p.Description.Contains(searchString) || p.Category.Name.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}
