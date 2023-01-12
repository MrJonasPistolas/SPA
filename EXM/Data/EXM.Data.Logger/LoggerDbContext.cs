using EXM.Common.Entities.Logger;
using Microsoft.EntityFrameworkCore;

namespace EXM.Data.Logger
{
    public class LoggerDbContext : DbContext
    {
        #region constructors 
        public LoggerDbContext() : base()
        {
        }

        public LoggerDbContext(DbContextOptions<LoggerDbContext> options)
        : base(options)
        {
        }

        public LoggerDbContext(string connectionString)
        : base(GetOptions(connectionString))
        {
        }
        #endregion
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }


        #region DbSets
        public DbSet<EventLog> EventLogSet { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<EventLog>(e =>
            {
                e.HasKey(pk => new { pk.Id, pk.SystemLog });
                e.Property(p => p.Id).UseIdentityColumn();
                e.ToTable("AspNetEventLog");
            });

        }
    }
}
