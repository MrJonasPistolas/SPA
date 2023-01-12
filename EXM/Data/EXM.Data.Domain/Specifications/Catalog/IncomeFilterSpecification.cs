using EXM.Common.Data.Specifications.Base;
using EXM.Common.Entities;

namespace EXM.Data.Domain.Specifications.Catalog
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
