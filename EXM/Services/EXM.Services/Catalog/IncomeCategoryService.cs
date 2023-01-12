using EXM.Common.Data.Contracts;
using EXM.Common.Data.Models;
using EXM.Common.Data.Wrapper;
using EXM.Common.Entities;
using EXM.Common.Models.IncomeCategories.Requests;
using EXM.Common.Models.IncomeCategories.Responses;
using EXM.Data.Domain;
using EXM.Data.Domain.Repositories.Catalog;
using EXM.Data.Domain.Specifications.Catalog;
using EXM.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EXM.Services.Catalog
{
    public class IncomeCategoryService : IIncomeCategoryService
    {
        private readonly EXMContext _context;
        private readonly ILogger<IncomeCategoryService> _logger;
        private readonly ICurrentUserService _currentUserService;

        public IncomeCategoryService(
            EXMContext context,
            ILogger<IncomeCategoryService> logger,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<Result<IncomeCategoryResponse>> AddEditAsync(IncomeCategoryRequest request)
        {
            using (IncomeCategoriesRepository repo = new IncomeCategoriesRepository(_context))
            {
                var entity = new IncomeCategory
                {
                    CreatedBy = _currentUserService.UserId,
                    CreatedOn = DateTime.UtcNow,
                    Id = request.Id,
                    LastModifiedBy = _currentUserService.UserId,
                    LastModifiedOn = DateTime.UtcNow,
                    Name = request.Name
                };

                var result = request.Id == 0 ? await repo.AddAsync(entity) : await repo.UpdateAsync(entity);

                if (result != null)
                {
                    return new Result<IncomeCategoryResponse>
                    {
                        Data = new IncomeCategoryResponse
                        {
                            Id = result.Id,
                            Name = result.Name
                        },
                        Messages = null,
                        Succeeded = true
                    };
                }
                else
                {
                    return new Result<IncomeCategoryResponse>
                    {
                        Data = null,
                        Messages = new List<string> { "Error on creating/updating the Income Category" },
                        Succeeded = false
                    };
                }
            }
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            using (IncomeCategoriesRepository repo = new IncomeCategoriesRepository(_context))
            {
                var incomeCategory = await repo.FirstOrDefaultAsync(x => x.Id == id);

                if (incomeCategory != null)
                {
                    var result = await repo.RemoveAsync(incomeCategory);
                    if (result != null)
                    {
                        return new Result<bool>
                        {
                            Data = true,
                            Messages = null,
                            Succeeded = true
                        };
                    }
                    else
                    {
                        return new Result<bool>
                        {
                            Data = false,
                            Messages = new List<string>
                            {
                                "Income Category failed to be deleted"
                            },
                            Succeeded = false
                        };
                    }
                }
                else
                {
                    return new Result<bool>
                    {
                        Data = false,
                        Messages = new List<string>
                        {
                            "Income Category doens't exists"
                        },
                        Succeeded = false
                    };
                }
            }
        }

        public async Task<Result<List<IncomeCategoryResponse>>> GetAllAsync()
        {
            using (IncomeCategoriesRepository repo = new IncomeCategoriesRepository(_context))
            {
                return new Result<List<IncomeCategoryResponse>>
                {
                    Data = await repo.Get().Select(ic => new IncomeCategoryResponse
                    {
                        Id = ic.Id,
                        Name = ic.Name
                    }).ToListAsync(),
                    Messages = null,
                    Succeeded = true
                };
            }
        }

        public async Task<Result<IncomeCategoryResponse>> GetByIdAsync(int id)
        {
            using (IncomeCategoriesRepository repo = new IncomeCategoriesRepository(_context))
            {
                var result = await repo.FirstOrDefaultAsync(ic => ic.Id == id);

                if (result == null)
                {
                    return new Result<IncomeCategoryResponse>
                    {
                        Data = null,
                        Messages = new List<string> 
                        {
                            "Error getting the Income Category"
                        },
                        Succeeded = false
                    };
                }
                else
                {
                    return new Result<IncomeCategoryResponse>
                    {
                        Data = new IncomeCategoryResponse
                        {
                            Id = result.Id,
                            Name = result.Name
                        },
                        Messages = null,
                        Succeeded = true
                    };
                }
            }
        }

        public async Task<DtResult<IncomeCategory>> GetPagedAsync(DtParameters parameters)
        {
            using (IncomeCategoriesRepository repo = new IncomeCategoriesRepository(_context))
            {
                var searchBy = parameters.Search?.Value;
                var specification = new IncomeCategoryFilterSpecification(searchBy);

                return await repo.GetPagedAsync(parameters, specification);
            }
        }
    }
}
