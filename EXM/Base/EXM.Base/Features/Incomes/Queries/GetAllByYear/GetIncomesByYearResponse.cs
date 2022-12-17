namespace EXM.Base.Features.Incomes.Queries.GetAllByYear
{
    public class GetIncomesByYearResponse
    {
        public int Month { get; set; }
        public List<IncomesByYear> Incomes { get; set; }
    }

    public class IncomesByYear
    {
        public string Name { get; set; }
        public double Percentage { get; set; }
    }
}
