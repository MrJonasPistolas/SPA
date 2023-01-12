using EXM.Common.Data.Models;
using EXM.Common.Data.Wrapper;
using EXM.Common.Entities;
using EXM.Common.Models.IncomeCategories.Requests;
using EXM.Common.Models.IncomeCategories.Responses;

namespace EXM.Services.Contracts
{
    public interface IIncomeCategoryService
    {
        Task<Result<List<IncomeCategoryResponse>>> GetAllAsync();
        Task<Result<IncomeCategoryResponse>> GetByIdAsync(int id);
        Task<DtResult<IncomeCategory>> GetPagedAsync(DtParameters parameters);
        Task<Result<IncomeCategoryResponse>> AddEditAsync(IncomeCategoryRequest request);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
