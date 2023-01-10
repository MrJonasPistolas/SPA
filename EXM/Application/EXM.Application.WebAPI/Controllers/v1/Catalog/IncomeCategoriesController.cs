using EXM.Base.Features.IncomeCategories.Commands.AddEdit;
using EXM.Base.Features.IncomeCategories.Commands.Delete;
using EXM.Base.Features.IncomeCategories.Queries.GetAll;
using EXM.Base.Features.IncomeCategories.Queries.GetById;
using EXM.Base.Features.Incomes.Commands.AddEdit;
using EXM.Base.Features.Incomes.Commands.Delete;
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
        [HttpGet()]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString = null, string orderBy = null)
        {
            var incomeCategories = await _mediator.Send(new GetAllIncomeCategoriesQuery(pageNumber, pageSize, searchString, orderBy));
            return Ok(incomeCategories);
        }

        /// <summary>
        /// Get Income Category by Id
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.IncomeCategories.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetIncomeCategoryByIdQuery(id)));
        }

        /// <summary>
        /// Create/Update a Income Category
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.IncomeCategories.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditIncomeCategoryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Income Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.IncomeCategories.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteIncomeCategoryCommand { Id = id }));
        }
    }
}
