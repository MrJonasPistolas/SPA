using EXM.Base.Specifications.Base;
using EXM.Domain.Entities;

namespace EXM.Base.Specifications.Catalog
{
    public class IncomeFilterSpecification : EXMSpecification<Income>
    {
        public IncomeFilterSpecification(string searchString)
        {
            Includes.Add(a => a.Category);
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Name.Contains(searchString) || p.Description.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}
