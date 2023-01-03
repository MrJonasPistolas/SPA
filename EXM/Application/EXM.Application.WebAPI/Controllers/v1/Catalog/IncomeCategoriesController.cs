using EXM.Base.Features.IncomeCategories.Queries.GetAll;
using EXM.Common.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXM.Application.WebAPI.Controllers.v1.Catalog
{
    public class IncomeCategoriesController : BaseApiController<IncomeCategoriesController>
    {
        /// <summary>
        /// Get All Income Categories
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.IncomeCategories.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString = null, string orderBy = null)
        {
            var incomeCategories = await _mediator.Send(new GetAllIncomeCategoriesQuery(pageNumber, pageSize, searchString, orderBy));
            return Ok(incomeCategories);
        }
    }
}
