using EXM.Application.API.Controllers;
using EXM.Common.Constants.Permission;
using EXM.Common.Data.Interfaces;
using EXM.Common.Data.Models;
using EXM.Common.Models.IncomeCategories.Requests;
using EXM.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace EXM.Application.API.Controllers.v1.Catalog
{

    public class IncomeCategoriesController : BaseApiController<IncomeCategoriesController>
    {
        #region > Properties <
        private readonly ILogger _logger;
        private readonly IIncomeCategoryService _incomeCategoryService;
        #endregion

        #region > Constructor <
        public IncomeCategoriesController(
            ILogger<IncomeCategoriesController> logger,
            IIncomeCategoryService incomeCategoryService
            )
        {
            _logger = logger;
            _incomeCategoryService = incomeCategoryService;
        }
        #endregion

        #region > Methods <
        [Authorize(Policy = Permissions.IncomeCategories.View)]
        [HttpPost("Paged")]
        public async Task<IActionResult> GetPaged(DtParameters parameters)
        {
            var result = await _incomeCategoryService.GetPagedAsync(parameters);
            return new JsonResult(result);
        }

        [Authorize(Policy = Permissions.IncomeCategories.View)]
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _incomeCategoryService.GetAllAsync());
        }

        [Authorize(Policy = Permissions.IncomeCategories.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _incomeCategoryService.GetByIdAsync(id));
        }

        [Authorize(Policy = Permissions.IncomeCategories.Create)]
        [HttpPost()]
        public async Task<IActionResult> AddAsync(IncomeCategoryRequest request)
        {
            var result = await _incomeCategoryService.AddEditAsync(request);
            return Ok(result);
        }

        [Authorize]
        [HttpPut()]
        public async Task<IActionResult> EditAsync(IncomeCategoryRequest request)
        {
            var result = await _incomeCategoryService.AddEditAsync(request);
            return Ok(result);
        }
        #endregion
    }
}
