namespace EXM.Base.Features.Dashboards.Queries.GetDataByYear
{
    public class DashboardByYearDataResponse
    {
        public List<BarSeries> Incomes { get; set; } = new();
        public List<BarSeries> Expenses { get; set; } = new();
        public List<BarSeries> Difference { get; set; } = new();
    }

    public class BarSeries
    {
        public BarSeries() { }
        public int Month { get; set; }
        public string Name { get; set; }
        public double Sum { get; set; }
    }
}
