using System.Collections.Generic;

namespace EXM.Base.Features.Dashboards.Queries.GetData
{
    public class DashboardDataResponse
    {
        public int IncomeCount { get; set; }
        public int IncomeCategoryCount { get; set; }
        public int ExpenseCount { get; set; }
        public int ExpenseCategoryCount { get; set; }
        public int UserCount { get; set; }
        public int RoleCount { get; set; }
        public List<ChartSeries> DataEnterBarChart { get; set; } = new();
        public Dictionary<string, double> IncomeByCategoryTypePieChart { get; set; }
        public Dictionary<string, double> ExpenseByCategoryTypePieChart { get; set; }
    }

    public class ChartSeries
    {
        public ChartSeries() { }

        public string Name { get; set; }
        public double[] Data { get; set; }
    }

}