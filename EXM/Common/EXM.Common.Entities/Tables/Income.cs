using EXM.Common.Base;

namespace EXM.Common.Entities
{
    public class Income : AuditableEntity<int>
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int IncomeCategoryId { get; set; }
        public virtual IncomeCategory Category { get; set; }
    }
}
