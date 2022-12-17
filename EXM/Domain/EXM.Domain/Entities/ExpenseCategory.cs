using EXM.Domain.Contracts;

namespace EXM.Domain.Entities
{
    public class ExpenseCategory : AuditableEntity<int>
    {
        public string Name { get; set; }
    }
}
