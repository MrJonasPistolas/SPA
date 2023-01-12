using EXM.Common.Data.Wrapper;
using EXM.Common.Entities;
using EXM.Common.Models.IncomeCategories.Responses;
using EXM.Data.Domain.Contracts.Catalog;
using Microsoft.EntityFrameworkCore;

namespace EXM.Data.Domain.Repositories.Catalog
{
    public class IncomeCategoriesRepository : RepositoryBase<IncomeCategory>, IIncomeCategoriesRepository
    {
        public IncomeCategoriesRepository(EXMContext dbContext) : base(dbContext)
        {
        }

        //public async Task<DtResult<IncomeCategoryResponse>> GetPagedAsync(DtParameters parameters)
        //{
        //    var searchBy = parameters.Search?.Value;

        //    // if we have an empty search then just order the results by Id ascending
        //    var orderCriteria = "Id";
        //    var orderAscendingDirection = true;

        //    if (parameters.Order != null)
        //    {
        //        // in this example we just default sort on the 1st column
        //        orderCriteria = parameters.Columns[parameters.Order[0].Column].Data;
        //        orderAscendingDirection = parameters.Order[0].Dir == "asc";
        //    }

        //    var result = Context.IncomeCategories.AsQueryable();

        //    if (!string.IsNullOrEmpty(searchBy))
        //    {
        //        result = result.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()));
        //    }

        //    result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria, "asc") : result.OrderByDynamic(orderCriteria, "desc");

        //    // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
        //    var filteredResultsCount = await result.CountAsync();
        //    var totalResultsCount = await Context.IncomeCategories.CountAsync();

        //    return new DtResult<IncomeCategoryResponse>
        //    {
        //        Draw = parameters.Draw,
        //        RecordsTotal = totalResultsCount,
        //        RecordsFiltered = filteredResultsCount,
        //        Data = await result
        //            .Skip(parameters.Start)
        //            .Take(parameters.Length)
        //            .Select(ic => new IncomeCategoryResponse
        //            {
        //                Id = ic.Id,
        //                Name = ic.Name
        //            })
        //            .ToListAsync()
        //    };
        //}
    }
}
