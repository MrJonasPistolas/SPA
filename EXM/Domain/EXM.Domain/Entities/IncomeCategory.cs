using EXM.Domain.Contracts;

namespace EXM.Domain.Entities
{
    public class IncomeCategory : AuditableEntity<int>
    {
        public string Name { get; set; }
    }
}
