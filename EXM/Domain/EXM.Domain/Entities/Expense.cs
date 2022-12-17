using EXM.Domain.Contracts;

namespace EXM.Domain.Entities
{
    public class Expense : AuditableEntity<int>
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int ExpenseCategoryId { get; set; }
        public virtual ExpenseCategory Category { get; set; }
    }
}
