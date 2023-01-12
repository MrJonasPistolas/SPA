using EXM.Common.Base;

namespace EXM.Common.Entities
{
    public class ExpenseCategory : AuditableEntity<int>
    {
        public string Name { get; set; }
    }
}
