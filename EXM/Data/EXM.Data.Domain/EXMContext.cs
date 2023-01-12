using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EXM.Common.Entities;
using EXM.Common.Entities.Identity;
using EXM.Common.Data.Contracts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EXM.Data.Domain
{
    public class EXMContext : IdentityDbContext<EXMUser, EXMRole, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, EXMRoleClaim, IdentityUserToken<string>>
    {
        public EXMContext(DbContextOptions<EXMContext> options)
            : base(options)
        {
        }

        public DbSet<Income> Incomes { get; set; }
        public DbSet<IncomeCategory> IncomeCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

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
