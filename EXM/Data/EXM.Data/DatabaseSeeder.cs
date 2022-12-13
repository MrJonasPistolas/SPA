using EXM.Common.Constants.Permission;
using EXM.Common.Entities.Identity;
using EXM.Data.Contexts;
using EXM.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EXM.Data
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly ILogger<DatabaseSeeder> _logger;
        private readonly IStringLocalizer<DatabaseSeeder> _localizer;
        private readonly EXMContext _db;
        private readonly UserManager<EXMUser> _userManager;
        private readonly RoleManager<EXMRole> _roleManager;

        public DatabaseSeeder(
            UserManager<EXMUser> userManager,
            RoleManager<EXMRole> roleManager,
            EXMContext db,
            ILogger<DatabaseSeeder> logger,
            IStringLocalizer<DatabaseSeeder> localizer)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _logger = logger;
            _localizer = localizer;
        }

        public void Initialize()
        {
            AddAdministrator();
            AddBasicUser();
            _db.SaveChanges();
        }

        private void AddAdministrator()
        {
            try
            {
                Task.Run(async () =>
                {
                    //Check if Role Exists
                    var adminRole = new EXMRole(RoleConstants.AdministratorRole, _localizer["Administrator role with full permissions"]);
                    var adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
                    if (adminRoleInDb == null)
                    {
                        await _roleManager.CreateAsync(adminRole);
                        adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
                        _logger.LogInformation(_localizer["Seeded Administrator Role."]);
                    }
                    //Check if User Exists
                    var superUser = new EXMUser
                    {
                        FirstName = "Admin",
                        LastName = "EXM",
                        Email = "admin@XM.com",
                        UserName = "admin",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        CreatedOn = DateTime.Now,
                        IsActive = true
                    };
                    var superUserInDb = await _userManager.FindByEmailAsync(superUser.Email);
                    if (superUserInDb == null)
                    {
                        await _userManager.CreateAsync(superUser, UserConstants.DefaultPassword);
                        var result = await _userManager.AddToRoleAsync(superUser, RoleConstants.AdministratorRole);
                        if (result.Succeeded)
                        {
                            _logger.LogInformation(_localizer["Seeded Default SuperAdmin User."]);
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                _logger.LogError(error.Description);
                            }
                        }
                    }
                    foreach (var permission in Permissions.GetRegisteredPermissions())
                    {
                        await _roleManager.AddPermissionClaim(adminRoleInDb, permission);
                    }
                }).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                var error = $"{ex.Message} - {ex.StackTrace}";
            }
        }

        private void AddBasicUser()
        {
            Task.Run(async () =>
            {
                //Check if Role Exists
                var basicRole = new EXMRole(RoleConstants.BasicRole, _localizer["Basic role with default permissions"]);
                var basicRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.BasicRole);
                if (basicRoleInDb == null)
                {
                    await _roleManager.CreateAsync(basicRole);
                    _logger.LogInformation(_localizer["Seeded Basic Role."]);
                }
                //Check if User Exists
                var basicUser = new EXMUser
                {
                    FirstName = "João",
                    LastName = "Santos",
                    Email = "joamsantos@sapo.pt",
                    UserName = "joamsantos",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CreatedOn = DateTime.Now,
                    IsActive = true
                };
                var basicUserInDb = await _userManager.FindByEmailAsync(basicUser.Email);
                if (basicUserInDb == null)
                {
                    await _userManager.CreateAsync(basicUser, UserConstants.DefaultPassword);
                    await _userManager.AddToRoleAsync(basicUser, RoleConstants.BasicRole);
                    _logger.LogInformation(_localizer["Seeded User with Basic Role."]);
                }
            }).GetAwaiter().GetResult();
        }
    }
}
