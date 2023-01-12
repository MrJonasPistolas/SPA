using EXM.Common.Base;

namespace EXM.Common.Entities
{
    public class IncomeCategory : AuditableEntity<int>
    {
        public string Name { get; set; }
    }
}
