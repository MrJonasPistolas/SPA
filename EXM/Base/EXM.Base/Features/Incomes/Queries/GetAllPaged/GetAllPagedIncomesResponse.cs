using System;

namespace EXM.Base.Features.Incomes.Queries.GetAllPaged
{
    public class GetAllPagedIncomesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string IncomeCategory { get; set; }
        public int IncomeCategoryId { get; set; }
    }
}
