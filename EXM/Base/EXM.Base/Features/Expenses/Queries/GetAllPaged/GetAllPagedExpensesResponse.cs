using System;

namespace EXM.Base.Features.Expenses.Queries.GetAllPaged
{
    public class GetAllPagedExpensesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string ExpenseCategory { get; set; }
        public int ExpenseCategoryId { get; set; }
    }
}
