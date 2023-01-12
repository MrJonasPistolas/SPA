using EXM.Common.Data.Contracts;
using EXM.Common.Data.Wrapper;
using EXM.Common.Entities.Identity;
using EXM.Common.Models.RoleClaims.Requests;
using EXM.Common.Models.RoleClaims.Responses;
using EXM.Data.Domain;
using EXM.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EXM.Services.Identity
{
    public class RoleClaimService : IRoleClaimService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly EXMContext _db;

        public RoleClaimService(
            ICurrentUserService currentUserService,
            EXMContext db)
        {
            _currentUserService = currentUserService;
            _db = db;
        }

        public async Task<Result<List<RoleClaimResponse>>> GetAllAsync()
        {
            var roleClaims = await _db.RoleClaims.ToListAsync();
            var roleClaimsResponse = roleClaims.Select(rc => new RoleClaimResponse
            {
                Description = rc.Description,
                Group = rc.Group,
                Id = rc.Id,
                RoleId = rc.RoleId,
                Selected = false,
                Type = rc.ClaimType,
                Value = rc.ClaimValue
            }).ToList();
            return await Result<List<RoleClaimResponse>>.SuccessAsync(roleClaimsResponse);
        }

        public async Task<int> GetCountAsync()
        {
            var count = await _db.RoleClaims.CountAsync();
            return count;
        }

        public async Task<Result<RoleClaimResponse>> GetByIdAsync(int id)
        {
            var roleClaim = await _db.RoleClaims.SingleOrDefaultAsync(x => x.Id == id);
            var roleClaimResponse = new RoleClaimResponse
            {
                Description = roleClaim.Description,
                Group = roleClaim.Group,
                Id = roleClaim.Id,
                RoleId = roleClaim.RoleId,
                Selected = false,
                Type = roleClaim.ClaimType,
                Value = roleClaim.ClaimValue
            };
            return await Result<RoleClaimResponse>.SuccessAsync(roleClaimResponse);
        }

        public async Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId)
        {
            var roleClaims = await _db.RoleClaims
                .Include(x => x.Role)
                .Where(x => x.RoleId == roleId)
                .ToListAsync();
            var roleClaimsResponse = roleClaims.Select(rc => new RoleClaimResponse
            {
                Description = rc.Description,
                Group = rc.Group,
                Id = rc.Id,
                RoleId = roleId,
                Selected = false,
                Type = rc.ClaimType,
                Value = rc.ClaimValue
            }).ToList();
            return await Result<List<RoleClaimResponse>>.SuccessAsync(roleClaimsResponse);
        }

        public async Task<Result<string>> SaveAsync(RoleClaimRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.RoleId))
            {
                return await Result<string>.FailAsync("Role is required.");
            }

            if (request.Id == 0)
            {
                var existingRoleClaim =
                    await _db.RoleClaims
                        .SingleOrDefaultAsync(x =>
                            x.RoleId == request.RoleId && x.ClaimType == request.Type && x.ClaimValue == request.Value);
                if (existingRoleClaim != null)
                {
                    return await Result<string>.FailAsync("Similar Role Claim already exists.");
                }
                var roleClaim = new EXMRoleClaim
                {
                    ClaimValue = request.Value,
                    RoleId = request.RoleId,
                    ClaimType = request.Type,
                    Id = request.Id,
                    Description = request.Description,
                    Group = request.Group
                };

                await _db.RoleClaims.AddAsync(roleClaim);
                await _db.SaveChangesAsync();
                return await Result<string>.SuccessAsync(string.Format("Role Claim {0} created.", request.Value));
            }
            else
            {
                var existingRoleClaim =
                    await _db.RoleClaims
                        .Include(x => x.Role)
                        .SingleOrDefaultAsync(x => x.Id == request.Id);
                if (existingRoleClaim == null)
                {
                    return await Result<string>.SuccessAsync("Role Claim does not exist.");
                }
                else
                {
                    existingRoleClaim.ClaimType = request.Type;
                    existingRoleClaim.ClaimValue = request.Value;
                    existingRoleClaim.Group = request.Group;
                    existingRoleClaim.Description = request.Description;
                    existingRoleClaim.RoleId = request.RoleId;
                    _db.RoleClaims.Update(existingRoleClaim);
                    await _db.SaveChangesAsync();
                    return await Result<string>.SuccessAsync(string.Format("Role Claim {0} for Role {1} updated.", request.Value, existingRoleClaim.Role.Name));
                }
            }
        }

        public async Task<Result<string>> DeleteAsync(int id)
        {
            var existingRoleClaim = await _db.RoleClaims
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (existingRoleClaim != null)
            {
                _db.RoleClaims.Remove(existingRoleClaim);
                await _db.SaveChangesAsync();
                return await Result<string>.SuccessAsync(string.Format("Role Claim {0} for {1} Role deleted.", existingRoleClaim.ClaimValue, existingRoleClaim.Role.Name));
            }
            else
            {
                return await Result<string>.FailAsync("Role Claim does not exist.");
            }
        }
    }
}