namespace EXM.Base.Features.Expenses.Queries.GetAllByYear
{
    public class GetAllExpensesByYearResponse
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Number { get; set; }
        public int Percentage { get; set; }
        public double Percent { get; set; }
        public double Amount { get; set; }
    }
}
