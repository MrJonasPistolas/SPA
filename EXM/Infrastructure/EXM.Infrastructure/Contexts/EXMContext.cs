using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using EXM.Domain.Entities;
using EXM.Base.Interfaces.Services;
using EXM.Domain.Contracts;
using EXM.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace EXM.Infrastructure.Contexts
{
    public class EXMContext : AuditableContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;

        public EXMContext(DbContextOptions<EXMContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
            : base(options)
        {
            _currentUserService = currentUserService;
            _dateTimeService = dateTimeService;
        }

        public DbSet<Income> Incomes { get; set; }
        public DbSet<IncomeCategory> IncomeCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTimeService.NowUtc;
                        entry.Entity.CreatedBy = _currentUserService.UserId == null ? "Seeder" : _currentUserService.UserId;
                        entry.Entity.LastModifiedOn = _dateTimeService.NowUtc;
                        entry.Entity.LastModifiedBy = _currentUserService.UserId == null ? "Seeder" : _currentUserService.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTimeService.NowUtc;
                        entry.Entity.LastModifiedBy = _currentUserService.UserId == null ? "Seeder" : _currentUserService.UserId;
                        break;
                }
            }
            if (_currentUserService.UserId == null)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return await base.SaveChangesAsync(_currentUserService.UserId, cancellationToken);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
            base.OnModelCreating(builder);
            builder.Entity<EXMUser>(entity =>
            {
                entity.ToTable(name: "Users", "Identity");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).IsRequired(false);
                entity.Property(e => e.LastName).IsRequired(false);
                entity.Property(e => e.CreatedBy).IsRequired(false);
                entity.Property(e => e.ProfilePictureDataUrl).IsRequired(false);
                entity.Property(e => e.LastModifiedBy).IsRequired(false);
                entity.Property(e => e.LastModifiedOn).IsRequired(false);
                entity.Property(e => e.DeletedOn).IsRequired(false);
                entity.Property(e => e.RefreshToken).IsRequired(false);
                entity.Property(e => e.UserName).IsRequired(false);
                entity.Property(e => e.NormalizedUserName).IsRequired(false);
                entity.Property(e => e.Email).IsRequired(false);
                entity.Property(e => e.NormalizedEmail).IsRequired(false);
                entity.Property(e => e.PasswordHash).IsRequired(false);
                entity.Property(e => e.SecurityStamp).IsRequired(false);
                entity.Property(e => e.ConcurrencyStamp).IsRequired(false);
                entity.Property(e => e.LockoutEnd).IsRequired(false);
                entity.Property(e => e.PhoneNumber).IsRequired(false);
            });

            builder.Entity<EXMRole>(entity =>
            {
                entity.ToTable(name: "Roles", "Identity");
                entity.Property(e => e.Description).IsRequired(false);
                entity.Property(e => e.CreatedBy).IsRequired(false);
                entity.Property(e => e.LastModifiedBy).IsRequired(false);
                entity.Property(e => e.LastModifiedOn).IsRequired(false);
                entity.Property(e => e.Name).IsRequired(false);
                entity.Property(e => e.NormalizedName).IsRequired(false);
                entity.Property(e => e.ConcurrencyStamp).IsRequired(false);
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles", "Identity");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims", "Identity");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins", "Identity");
            });

            builder.Entity<EXMRoleClaim>(entity =>
            {
                entity.ToTable(name: "RoleClaims", "Identity");

                entity.Property(e => e.Description).IsRequired(false);
                entity.Property(e => e.Group).IsRequired(false);
                entity.Property(e => e.CreatedBy).IsRequired(false);
                entity.Property(e => e.LastModifiedBy).IsRequired(false);
                entity.Property(e => e.LastModifiedOn).IsRequired(false);
                entity.Property(e => e.ClaimType).IsRequired(false);
                entity.Property(e => e.ClaimValue).IsRequired(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens", "Identity");
            });
        }
    }
}
