namespace EXM.Base.Features.Expenses.Queries.GetAllByYear
{
    public class GetExpensesByYearResponse
    {
        public int Month { get; set; }
        public List<ExpensesByYear> Expenses { get; set; }
    }

    public class ExpensesByYear
    {
        public string Name { get; set; }
        public double Percentage { get; set; }
    }
}
